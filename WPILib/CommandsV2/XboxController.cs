using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.CommandsV2
{
    class XboxController
    {
        public enum Button { A, B, X, Y, LeftBumper, RightBumper, Back, Start, LeftStick, RightStick }

        public Trigger this[Button button] { get { return _buttons[button]; } }

        public XboxController(int port)
        {
            foreach(var button in Enum.GetValues(typeof(Button)).Cast<Button>())
            {
                //_buttons[button] = new Trigger( ()=>  _buttonNumber[button]
            }
        }

        private readonly Dictionary<Button, Trigger> _buttons;

        private static readonly IDictionary<Button, int> _buttonNumber = new Dictionary<Button, int>
        {
            { Button.A, 1 },
            { Button.B, 2 },
            { Button.X, 3 },
            { Button.Y, 4 },
            { Button.LeftBumper, 5 },
            { Button.LeftBumper, 6 },
            { Button.Back, 7 },
            { Button.Start, 8 },
            { Button.LeftStick, 9 },
            { Button.RightStick, 10 },
        };

        //private Joystick stick;
        //public Trigger XButton { get; }

        //public XboxController()
        //{
        //    new Trigger(() => stick.GetButton(Joystick.ButtonType.Top));
        //}
    }
}
