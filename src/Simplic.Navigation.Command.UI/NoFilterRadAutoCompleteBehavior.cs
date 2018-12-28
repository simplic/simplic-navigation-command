using System.Collections.Generic;
using Telerik.Windows.Controls;
using System.Linq;

namespace Simplic.Navigation.Command.UI
{
    internal class NoFilterRadAutoCompleteBehavior : IFilteringBehavior
    {
        internal NoFilterRadAutoCompleteBehavior()
        {

        }

        public IEnumerable<object> FindMatchingItems(string searchText, System.Collections.IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
        {
            return items.OfType<NavigationCommandItem>();
        }
    }
}
