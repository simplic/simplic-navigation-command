using System.Collections.Generic;

namespace Simplic.Navigation.Command.Service
{
    /// <summary>
    /// Command service
    /// </summary>
    public class NavigationCommandService : INavigationCommandService
    {
        /// <summary>
        /// Execute command event
        /// </summary>
        public event ExecuteCommandEventHandler ExecuteCommand;

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
        /// Execute command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <param name="arguments">Command arguments</param>
        /// <returns>Execution result</returns>
        public ExecuteCommandResult Execute(NavigationCommand command, IList<string> arguments)
        {
            var args = new ExecuteCommandEventArgs
            {
                Arguments = arguments ?? new List<string>(),
                Command = command
            };

            ExecuteCommand?.Invoke(this, args);

            return args.Result;
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
