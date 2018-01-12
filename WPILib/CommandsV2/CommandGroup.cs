using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WPILib.CommandsV2
{
    /// <summary>
    /// A <see cref="CommandGroup"/> is a set of commands that will run in parallel
    /// </summary>
    public sealed class CommandGroup : ICommand, IMultiCommand
    {
        public event Action<ICommand> OnComplete;
        public event Action<ICommand> OnInterrupt;
        public event Action<ICommand> OnEnd;

#pragma warning disable CS0067
        // A CommandGroup doesn't have any external actions on initialize
        public event Action<ICommand> OnInitialize;
        // A CommandGroup doesn't have any external actions on execute
        public event Action<ICommand> OnExecute;
#pragma warning restore CS0067
        // A CommandGroup ends when all the commands are completed
        public Func<bool> IsFinished { set { throw new NotSupportedException(); } }


        public bool IsInterruptible => _isInterruptible;
        public ISubsystem Required => null;
        public EntityId Id { get; } = EntityId.Generate();
        public string Name => string.Format("CommandGroup ({0})", (int)Id);
        public TimeSpan Duration { get; private set; }
        public bool IsRunning => _isRunning;

        public IEnumerable<ISubsystem> Requirements => _subsystems;
        public IEnumerable<ICommand> Commands => _commands;

        //TODO Need to be able to force a timeout on commands added to a group
        public CommandGroup(params Command[] list)
            : this(list.Cast<ICommand>())
        { }

        public ICommand Duplicate() => new CommandGroup(this);

        public static CommandGroup Create(params Command[] list)
        {
            return new CommandGroup(list);
        }

        public void SetNonInterruptible()
        {
            throw new InvalidOperationException("Command group can't be set non-interruptible");
        }

        public void Start()
        {
            if (!_isRunning)
            {
                // Get all the commands in the command group
                var commandsWithRequirement = Commands.Where(x => x.Required != null)
                                                      .Distinct()
                                                       .ToList();

                // Determine if any of the commands can't run on their requirerd subsystem
                var canRun = commandsWithRequirement.All(cmd => cmd.Required.IsRunnable(cmd));

                // If the command isn't blocked, stop all the subsystems and begin running the command group
                if (canRun)
                {
                    _startTime = Scheduler.Instance.Timer.Now;
                    _isRunning = true;
                    ((IScheduler)Scheduler.Instance).Start(this);

                    foreach (var subsystem in _subsystems)
                        subsystem.StopActiveCommand();

                    foreach (var command in _commands)
                        command.Start();
                }
            }
        }

        void ICommand.Run()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                OnInitialize?.Invoke(this);
            }

            // If all of the commands in the group are done, the command group is done
            if (_commandsCompleted == _commands.Count())
                FinishCommandGroup(false);
        }

        public void Stop()
        {
            foreach (var command in _commands)
                ((ICommand)command).Stop();

            FinishCommandGroup(true);
        }

        private readonly bool _isInterruptible;
        private readonly IList<ICommand> _commands;
        private readonly IList<ISubsystem> _subsystems;

        private bool _isInitialized;
        private bool _isRunning;
        private int _commandsCompleted;
        private TimeSpan _startTime;

        private CommandGroup(CommandGroup group)
         : this(group._commands) { }

        private CommandGroup(IEnumerable<ICommand> list)
        {
            _commands = list.Select(x => x.Duplicate()).ToList().AsReadOnly();

            foreach (var command in _commands)
                command.OnEnd += CommandEnded;

            _isInterruptible = _commands.All(x => x.IsInterruptible);
            _subsystems = _commands.Select(x => x.Required)
                                   .Where(x => x != null)
                                   .Distinct()
                                   .ToList().AsReadOnly();
        }

        /// <summary>
        /// Runs the appropriate command-ending events and reset state variables for the next time
        /// </summary>
        /// <param name="interrupted">Indicates if the command finished normally or was interrupted</param>
        private void FinishCommandGroup(bool interrupted)
        {
            Duration = Scheduler.Instance.Timer.Now - _startTime;
            (interrupted ? OnInterrupt : OnComplete)?.Invoke(this);
            OnEnd?.Invoke(this);
            _isInitialized = false;
            _isRunning = false;
            _commandsCompleted = 0;
        }

        /// <summary>
        /// This handler will count the number of commands that complete
        /// </summary>
        private void CommandEnded(ICommand command)
        {
            _commandsCompleted++;
            command.OnEnd -= CommandEnded;
        }
    }
}