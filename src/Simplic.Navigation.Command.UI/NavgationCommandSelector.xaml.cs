using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simplic.Navigation.Command.UI
{
    /// <summary>
    /// Interaction logic for NavgationCommandSelector.xaml
    /// </summary>
    public partial class NavgationCommandSelector : UserControl
    {
        /// <summary>
        /// Configuration viewmodel
        /// </summary>
        public static readonly DependencyProperty SelectorConfigurationProperty =
            DependencyProperty.Register("SelectorConfiguration", typeof(NavigationCommandSelectorConfiguration), typeof(NavgationCommandSelector), new PropertyMetadata(0));

        /// <summary>
        /// Initialize selector
        /// </summary>
        public NavgationCommandSelector()
        {
            InitializeComponent();
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
