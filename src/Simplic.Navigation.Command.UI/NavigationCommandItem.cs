using Simplic.Icon;
using Simplic.Studio.UI;
using Simplic.UI.MVC;
using System.Collections.Generic;
using System.Diagnostics;
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
            Command = command;
            DocumentationCommand = new RelayCommand(OpenDocumentation);
            this.iconService = iconService;
        }

        private void OpenDocumentation(object param)
        {
            try
            {
                // We need to discuss this
                Process.Start(Command.DocumentationLink);
            }
            catch
            {
                LocalizedMessageBox.Show("documentation_open_failed", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        /// <summary>
        /// Gets whether the documentation "i" is visible
        /// </summary>
        public Visibility DocumentationVisibility { get => string.IsNullOrWhiteSpace(Command.DocumentationLink) ? Visibility.Collapsed : Visibility.Visible; }

        /// <summary>
        /// Gets or sets the command that opens the documentation
        /// </summary>
        public RelayCommand DocumentationCommand { get; set; }
    }
}
