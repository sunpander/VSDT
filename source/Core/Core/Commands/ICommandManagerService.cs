using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.Commands
{
    public interface ICommandManagerService
    {
        /// <summary>
        /// Registers the command.
        /// </summary>
        /// <param name="command">The command.</param>
        void RegisterCommand(VSMenuCommand command);
        /// <summary>
        /// Uns the register command.
        /// </summary>
        /// <param name="command">The command.</param>
        void UnRegisterCommand(VSMenuCommand command);
        /// <summary>
        /// Gets the registered commands.
        /// </summary>
        /// <returns></returns>
        List<VSMenuCommand> GetRegisteredCommands();
    } 
}
