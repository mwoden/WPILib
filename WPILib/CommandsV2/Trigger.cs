using System;

namespace WPILib.CommandsV2
{
    public class Trigger
    {
        /// <summary>
        /// Create the base trigger by providing the trigger condition
        /// </summary>
        /// <param name="trigger">The trigger condition</param>
        /// <remarks></remarks>
        public Trigger(Func<bool> trigger)
        {
            _trigger = trigger;
        }

        /// <summary>
        /// Determine if the trigger is active and execute the command as required
        /// </summary>
        public void Execute()
        {
            _execution?.Invoke();
        }

        public void WhenActive(ICommand command)
        {
            Scheduler.Instance.AddTrigger(new Trigger(this, WhenActiveExecution, command));
        }

        public Trigger WhileActive(ICommand command)
        {
            return new Trigger(this, WhileActiveExecution, command);
        }

        public Trigger WhenInactive(ICommand command)
        {
            return new Trigger(this, WhenInactiveExecution, command);
        }

        public Trigger ToggleWhen(ICommand command)
        {
            return new Trigger(this, ToggleWhenActive, command);
        }

        public Trigger CancelWhen(ICommand command)
        {
            return new Trigger(this, CancelWhenActive, command);
        }

        private readonly Func<bool> _trigger;
        private readonly ICommand _command;
        private readonly Action _execution;

        private enum TriggerState { Active, Inactive }
        private TriggerState _lastState;

        /// <summary>
        /// Indicates the trigger is active, either by the trigger itself or a SmartDashboard button
        /// </summary>
        private TriggerState State => (IsTriggered || IsSmartDashboardActivated) ? TriggerState.Active : TriggerState.Inactive;

        /// <summary>
        /// Indicates whether the condition to active the trigger is asserted
        /// </summary>
        private bool IsTriggered => _trigger?.Invoke() ?? false;

        /// <summary>
        /// Indicates whether the trigger is activated by a SmartDashboard button
        /// </summary>
        private bool IsSmartDashboardActivated => false;

        private Trigger(Trigger initial)
        {
            _trigger = initial._trigger;
        }

        private Trigger(Trigger initial, Action triggerExecution, ICommand command)
            : this(initial)
        {
            // Do NOT duplicate the command. For triggers/buttons, there's a good chance that
            // the user may actually be referring to an existing commands
            _command = command;

            _execution = triggerExecution;

            _lastState = State;
        }

        // Trigger needs to handle the following:
        // When Pressed: Upon trigger going active, start the command once
        // While Held: Upon trigger going active, start the command continuously
        // When Inactive: Upon trigger going inactive, start the command once
        // Toggle When Active: Upon trigger going active, start the command if it's not running, or stop it if it is running
        // Cancel When Active: Upon trigger going active, cancel the command

        private void WhenActiveExecution()
        {
            if (State == TriggerState.Active)
            {
                if (_lastState == TriggerState.Inactive)
                {
                    _lastState = TriggerState.Active;
                    _command.Start();
                }
            }
            else
                _lastState = TriggerState.Inactive;
        }

        private void WhileActiveExecution()
        {
            if (State == TriggerState.Active)
            {
                _lastState = TriggerState.Active;
                _command.Start();
            }
            else
            {
                _lastState = TriggerState.Inactive;
                _command.Stop();
            }
        }

        private void WhenInactiveExecution()
        {
            if (State == TriggerState.Inactive)
            {
                if (_lastState == TriggerState.Active)
                {
                    _lastState = TriggerState.Inactive;
                    _command.Start();
                }
            }
            else
                _lastState = TriggerState.Active;
        }

        private void ToggleWhenActive()
        {
            if (State == TriggerState.Active)
            {
                if (_lastState == TriggerState.Inactive)
                {
                    _lastState = TriggerState.Active;

                    if (_command.IsRunning)
                        _command.Stop();
                    else
                        _command.Start();
                }
            }
            else
                _lastState = TriggerState.Inactive;
        }

        private void CancelWhenActive()
        {
            if (State == TriggerState.Active)
            {
                if (_lastState == TriggerState.Inactive)
                {
                    _lastState = TriggerState.Active;
                    _command.Stop();
                }
            }
            else
                _lastState = TriggerState.Inactive;
        }
    }
}
