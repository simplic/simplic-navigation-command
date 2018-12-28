﻿using Simplic.Icon;
using Simplic.UI.MVC;
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
        public string DisplayName { get => $"{Command.Name} ({Command.Command})"; }

        /// <summary>
        /// Gets the current command instance
        /// </summary>
        public NavigationCommand Command { get; }

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
        /// Gets or sets the order number
        /// </summary>
        public int OrderNr { get; set; }
    }
}
