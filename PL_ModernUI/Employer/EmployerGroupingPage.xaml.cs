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
using FirstFloor.ModernUI.Windows.Controls;
using BL;
using BE;

namespace PL_ModernUI.Employer
{
    /// <summary>
    /// Interaction logic for EmployerGroupingPage.xaml
    /// </summary>
    public partial class EmployerGroupingPage : UserControl
    {
        IBL bl;
        public EmployerGroupingPage()
        {
            InitializeComponent();
            GroupingList.Items.Add("Specialization area");
            GroupingList.Items.Add("Number of signed contracts");
            bl = FactoryBL.GetBL();
            GroupingList.SelectionChanged += Grouping_Initiate;
            SortBox.Click += Grouping_Initiate;
            Loaded += Grouping_Initiate;
        }

        private void Grouping_Initiate(object sender, RoutedEventArgs e)
        {

            if (GroupingList.SelectedItem != null)
            {
                switch ((string)GroupingList.SelectedItem)
                {
                    case "Specialization area":
                        lvUsers.ItemsSource = bl.GroupEmployerBySpecializationArea(SortBox.IsChecked == true);
                        break;

                    case "Number of signed contracts":
                        lvUsers.ItemsSource = bl.GroupEmployerByNumContracts(SortBox.IsChecked == true);
                        break;

                }
            }

        }
    }
}
