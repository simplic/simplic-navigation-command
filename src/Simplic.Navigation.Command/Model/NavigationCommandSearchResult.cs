using System.Collections.Generic;

namespace Simplic.Navigation.Command
{
    /// <summary>
    /// Represents a command search result entry
    /// </summary>
    public class NavigationCommandSearchResult
    {
        /// <summary>
        /// Gets or sets the command
        /// </summary>
        public NavigationCommand Command { get; set; }

        /// <summary>
        /// Gets or sets the arguments
        /// </summary>
        public IList<string> Arguments { get; set; } = new List<string>();
    }
}
