using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALPower
    {
        static HALPower()
        {
            HAL.Initialize();
        }

        public delegate double HAL_GetVinVoltageDelegate(ref int status);
        public static HAL_GetVinVoltageDelegate HAL_GetVinVoltage;

        public delegate double HAL_GetVinCurrentDelegate(ref int status);
        public static HAL_GetVinCurrentDelegate HAL_GetVinCurrent;

        public delegate double HAL_GetUserVoltage6VDelegate(ref int status);
        public static HAL_GetUserVoltage6VDelegate HAL_GetUserVoltage6V;

        public delegate double HAL_GetUserCurrent6VDelegate(ref int status);
        public static HAL_GetUserCurrent6VDelegate HAL_GetUserCurrent6V;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetUserActive6VDelegate(ref int status);
        public static HAL_GetUserActive6VDelegate HAL_GetUserActive6V;

        public delegate int HAL_GetUserCurrentFaults6VDelegate(ref int status);
        public static HAL_GetUserCurrentFaults6VDelegate HAL_GetUserCurrentFaults6V;

        public delegate double HAL_GetUserVoltage5VDelegate(ref int status);
        public static HAL_GetUserVoltage5VDelegate HAL_GetUserVoltage5V;

        public delegate double HAL_GetUserCurrent5VDelegate(ref int status);
        public static HAL_GetUserCurrent5VDelegate HAL_GetUserCurrent5V;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetUserActive5VDelegate(ref int status);
        public static HAL_GetUserActive5VDelegate HAL_GetUserActive5V;

        public delegate int HAL_GetUserCurrentFaults5VDelegate(ref int status);
        public static HAL_GetUserCurrentFaults5VDelegate HAL_GetUserCurrentFaults5V;

        public delegate double HAL_GetUserVoltage3V3Delegate(ref int status);
        public static HAL_GetUserVoltage3V3Delegate HAL_GetUserVoltage3V3;

        public delegate double HAL_GetUserCurrent3V3Delegate(ref int status);
        public static HAL_GetUserCurrent3V3Delegate HAL_GetUserCurrent3V3;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetUserActive3V3Delegate(ref int status);
        public static HAL_GetUserActive3V3Delegate HAL_GetUserActive3V3;

        public delegate int HAL_GetUserCurrentFaults3V3Delegate(ref int status);
        public static HAL_GetUserCurrentFaults3V3Delegate HAL_GetUserCurrentFaults3V3;
    }
}

