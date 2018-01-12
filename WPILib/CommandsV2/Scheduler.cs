using System;
using System.Collections.Generic;
using System.Linq;

namespace WPILib.CommandsV2
{
    /// <summary>
    /// The scheduler is one of the core components for command-based programming.By registering<see cref="Subsystem"/> s
    /// and adding <see cref="Trigger"/>s, the scheduler will determine what commands need to be run upon each iteration
    /// of the robot. 
    /// </summary>
    public sealed class Scheduler : IScheduler
    {
        /// <summary>
        /// The scheduler singleton instance
        /// </summary>
        public static Scheduler Instance => _instance.Value;

        /// <summary>
        /// Add a system to scheduler. Subsystems not registered will not have commands scheduled.
        /// </summary>
        /// <param name="subsystem">The subsystem to register</param>
        public void RegisterSubsystem(ISubsystem subsystem)
        {
            if (subsystem != null)
                _subsystems.Add(subsystem);
        }

        public void AddTrigger(Trigger trigger)
        {
            _triggers.Add(trigger);
            Console.WriteLine(string.Format("Added trigger ({0})", trigger));
        }

        /// <summary>
        /// Runs a single iteration of the scheduler execution loop. The loop will determine what commands
        /// are waiting to be run, from the registered subsystems and from the orphan command list (commands
        /// without a subsystem)
        /// </summary>
        public void Run()
        {
            // Get any inputs, in reverse so first item will 'win'
            foreach (var trigger in _triggers.Reverse())
                trigger.Execute();

            // Build the command list to run this iteration
            // 1) Grab from each registered subsystem the active command
            //      (if there's no command, but a default exists, that will be used. Otherwise, it will be null)
            // 2) Concatenate the commands that do not have a subsystem (orphan)
            var runList = _subsystems.Select(x => x.ActiveCommand)
                                     .Where(x => x != null)
                                     .Concat(_orphanList)
                                     .ToList();


            // Commands should run in the following order:
            // 1) Commands
            // 2) Command Groups
            // 3) Command Sequences

            // This will allow sequences and groups to finish when the last command does, rather
            // than waiting for another iteration

            foreach (var active in SortCommandTypes(runList))
                active.Run();
        }

        public void SetTimer(ITimer timer)
        {
            if (Timer == null)
                Timer = timer;
        }

        /// <summary>
        /// A timer for commands to use from timeout and duration
        /// </summary>
        public ITimer Timer { get; private set; }

        HashSet<ISubsystem> IScheduler.Subsystems => new HashSet<ISubsystem>(_subsystems);

        /// <summary>
        /// Performs the work of starting a command. For commands with no subsystem, just add it to the orphan list
        /// For commands with a subsystem, ensure that the subsystem has been register. Then if the command can be
        /// run, set it as the active command on the subsystem for <see cref="Run"/> to use
        /// </summary>
        /// <param name="command">The command to start</param>
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
                // If a command has no required subsystem, then it's an orphan
                AddOrphanCommand(command);
        }

        private static readonly Lazy<Scheduler> _instance = new Lazy<Scheduler>(() => new Scheduler());
        private Scheduler() { }

        // Commands that aren't running on a specific subsystem are orphans, and need to be tracked
        private HashSet<ICommand> _orphanList = new HashSet<ICommand>();

        // The subsystems registered with the scheduler
        private readonly IList<ISubsystem> _subsystems = new List<ISubsystem>();

        // Triggers for the schedulers
        private readonly IList<Trigger> _triggers = new List<Trigger>();

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
            command.OnEnd -= OrphanComplete;
        }

        private IList<ICommand> SortCommandTypes(IList<ICommand> list)
        {
            var commands = list.OfType<Command>().Cast<ICommand>().ToArray();
            var groups = list.OfType<CommandGroup>().Cast<ICommand>().ToArray();
            var sequences = list.OfType<CommandSequence>().Cast<ICommand>().ToArray();
            var others = list.Except(commands).Except(groups).Except(sequences).Cast<ICommand>().ToArray();

            return commands.Concat(others).Concat(groups).Concat(sequences).ToList().AsReadOnly(); ;
        }
    }
}
