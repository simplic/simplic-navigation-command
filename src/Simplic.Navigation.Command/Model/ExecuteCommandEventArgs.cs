using System;
using System.Collections.Generic;

namespace Simplic.Navigation.Command
{
    /// <summary>
    /// Command executed arguments
    /// </summary>
    public class ExecuteCommandEventArgs : EventArgs
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
        /// Gets or sets the result object
        /// </summary>
        public ExecuteCommandResult Result { get; private set; } = new ExecuteCommandResult();
    }
}
