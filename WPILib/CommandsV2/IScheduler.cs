using System.Collections.Generic;

namespace WPILib.CommandsV2
{
    public interface IScheduler
    {
        HashSet<ISubsystem> Subsystems { get; }
        void Run();
        void RegisterSubsystem(ISubsystem subsystem);
        void Start(ICommand command);
    }
}
