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
            var searchBox = (d as NavigationCommandSelector).restAutoCompleteBox;
            searchBox.DataContext = e.NewValue;
        }

        private bool inFocusScope;

        /// <summary>
        /// Initialize selector
        /// </summary>
        public NavigationCommandSelector()
        {
            InitializeComponent();

            restAutoCompleteBox.FilteringBehavior = new NoFilterRadAutoCompleteBehavior();

            GotFocus += (s, e) => 
            {
                if (!inFocusScope)
                {
                    inFocusScope = true;
                    restAutoCompleteBox.Focus();
                    inFocusScope = false;
                }
            };

            GotKeyboardFocus += (s, e) =>
            {
                if (!inFocusScope)
                {
                    inFocusScope = true;
                    Keyboard.Focus(restAutoCompleteBox);
                    inFocusScope = false;
                }
            };
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
