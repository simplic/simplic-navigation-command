using Simplic.Icon;
using Simplic.UI.MVC;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Simplic.Navigation.Command.UI
{
    /// <summary>
    /// Command item
    /// </summary>
    public class NavigationCommandItem : ViewModelBase
    {
        private BitmapImage iconImage;
        private IIconService iconService;

        /// <summary>
        /// Initialize command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="iconService"></param>
        public NavigationCommandItem(NavigationCommand command, IIconService iconService)
        {
            this.Command = command;
            this.iconService = iconService;
        }

        /// <summary>
        /// Get the display name
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Command.Command))
                    return Command.Name;

                return $"{Command.Name} ({Command.Command})";
            }
        }

        /// <summary>
        /// Gets the current command instance
        /// </summary>
        public NavigationCommand Command { get; }

        /// <summary>
        /// Gets or sets the command arguments
        /// </summary>
        public IList<string> Arguments { get; set; }

        /// <summary>
        /// Gets the icon image
        /// </summary>
        public BitmapImage IconImage
        {
            get
            {
                if (iconImage == null)
                {
                    iconImage = iconService.GetByIdAsImage(Command.IconId);
                }

                return iconImage;
            }
        }

        /// <summary>
        /// Gets the parameter visibility
        /// </summary>
        public Visibility ParameterImageVisibility
        {
            get
            {
                return Command.HasArguments ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Gets or sets the order number
        /// </summary>
        public int OrderNr { get; set; }
    }
}
