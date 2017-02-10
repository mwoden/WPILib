using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.CommandsV2
{
    public interface IMultiCommand : ICommand
    {
        /// <summary>
        /// Represents a set of distinct requirements for commands in a group or sequencec
        /// </summary>
        IEnumerable<ISubsystem> Requirements { get; }
    }
}
