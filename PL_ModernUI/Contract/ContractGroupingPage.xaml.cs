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
using BL;
using BE;

namespace PL_ModernUI.Contract
{
    /// <summary>
    /// Interaction logic for ContractGroupingPage.xaml
    /// </summary>
    public partial class ContractGroupingPage : UserControl
    {
        IBL bl = FactoryBL.GetBL();
        public ContractGroupingPage()
        {
            InitializeComponent();
            GroupingList.Items.Add("Years by profit");
            GroupingList.Items.Add("Profit by year");
            GroupingList.Items.Add("Specialization area");

            GroupingList.SelectionChanged += Grouping_Initiate;
            SortBox.Click += Grouping_Initiate; // give user the illusion of control

            Loaded += Grouping_Initiate; // keep updated
        }


        private void Grouping_Initiate(object sender, RoutedEventArgs e)
        {

            if (GroupingList.SelectedItem != null)
            {
                switch ((string)GroupingList.SelectedItem)
                {

                    case "Years by profit":
                        GroupingType.Content = (string)GroupingList.SelectedItem; //for range (ex. "{binding}-{binding+100}") datatrigger
                        lvUsers.ItemsSource = bl.GroupByYearlyProfit(SortBox.IsChecked == true);
                        break;

                    case "Profit by year":
                        GroupingType.Content = (string)GroupingList.SelectedItem;
                        lvUsers.ItemsSource = bl.GroupProfitYearly(SortBox.IsChecked == true);
                        break;

                    case "Specialization area":
                        GroupingType.Content = (string)GroupingList.SelectedItem;
                        lvUsers.ItemsSource = bl.GroupBySpecializationArea(SortBox.IsChecked == true);
                        break;
                }
            }

        }
    }
}
