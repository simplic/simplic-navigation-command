namespace Simplic.Navigation.Command
{
    /// <summary>
    /// Execution result
    /// </summary>
    public class ExecuteCommandResult
    {
        /// <summary>
        /// Gets or sets whether the execution was successful
        /// </summary>
        public bool ExecutionFailed { get; set; }

        /// <summary>
        /// Gets or sets the error message text if the execution fails
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the execute command
        /// </summary>
        public NavigationCommand Command { get; private set; }
    }
}
