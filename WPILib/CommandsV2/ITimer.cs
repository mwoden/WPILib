using System;

namespace WPILib.CommandsV2
{
    public interface ITimer
    {
        TimeSpan Now { get; }
    }
}
