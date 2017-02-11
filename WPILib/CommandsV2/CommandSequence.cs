using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WPILib.CommandsV2
{
    /// <summary>
    /// A <see cref="CommandSequence"/> is a set of commands that will run serially
    /// </summary>
    public sealed class CommandSequence : ICommand, IMultiCommand
    {
        public event Action<ICommand> OnComplete;
        public event Action<ICommand> OnInterrupt;
        public event Action<ICommand> OnEnd;

#pragma warning disable CS0067
        // A CommandSequence doesn't have any external actions on initalize
        public event Action<ICommand> OnInitialize;
        // A CommandSequence doesn't have any external actions on execute
        public event Action<ICommand> OnExecute;
        // A CommandSequence ends when all the commands are completed
        public event Func<bool> IsFinished;
#pragma warning restore CS0067

        public bool IsInterruptible => _isInterruptible;
        public ISubsystem Required => null;
        public EntityId Id { get; } = EntityId.Generate();
        public string Name => string.Format("CommandSequence ({0})", (int)Id);
        public bool IsRunning => _isRunning;

        public IEnumerable<ISubsystem> Requirements => _subsystems;
        public IEnumerable<ICommand> Commands => _groups.SelectMany(x => x.Commands);

        public CommandSequence() { }

        public CommandSequence Add(Command command)
        {
            return Add(new CommandGroup(command));
        }

        public CommandSequence Add(CommandGroup commandGroup)
        {
            return new CommandSequence(_groups.Concat(new[] { commandGroup }));
        }

        private CommandSequence(IEnumerable<CommandGroup> groups)
        {
            _groups = groups.Select(x => new CommandGroup(x)).ToList().AsReadOnly();

            _subsystems = _groups.Cast<IMultiCommand>()
                                 .SelectMany(x => x.Requirements)
                                 .Distinct()
                                 .ToList()
                                 .AsReadOnly();

            foreach (var group in _groups)
                group.OnEnd += CommandEnded;

            _isInterruptible = groups.All(x => x.IsInterruptible);
        }

        public void SetNonInterruptible()
        {
            throw new InvalidOperationException("Command group can't be set non-interruptible");
        }

        public void Start()
        {
            if (!_groups.Any())
                throw new InvalidOperationException("CommandSequence has no items");

            // Get all the commands in the command group
            var commandsWithRequirement = Commands.Where(x => x.Required != null)
                                                  .Distinct()
                                                  .ToList();

            // Determine if any of the commands can't run on their requirerd subsystem
            var canRun = commandsWithRequirement.All(cmd => cmd.Required.IsRunnable(cmd));

            // If the command isn't blocked, stop all the subsystems and begin running the command group
            if (canRun)
            {
                _isRunning = true;
                ((IScheduler)Scheduler.Instance).Start(this);

                foreach (var subsystem in _subsystems)
                    subsystem.StopActiveCommand();

                _activeCommands = new Queue<CommandGroup>(_groups);

                _activeCommands.Dequeue().Start();

            }
        }

        void ICommand.Run()
        {
            // If all of the commands in the group are done, the command group is done
            if (!_activeCommands.Any())
                FinishCommandSequence(false);
                   }

        public void Stop()
        {
            foreach (var group in _groups)
                ((ICommand)group).Stop();

            FinishCommandSequence(true);
        }

        private readonly IList<CommandGroup> _groups = new CommandGroup[0];

        private readonly bool _isInterruptible;
        private readonly IList<ISubsystem> _subsystems;
        private bool _isInitialized;
        private bool _isRunning;

        private Queue<CommandGroup> _activeCommands = new Queue<CommandGroup>();


        /// <summary>
        /// Runs the appropriate command-ending events and reset state variables for the next time
        /// </summary>
        /// <param name="interrupted">Indicates if the command finished normally or was interrupted</param>
        private void FinishCommandSequence(bool interrupted)
        {
            (interrupted ? OnInterrupt : OnComplete)?.Invoke(this);
            OnEnd?.Invoke(this);
            _isInitialized = false;
            _isRunning = false;
            _activeCommands.Clear();
        }

        /// <summary>
        /// This handler will count the number of commands that complete
        /// </summary>
        private void CommandEnded(ICommand command)
        {
            command.OnEnd -= CommandEnded;

            if (_activeCommands.Any())
                _activeCommands.Dequeue().Start();
        }
    }
}