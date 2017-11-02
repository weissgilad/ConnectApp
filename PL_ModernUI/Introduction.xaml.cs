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
using FirstFloor.ModernUI.Presentation;

namespace PL_ModernUI
{
    /// <summary>
    /// Interaction logic for Introduction.xaml
    /// </summary>
    public partial class Introduction : UserControl
    {
        public Introduction()
        {
            InitializeComponent();
            LightButton.Click += Theme_SelectionChanged;
            DarkButton.Click += Theme_SelectionChanged;
            AppearanceManager.Current.AccentColor = Colors.DarkCyan;
        }

        private void Theme_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (LightButton.IsChecked == true)
                AppearanceManager.Current.ThemeSource = AppearanceManager.LightThemeSource;
            else
            {
                AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
            }
        }
    }
}
