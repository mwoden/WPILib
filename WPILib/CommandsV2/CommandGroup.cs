using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WPILib.CommandsV2
{
    public sealed class CommandGroup : ICommand
    {
        public event Action<ICommand> OnInitialize;
        public event Action<ICommand> OnComplete;
        public event Action<ICommand> OnInterrupt;
        public event Action<ICommand> OnEnd;

#pragma warning disable CS0067
        public event Action<ICommand> OnExecute;
        public event Func<bool> IsFinished;
#pragma warning restore CS0067

        public bool IsInterruptible => _isInterruptible;
        public ISubsystem Required => null;
        public EntityId Id => _id;
        public string Name => string.Format("CommandGroup ({0})", (int)Id);
        public bool IsRunning => _isRunning;

        public CommandGroup(params Grouping[] list)
        {
            _subgroups = list.Select((x, index) => new Subgroup(index + 1, x)).ToList();
            _isInterruptible = list.SelectMany(x => x).All(x => x.IsInterruptible);

            _subsystems = _subgroups.SelectMany(x => x.Subsystems).Distinct().ToList();
        }

        public CommandGroup(CommandGroup commandGroup)
        {
            //_subgroups = list.Select((x, index) => new Subgroup(index + 1, x)).ToList();
            //_isInterruptible = list.SelectMany(x => x).All(x => x.IsInterruptible);

            // Grab each 


            _subsystems = commandGroup._subsystems;
        }

        public class Grouping : IEnumerable<Command>
        {
            public IReadOnlyCollection<Command> Commands { get; private set; }

            //TODO Should be able to pass a command group in as well, and have it do the 'right' thing
            public Grouping(params Command[] list) { Commands = list.ToList().AsReadOnly(); }

            public Grouping(params CommandGroup[] list)
            {
                throw new NotImplementedException();


            }


            public static Grouping Create(params Command[] list) { return new Grouping(list); }

            public IEnumerator<Command> GetEnumerator() { return Commands.GetEnumerator(); }

            IEnumerator IEnumerable.GetEnumerator() { return Commands.GetEnumerator(); }
        }

        public void SetNonInterruptible()
        {
            throw new InvalidOperationException("Command group can't be set non-interruptible");
        }

        public void Start()
        {
            // Get all the commands in the command group
            var commandsWithRequirement = _subgroups.SelectMany(x => x)
                                                    .Where(x => x.Required != null)
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

                _runningSubgroups = new Queue<Subgroup>(_subgroups);

                CheckAndRunNextGroup();
            }
        }

        void ICommand.Run()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                OnInitialize?.Invoke(this);
            }

            // Check if the current subgroup is done, and if it is, start the next
            // subgroup
            CheckAndRunNextGroup();

            // If the running subgroup has gone past the end, the command group is done
            if (_activeSubgroup.IsDone && !_runningSubgroups.Any())
            {
                _activeSubgroup = Subgroup.Default;
                OnComplete?.Invoke(this);
                OnEnd?.Invoke(this);
                _isInitialized = false;
                _isRunning = false;
            }
        }

        /// <summary>
        /// This method will stop the current subgroup and end the command group
        /// </summary>
        public void Stop()
        {
            _activeSubgroup.Stop();
            _runningSubgroups.Clear();
            _activeSubgroup = Subgroup.Default;
            OnInterrupt?.Invoke(this);
            OnEnd?.Invoke(this);
            _isInitialized = false;
            _isRunning = false;
        }

        private readonly EntityId _id = EntityId.Generate();
        private readonly bool _isInterruptible;
        private readonly List<Subgroup> _subgroups;
        private readonly List<ISubsystem> _subsystems;
        private bool _isInitialized;
        private bool _isRunning;

        private Queue<Subgroup> _runningSubgroups;
        private Subgroup _activeSubgroup = Subgroup.Default;


        /// <summary>
        /// Helper class to manager the subgroups in the command group
        /// </summary>
        private class Subgroup : IEnumerable<Command>
        {
            public Subgroup(int index, IEnumerable<Command> commands)
            {
                _index = index;
                _commands = commands.Select(x => new Command(x)).ToList();
                _subsystems = new HashSet<ISubsystem>(_commands.Select(x => x.Required).Where(x => x != null));

                foreach (var cmd in _commands)
                    cmd.OnEnd += CommandEnded;
            }

            public void Start()
            {
                foreach (var cmd in _commands)
                    cmd.Start();
            }

            public void Stop()
            {
                foreach (var entry in _subsystems.Cast<ISubsystem>())
                    entry.StopActiveCommand();
            }

            public IEnumerator<Command> GetEnumerator()
            {
                return _commands.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _commands.GetEnumerator();
            }

            public HashSet<ISubsystem> Subsystems => _subsystems;

            public int Index => _index;
            public bool IsDone => _commands.Count == _commandsDone;

            public static Subgroup Default => _default;

            private readonly HashSet<ISubsystem> _subsystems;
            private readonly List<Command> _commands;
            private readonly int _index;

            private int _commandsDone;

            private void CommandEnded(ICommand command)
            {
                _commandsDone++;
            }

            private static readonly Subgroup _default = new Subgroup(1, new Command[0]);
        }


        private void CheckAndRunNextGroup()
        {
            // Check if the current subgroup is done and there's another subgroup to run
            if (_activeSubgroup.IsDone && _runningSubgroups.Any())
            {
                _activeSubgroup = _runningSubgroups.Dequeue();
                _activeSubgroup.Start();
            }
        }

        /// <summary>
        /// This handler will count the number of commands that complete
        /// </summary>
        private void CommandEnded(ICommand command)
        {
            command.OnEnd -= CommandEnded;
        }
    }
}
