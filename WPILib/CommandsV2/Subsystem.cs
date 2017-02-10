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

        public void SetDefaultAction(Action defaultAction)
        {
            if (_defaultCommand != null)
                throw new InvalidOperationException(string.Format("{0} default command already set", Name));

            // Create a default command. The command by definition never completed
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
            //return command != null &&
            //       command.Required == this &&
            //       true;
            //  command != (Command)((ISubsystem)this).ActiveCommand


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
