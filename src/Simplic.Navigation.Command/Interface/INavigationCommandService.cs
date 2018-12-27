using System.Collections.Generic;

namespace Simplic.Navigation.Command
{
    /// <summary>
    /// Execute command event handler
    /// </summary>
    /// <param name="sender">Sender instance</param>
    /// <param name="args">Command parameter</param>
    public delegate void ExecuteCommandEventHandler(object sender, ExecuteCommandEventArgs args);

    /// <summary>
    /// Command service
    /// </summary>
    public interface INavigationCommandService
    {
        /// <summary>
        /// Execute command event
        /// </summary>
        event ExecuteCommandEventHandler ExecuteCommand;

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
        /// Execute command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <param name="arguments">Command arguments</param>
        /// <returns>Execution result</returns>
        ExecuteCommandResult Execute(NavigationCommand command, IList<string> arguments);

        /// <summary>
        /// Get all commands
        /// </summary>
        IReadOnlyList<NavigationCommand> Commands { get; }
    }
}
