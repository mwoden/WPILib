using System;
using System.Collections.Generic;
using System.Linq;

namespace WPILib.CommandsV2
{
    public sealed class Scheduler : IScheduler
    {
        abstract public class Button
        {
            public sealed class WhenActive : Button
            {
                //TODO This should accept the actual hardware object
                public static void Link(Func<bool> buttonState, ICommand command)
                {
                    new WhenActive(buttonState, command);
                }

                public override void Execute()
                {
                    if (GetCurrentState() == State.Active)
                    {
                        if (lastState == State.Inactive)
                        {
                            lastState = State.Active;
                            _command.Start();
                        }
                    }
                    else
                        lastState = State.Inactive;
                }

                private WhenActive(Func<bool> buttonState, ICommand command)
                    : base(buttonState, command)
                { }
            }

            public sealed class WhileActive : Button
            {
                //TODO This should accept the actual hardware object
                public static void Link(Func<bool> buttonState, ICommand command)
                {
                    new WhileActive(buttonState, command);
                }

                public override void Execute()
                {
                    if (GetCurrentState() == State.Active)
                    {
                        lastState = State.Active;
                        _command.Start();
                    }
                    else
                    {
                        lastState = State.Inactive;
                        _command.Stop();
                    }
                }

                private WhileActive(Func<bool> buttonState, ICommand command)
                    : base(buttonState, command)
                { }
            }

            public sealed class WhenInactive : Button
            {
                //TODO This should accept the actual hardware object
                public static void Link(Func<bool> buttonState, ICommand command)
                {
                    new WhenInactive(buttonState, command);
                }

                public override void Execute()
                {
                    if (GetCurrentState() == State.Inactive)
                    {
                        if (lastState == State.Active)
                        {
                            lastState = State.Inactive;
                            _command.Start();
                        }
                    }
                    else
                        lastState = State.Active;
                }

                private WhenInactive(Func<bool> buttonState, ICommand command)
                    : base(buttonState, command)
                { }
            }

            public sealed class ToggleWhenActive : Button
            {
                //TODO This should accept the actual hardware object
                public static void Link(Func<bool> buttonState, ICommand command)
                {
                    new ToggleWhenActive(buttonState, command);
                }

                public override void Execute()
                {
                    if (GetCurrentState() == State.Active)
                    {
                        if (lastState == State.Inactive)
                        {
                            lastState = State.Active;

                            if (_command.IsRunning)
                                _command.Stop();
                            else
                                _command.Start();
                        }
                    }
                    else
                        lastState = State.Inactive;
                }

                private ToggleWhenActive(Func<bool> buttonState, ICommand command)
                    : base(buttonState, command)
                { }
            }

            public sealed class CancelWhenActive : Button
            {
                public static void Link(Func<bool> buttonState, ICommand command)
                {
                    new CancelWhenActive(buttonState, command);
                }

                public override void Execute()
                {
                    if (GetCurrentState() == State.Active)
                    {
                        if (lastState == State.Inactive)
                        {
                            lastState = State.Active;
                            _command.Stop();
                        }
                    }
                    else
                        lastState = State.Inactive;
                }

                private CancelWhenActive(Func<bool> buttonState, ICommand command)
                    : base(buttonState, command)
                { }
            }





            abstract public void Execute();

            protected void Start()
            {
                Scheduler.Instance.AddButton(this);
            }

            protected State lastState;
            protected readonly ICommand _command;

            protected Button(Func<bool> stateGetter, ICommand command)
            {
                getTriggerState += stateGetter;

                if (command is Command)
                    _command = new Command((Command)command);
                else if (command is CommandGroup)
                    //_command = new CommandGroup((CommandGroup)command);
                    throw new NotImplementedException();

                lastState = GetCurrentState();
            }

            protected State GetCurrentState()
            {
                return (getTriggerState?.Invoke() ?? false) ? State.Active : State.Inactive;
            }

            protected enum State { Active, Inactive }

            private readonly Func<bool> getTriggerState;
        }

        public static Scheduler Instance => _instance.Value;


        /// <summary>
        /// Register a 
        /// </summary>
        /// <param name="subsystem"></param>
        public void RegisterSubsystem(ISubsystem subsystem)
        {
            if (subsystem != null)
                _subsystems.Add(subsystem);
        }

        private void AddButton(Button button)
        {
            Console.WriteLine(string.Format("Added button ({0}", button));
        }

        public void Run()
        {
            // Build the command list to run this iteration
            var runList = _subsystems.Select(x => x.ActiveCommand)
                                    .Where(x => x != null)
                                    .Concat(_orphanList)
                                    .ToList();

            // Get any inputs

            // Run all commands
            foreach (var active in runList)
                active.Run();
        }


        HashSet<ISubsystem> IScheduler.Subsystems => new HashSet<ISubsystem>(_subsystems);

        void IScheduler.Start(ICommand command)
        {
            // See if there's a required subsystem
            if (command.Required != null)
            {
                if (!_subsystems.Contains(command.Required))
                    throw new InvalidOperationException(string.Format("Command \"{0}\" has a subsystem \"{1}\" that is not registered with the scheduler", command.ToString(), command.Required.ToString()));

                // If the command can run, need to stop any commands on required subsystems
                if (command.Required.IsRunnable(command))
                    command.Required.SetActiveCommand(command);
            }
            else
                AddOrphanCommand(command);
        }



        private static readonly Lazy<Scheduler> _instance = new Lazy<Scheduler>(() => new Scheduler());
        private Scheduler() { }

        // Commands that aren't running on a specific subsystem
        private HashSet<ICommand> _orphanList = new HashSet<ICommand>();

        // The subsystems registered with the scheduler
        private List<ISubsystem> _subsystems = new List<ISubsystem>();

        /// <summary>
        /// Adds a command to the orphan list so it can be run by the scheduler
        /// </summary>
        /// <param name="command">Command to add</param>
        private void AddOrphanCommand(ICommand command)
        {
            if (!_orphanList.Contains(command))
            {
                // Since orphan commands aren't tied to a subsystem, add it to the orphan list
                _orphanList.Add(command);
                command.OnEnd += OrphanComplete;
            }
        }

        /// <summary>
        /// This handler will remove the command from the orphan list when it
        /// completes
        /// </summary>
        private void OrphanComplete(ICommand command)
        {
            _orphanList.Remove(command);
        }
    }
}
