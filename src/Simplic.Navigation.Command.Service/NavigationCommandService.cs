using System.Collections.Generic;

namespace Simplic.Navigation.Command.Service
{
    /// <summary>
    /// Command service
    /// </summary>
    public class NavigationCommandService : INavigationCommandService
    {
        private List<NavigationCommand> commands = new List<NavigationCommand>();

        /// <summary>
        /// Register a command
        /// </summary>
        /// <param name="command">Command to register</param>
        public void AddCommand(NavigationCommand command)
        {
            commands.Add(command);
        }

        /// <summary>
        /// Remove a command
        /// </summary>
        /// <param name="command">Command to remove</param>
        public void RemoveCommand(NavigationCommand command)
        {
            if (commands.Contains(command))
                commands.Remove(command);
        }

        /// <summary>
        /// Get all commands
        /// </summary>
        public IReadOnlyList<NavigationCommand> Commands
        {
            get { return commands; }
        }
    }
}
