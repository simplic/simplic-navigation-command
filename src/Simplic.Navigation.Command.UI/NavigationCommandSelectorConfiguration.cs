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

            AvailableCommands = new List<NavigationCommandSearchResult>();

            Items = new ObservableCollection<NavigationCommandItem>();

            ItemsViewSource = new CollectionViewSource();
            ItemsViewSource.Source = Items;
            ItemsViewSource.Filter += (s, e) =>
            {
                var item = e.Item as NavigationCommandItem;
                e.Accepted = AvailableCommands.Any(x => x.Command == item.Command);
            };

            // Set order index
            ItemsViewSource.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(NavigationCommandItem.OrderNr), System.ComponentModel.ListSortDirection.Ascending));
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
        public IList<NavigationCommandSearchResult> AvailableCommands { get; set; }

        /// <summary>
        /// Gets or sets the search text
        /// </summary>
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Items.Clear();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    // Search
                    AvailableCommands = searchService.Search(searchText);

                    // Add available items which will be filtered later.
                    foreach (var command in AvailableCommands)
                        Items.Add(new NavigationCommandItem(command.Command, iconService) { Parent = this });
                                        
                    // Sort items
                    int i = 0;
                    foreach (var command in AvailableCommands)
                    {
                        var item = Items.FirstOrDefault(x => x.Command == command.Command);
                        item.Arguments = command.Arguments;

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
