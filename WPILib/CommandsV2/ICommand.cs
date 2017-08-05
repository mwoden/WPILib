using System;

namespace WPILib.CommandsV2
{
    /// <summary>
    /// The <typeparamref name="ICommand"/> is the basic building block of a command-based robot
    /// 
    /// A command started has a lifetime that goes through the states of Initializing, Running, and Ending. After a command
    /// ends, it is ready to be started again.
    /// 
    /// The ICommand interface defines events that occur throughout a commands lifetime. Each event is a delgate that accepts
    /// a single parameter (the ICommand) and returns nothing.
    /// 
    /// A callback is provide to all externally specifying when a command is done.
    /// 
    /// A command can end in two different ways.
    /// 1) IsFinished returns true, in which case the command has completed. 
    /// 2) The command is interrupted,
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Event raised when the command is initialized
        /// This only occurs when the is scheduled, and will not run again until the command is re-started
        /// </summary>
        event Action<ICommand> OnInitialize;

        /// <summary>
        /// Even raised when the command is executed by the scheduler
        /// This is run every time the scheduler loop runs
        /// </summary>
        event Action<ICommand> OnExecute;

        /// <summary>
        /// Event raised when the command completes
        /// This condition occurs when <see cref="IsFinished"/> returns true
        /// </summary>
        event Action<ICommand> OnComplete;

        /// <summary>
        /// Event raised when the command is interrupted
        /// This condition occurs when command is stopped, most likely by another command being run
        /// </summary>
        event Action<ICommand> OnInterrupt;

        /// <summary>
        /// Event raised when the command end
        /// This event is run regardless of how the command ends, Completed or Interrupted
        /// </summary>
        event Action<ICommand> OnEnd;

        /// <summary>
        /// Callback to determine whether the command is finished it's work
        /// </summary>
        Func<bool> IsFinished { set; }

        /// <summary>
        /// A string to indentify the command
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The amount of time the command took to run it's most recent execution
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Whether or not the command can be interrupted by another command for the same subsystem
        /// </summary>
        bool IsInterruptible { get; }

        /// <summary>
        /// Whether or not the command is active in the scheduler
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// The subsystem required for the command (if there is one)
        /// </summary>
        ISubsystem Required { get; }

        /// <summary>
        /// A unique ID for the command
        /// </summary>
        EntityId Id { get; }

        /// <summary>
        /// Sets the command to not be allowed to be interrupted
        /// </summary>
        void SetNonInterruptible();

        /// <summary>
        /// Starts the command be scheduling it to be run. If the subsystem required is available, the command
        /// will begin executing.
        /// </summary>
        void Start();

        /// <summary>
        /// Perform the actions specific for the body of the command
        /// </summary>
        void Run();

        /// <summary>
        /// Stops the command. This will interrupt the command.
        /// </summary>
        void Stop();

        /// <summary>
        /// Creates a copy of the command.
        /// </summary>
        ICommand Duplicate();
    }
}
