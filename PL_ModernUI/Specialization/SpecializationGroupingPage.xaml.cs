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

namespace PL_ModernUI.Specialization
{
    /// <summary>
    /// Interaction logic for SpecializationGroupingPage.xaml
    /// </summary>
    public partial class SpecializationGroupingPage : UserControl
    {
            IBL bl;
        public SpecializationGroupingPage()
        {
            InitializeComponent();
            GroupingList.Items.Add("Average gross wage");
            GroupingList.Items.Add("Amount of specialists");
            bl = FactoryBL.GetBL();
            GroupingList.SelectionChanged += Grouping_Initiate;
            SortBox.Click += Grouping_Initiate;
        }

        private void Grouping_Initiate(object sender, RoutedEventArgs e)
        {

            if (GroupingList.SelectedItem != null)
            {
                switch ((string)GroupingList.SelectedItem)
                {
                    case "Average gross wage":
                        GroupingType.Content = (string)GroupingList.SelectedItem; //for range (ex. "{binding}-{binding+100}") datatrigger
                        lvUsers.ItemsSource = bl.GroupSpecializationByAverageGrossWage(SortBox.IsChecked == true);
                        break;

                    case "Amount of specialists":
                        GroupingType.Content = (string)GroupingList.SelectedItem;
                        lvUsers.ItemsSource = bl.GroupSpecializationByNumPeople(SortBox.IsChecked == true);
                        break;

                }
            }

        }
    }
}
