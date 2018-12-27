using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Navigation.Command
{
    /// <summary>
    /// Command service
    /// </summary>
    public interface INavigationCommandService
    {
        /// <summary>
        /// Register a command
        /// </summary>
        /// <param name="command">Command to register</param>
        void AddCommand(NavigationCommand command);

        /// <summary>
        /// Remove a command
        /// </summary>
        /// <param name="command">Command to remove</param>
        void RemoveCommand(NavigationCommand command);

        /// <summary>
        /// Get all commands
        /// </summary>
        IReadOnlyList<NavigationCommand> Commands { get; }
    }
}
