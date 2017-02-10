namespace WPILib.CommandsV2
{
    public interface ISubsystem
    {
        string Name { get; }

        ICommand ActiveCommand { get; }
        void SetActiveCommand(ICommand command);
        void StopActiveCommand();
        bool IsRunnable(ICommand command);
    }
}
