using System;

namespace WPILib.CommandsV2
{
    /// <summary>
    /// This class represents a logical robot subsystem
    /// </summary>
    public class Subsystem : ISubsystem, IEquatable<Subsystem>
    {
        /// <summary>
        /// This gets the command currently running on this subsystem
        /// </summary>
        public ICommand ActiveCommand => _userCommand ?? _defaultCommand;

        public string Name { get; }

        public Subsystem(string name)
        {
            _id = EntityId.Generate();
            Name = name;
        }

        /// <summary>
        /// Sets a given action to be the default to execute on the subsystem when no
        /// other command is active
        /// </summary>
        /// <param name="defaultAction">Action to perform as the default</param>
        public void SetDefaultAction(Action defaultAction)
        {
            if (_defaultCommand != null)
                throw new InvalidOperationException(string.Format("{0} default command already set", Name));

            if (defaultAction == null)
                throw new ArgumentNullException("defaultAction", "can't be null");

            // Create a default command. The command by definition can never complete
            var defaultCommand = new Command(this, Name + " default");
            defaultCommand.OnExecute += cmd => defaultAction();
            defaultCommand.IsFinished += () => false;

            _defaultCommand = defaultCommand;
        }

        void ISubsystem.SetActiveCommand(ICommand command)
        {
            if (((ISubsystem)this).IsRunnable(command))
            {
                ((ISubsystem)this).StopActiveCommand();

                _userCommand = command;
                _userCommand.OnEnd += CommandEnded;
            }
        }

        /// <summary>
        /// Stop the active command
        /// </summary>
        void ISubsystem.StopActiveCommand()
        {
            ActiveCommand?.Stop();
        }

        /// <summary>
        /// Determine if the command 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool ISubsystem.IsRunnable(ICommand command)
        {
            // If the argument is non-null and requires this subsystem,
            // then it's runnable if the active command is is interruptible or there is no active command
            if (command != null && command.Required == this)
                return ActiveCommand?.IsInterruptible ?? true;

            return false;
        }


        public bool Equals(Subsystem other)
        {
            if (ReferenceEquals(null, other))
                return false;

            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Subsystem);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} (ID {1})", Name, (int)_id);
        }


        private readonly EntityId _id;
        private Command _defaultCommand;
        private ICommand _userCommand;


        /// <summary>
        /// Handler to run when a command completes
        /// </summary>
        private void CommandEnded(ICommand command)
        {
            _userCommand.OnEnd -= CommandEnded;
            _userCommand = null;
        }
    }
}
