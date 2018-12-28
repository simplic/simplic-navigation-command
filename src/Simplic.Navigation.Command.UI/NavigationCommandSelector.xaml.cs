using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Simplic.Navigation.Command.UI
{
    /// <summary>
    /// Interaction logic for NavgationCommandSelector.xaml
    /// </summary>
    public partial class NavigationCommandSelector : UserControl
    {
        private INavigationCommandService service;

        /// <summary>
        /// Configuration viewmodel
        /// </summary>
        public static readonly DependencyProperty SelectorConfigurationProperty =
            DependencyProperty.Register("SelectorConfiguration", typeof(NavigationCommandSelectorConfiguration), typeof(NavigationCommandSelector), new PropertyMetadata(null, ConfigurationChangedCallback));

        /// <summary>
        /// Configuration changed callback
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void ConfigurationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var searchBox = (d as NavigationCommandSelector).commandAutoCompleteBox;
            searchBox.DataContext = e.NewValue;
        }
        
        private bool inSelectionChanged;

        /// <summary>
        /// Initialize selector
        /// </summary>
        public NavigationCommandSelector()
        {
            InitializeComponent();

            service = CommonServiceLocator.ServiceLocator.Current.GetInstance<INavigationCommandService>();
            commandAutoCompleteBox.FilteringBehavior = new NoFilterRadAutoCompleteBehavior();
        }

        /// <summary>
        /// Override focus
        /// </summary>
        public new void Focus()
        {
            commandAutoCompleteBox.Focus();
            Keyboard.Focus(commandAutoCompleteBox);
        }

        /// <summary>
        /// Selection changed command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!inSelectionChanged)
            {
                inSelectionChanged = true;

                SelectorConfiguration.SearchText = "";
                if (e.AddedItems != null)
                {
                    foreach (var command in e.AddedItems.OfType<NavigationCommandItem>())
                        service.Execute(command.Command, command.Arguments);

                    e.AddedItems.Clear();
                }

                commandAutoCompleteBox.SelectedItem = null;
                commandAutoCompleteBox.SelectedItems = null;
                inSelectionChanged = false;
            }

            e.Handled = true;
        }

        /// <summary>
        /// Gets or sets the configuration instance
        /// </summary>
        public NavigationCommandSelectorConfiguration SelectorConfiguration
        {
            get { return (NavigationCommandSelectorConfiguration)GetValue(SelectorConfigurationProperty); }
            set { SetValue(SelectorConfigurationProperty, value); }
        }
    }
}
