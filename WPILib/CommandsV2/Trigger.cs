using System;

namespace WPILib.CommandsV2
{
    public class Trigger
    {
        public bool IsActivated => TriggerActive?.Invoke() ?? false;
        public event Func<bool> TriggerActive;

        public void WhenActive(Command command)
        {

        }
        private bool lastState;

        private bool GetActiveState()
        {
            //TODO Add NetworkTable support
            return IsActivated || false;
        }
    }
}
