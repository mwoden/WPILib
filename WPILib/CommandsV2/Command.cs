using System;
using System.Linq;

namespace WPILib.CommandsV2
{
    /// <summary>
    /// The basic command
    /// A standard command can require a subsystem, or be an orphan
    /// </summary>
    public sealed class Command : ICommand, IEquatable<Command>
    {
        /// <summary>
        /// Returns a command that just delays for the specifed amount of time
        /// </summary>
        /// <param name="timeout">The length of time to delay</param>
        public static Command Wait(TimeSpan timeout)
        {
            var cmd = new Command("Wait", timeout);

            cmd.OnInitialize += c => Console.WriteLine(string.Format("{0}{1} ({2}):  {3} @ {4}", "", c.Name, c.Id, "Init", ""));
            cmd.OnEnd += c => Console.WriteLine(string.Format("{0}{1} ({2}):  {3} @ {4}", "", c.Name, c.Id, "End", ""));

            // The command will finish when it has timed out
            cmd.IsFinished += () => cmd.IsTimedOut;

            return cmd;
        }

        /// <summary>
        /// Returns a command that executes the action immediately and finishes
        /// </summary>
        /// <param name="action">The action to execute</param>
        public static Command Instant(Action action)
        {
            var cmd = new Command("Instant");

            cmd.OnInitialize += c => action();
            cmd.OnInitialize += c => Console.WriteLine(string.Format("{0}{1} ({2}):  {3} @ {4}", "", c.Name, c.Id, "Init", ""));
            cmd.OnEnd += c => Console.WriteLine(string.Format("{0}{1} ({2}):  {3} @ {4}", "", c.Name, c.Id, "End", ""));

            // The command finishes immediately
            cmd.IsFinished += () => true;

            return cmd;
        }

        /// <summary>
        /// Returns a command that executes the action immediately and finishes
        /// </summary>
        /// <param name="subsystem">The required subsystem</param>
        /// <param name="action">The action to execute</param>
        /// <returns></returns>
        public static Command Instant(ISubsystem subsystem, Action action)
        {
            var cmd = Instant(action);

            cmd.Required = subsystem;

            return cmd;
        }

        public static ITimer Timer { get; set; }

        /// <summary>
        /// Runs when the command is initialized
        /// </summary>
        public event Action<ICommand> OnInitialize;
        public event Action<ICommand> OnExecute;
        public event Action<ICommand> OnComplete;
        public event Action<ICommand> OnInterrupt;
        public event Action<ICommand> OnEnd;
        public event Func<bool> IsFinished;

        public string Name { get; }
        public EntityId Id => _id;
        public bool IsInterruptible => _isInterruptible;
        public bool IsTimedOut => RunningTime > _timeOut;
        public ISubsystem Required { get; private set; }
        public bool IsRunning => _isRunning;

        public Command()
        {
            Name = GetType().Name.Split(new[] { '.' }).Last();
        }

        public Command(ISubsystem subsystem)
            : this()
        {
            Required = subsystem;
        }

        public Command(ISubsystem subsystem, TimeSpan timeOut)
            : this(subsystem)
        {
            _timeOut = timeOut;
        }

        public Command(ISubsystem subsystem, string name)
            : this(subsystem)
        {
            Name = name;
        }

        public Command(ISubsystem subsystem, TimeSpan timeOut, string name)
            : this(subsystem, timeOut)
        {
            Name = name;
        }

        public Command(TimeSpan timeOut)
            : this()
        {
            _timeOut = timeOut;
        }

        public Command(string name, TimeSpan timeOut)
            : this(name)
        {
            _timeOut = timeOut;
        }

        public Command(string name)
        {
            Name = name;
        }

        private Command(Command original)
            : this()
        {
            OnComplete = original.OnComplete;
            OnEnd = original.OnEnd;
            OnExecute = original.OnExecute;
            OnInitialize = original.OnInitialize;
            OnInterrupt = original.OnInterrupt;
            IsFinished = original.IsFinished;
            Name = original.Name;
            Required = original.Required;
            _isFrozen = original._isFrozen;
            _isInterruptible = original._isInterruptible;
        }

        /// <summary>
        /// Set the command to be non-interruptible
        /// This can only be done before the command is run for the first time
        /// </summary>
        public void SetNonInterruptible()
        {
            if (_isFrozen)
                throw new InvalidOperationException("Can't change command interruptible status after it's been started");

            _isInterruptible = false;
        }

        /// <summary>
        /// Starting a command will cause the scheduler to schedule the command
        /// </summary>
        public void Start()
        {
            _isFrozen = true;

            if (!_isRunning)
            {
                _isRunning = true;
                ((IScheduler)(Scheduler.Instance)).Start(this);
            }
        }

        /// <summary>
        /// The main processing element of the command. This will be called repeatedly
        /// by the scheduler.
        /// </summary>
        void ICommand.Run()
        {
            RunInitialize();

            OnExecute?.Invoke(this);

            if (IsDone)
                RunComplete();

            if (IsTimedOut)
                RunInterrupted();
        }

        /// <summary>
        /// Causes the command to raise the Interrupted event
        /// </summary>
        void ICommand.Stop()
        {
            RunInterrupted();
        }

        public ICommand Duplicate() => new Command(this);

        public static bool operator ==(Command lhs, Command rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Command lhs, Command rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(Command command)
        {
            if (ReferenceEquals(null, command))
                return false;

            return _id == command._id;
        }

        public override bool Equals(object obj)
        {
            var cmd = obj as Command;

            return cmd != null && _id.Equals(cmd._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Command: {0} ({1,3})  Subsystem: {2}", Name, (int)_id, Required);
        }


        private TimeSpan RunningTime => Timer.Now - _startTime;
        private bool IsDone => IsFinished?.Invoke() ?? true;

        private readonly EntityId _id = EntityId.Generate();
        private readonly TimeSpan? _timeOut;

        private bool _isInitialized;
        private bool _isRunning;
        private bool _isInterruptible = true;
        private bool _isFrozen;
        private TimeSpan _startTime;

        /// <summary>
        /// Helper to test that the command hasn't been initialized and run it if
        /// it hasn't been
        /// </summary>
        private void RunInitialize()
        {
            if (!_isInitialized)
            {
                OnInitialize?.Invoke(this);
                _isInitialized = true;
                _startTime = Timer.Now;
            }
        }

        /// <summary>
        /// Helper to raise the 'Complete' event when the command is done
        /// </summary>
        private void RunComplete()
        {
            if (_isRunning)
            {
                OnComplete?.Invoke(this);
                Stopping();
            }
        }

        /// <summary>
        /// Helper to raise the 'Interrupted' event when the command is done
        /// </summary>
        private void RunInterrupted()
        {
            if (_isRunning)
            {
                OnInterrupt?.Invoke(this);
                Stopping();
            }
        }

        /// <summary>
        /// Helper to raise the 'Ended' event and set the command ready to run again
        /// </summary>
        private void Stopping()
        {
            OnEnd?.Invoke(this);
            _isInitialized = false;
            _isRunning = false;
        }

    }
}