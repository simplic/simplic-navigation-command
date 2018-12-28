using CommonServiceLocator;
using Simplic.Icon;
using Simplic.UI.MVC;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace Simplic.Navigation.Command.UI
{
    /// <summary>
    /// Navigation command selector configuration
    /// </summary>
    public class NavigationCommandSelectorConfiguration : ViewModelBase
    {
        private readonly INavigationCommandService service;
        private readonly INavigationCommandSearchService searchService;
        private readonly IIconService iconService;
        private string searchText;

        /// <summary>
        /// Initialize configuration
        /// </summary>
        public NavigationCommandSelectorConfiguration()
        {
            service = ServiceLocator.Current.GetInstance<INavigationCommandService>();
            searchService = ServiceLocator.Current.GetInstance<INavigationCommandSearchService>();
            iconService = ServiceLocator.Current.GetInstance<IIconService>();

            AvailableCommands = new List<NavigationCommand>();

            Items = new ObservableCollection<NavigationCommandItem>();

            ItemsViewSource = new CollectionViewSource();
            ItemsViewSource.Source = Items;
            ItemsViewSource.Filter += (s, e) =>
            {
                var item = e.Item as NavigationCommandItem;
                e.Accepted = AvailableCommands.Contains(item.Command);
            };

            // Set order index
            ItemsViewSource.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(NavigationCommandItem.OrderNr), System.ComponentModel.ListSortDirection.Ascending));
        }

        /// <summary>
        /// Reset the order number
        /// </summary>
        private void ResetItemOrderNumber()
        {
            foreach (var command in Items)
                command.OrderNr = 99999;
        }

        /// <summary>
        /// Gets or sets the available items
        /// </summary>
        public ObservableCollection<NavigationCommandItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the items view source of <see cref="NavigationCommandItem"/>
        /// </summary>
        public CollectionViewSource ItemsViewSource { get; set; }

        /// <summary>
        /// Gets or sets the currently available commands
        /// </summary>
        public IList<NavigationCommand> AvailableCommands { get; set; }

        /// <summary>
        /// Gets or sets the search text
        /// </summary>
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    Items.Clear();
                }
                else
                {
                    // Search
                    AvailableCommands = searchService.Search(searchText).Select(x => x.Command).ToList();

                    // Add available items which will be filtered later.
                    if (Items.Count == 0)
                    {
                        foreach (var command in AvailableCommands)
                            Items.Add(new NavigationCommandItem(command, iconService) { Parent = this });
                    }

                    ResetItemOrderNumber();

                    // Sort items
                    int i = 0;
                    foreach (var command in AvailableCommands)
                    {
                        var item = Items.FirstOrDefault(x => x.Command == command);
                        if (item != null)
                            item.OrderNr = i;
                        i++;
                    }
                }

                // Refresh view
                ItemsViewSource.View.Refresh();
                RaisePropertyChanged(nameof(SearchText));
            }
        }
    }
}
