using System;

namespace Simplic.Navigation.Command
{
    /// <summary>
    /// Represents a navigation command
    /// </summary>
    public class NavigationCommand
    {
        /// <summary>
        /// Gets or sets the command id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the command name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or sets the command / shortcut
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets whether the command requires arguments
        /// </summary>
        public bool HasArguments { get; set; }

        /// <summary>
        /// Gets or sets the icon id
        /// </summary>
        public Guid IconId { get; set; }
    }
}
