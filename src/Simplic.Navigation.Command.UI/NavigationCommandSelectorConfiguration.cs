using CommonServiceLocator;

namespace Simplic.Navigation.Command.UI
{
    /// <summary>
    /// Navigation command selector configuration
    /// </summary>
    public class NavigationCommandSelectorConfiguration
    {
        private INavigationCommandService service;
        private INavigationCommandSearchService searchService;

        /// <summary>
        /// Initialize configuration
        /// </summary>
        public NavigationCommandSelectorConfiguration()
        {
            service = ServiceLocator.Current.GetInstance<INavigationCommandService>();
            searchService = ServiceLocator.Current.GetInstance<INavigationCommandSearchService>();
        }
    }
}
