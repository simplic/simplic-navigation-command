using Simplic.UI.MVC;

namespace Simplic.Navigation.Command.UI
{
    /// <summary>
    /// Command item
    /// </summary>
    public class NavigationCommandItem : ViewModelBase
    {
        /// <summary>
        /// Initialize command
        /// </summary>
        /// <param name="command"></param>
        public NavigationCommandItem(NavigationCommand command)
        {
            this.Command = command;
        }

        /// <summary>
        /// Get the display name
        /// </summary>
        public string DisplayName { get => $"{Command.Name} ({Command.Command})"; }

        /// <summary>
        /// Gets the current command instance
        /// </summary>
        public NavigationCommand Command { get; }

        /// <summary>
        /// Gets or sets the order number
        /// </summary>
        public int OrderNr { get; set; }
    }
}
