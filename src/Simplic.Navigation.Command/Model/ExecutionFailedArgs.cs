using System;
using System.Collections.Generic;

namespace Simplic.Navigation.Command
{
    /// <summary>
    /// Command execution failed
    /// </summary>
    public class ExecutionFailedArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the command
        /// </summary>
        public NavigationCommand Command { get; set; }

        /// <summary>
        /// Gets or sets the arguments
        /// </summary>
        public IList<string> Arguments { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
