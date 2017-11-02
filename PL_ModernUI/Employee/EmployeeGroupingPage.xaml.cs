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

namespace PL_ModernUI.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeGroupingPage.xaml
    /// </summary>
    public partial class EmployeeGroupingPage : UserControl
    {
        IBL bl = FactoryBL.GetBL();
        public EmployeeGroupingPage()
        {
            InitializeComponent();
            GroupingList.Items.Add("Salary");
            GroupingList.Items.Add("Age");

            GroupingList.SelectionChanged += Grouping_Initiate;
            SortBox.Click += Grouping_Initiate; //give the illusion the user has control

            Loaded += Grouping_Initiate;//keep everything updated
        }


        private void Grouping_Initiate(object sender, RoutedEventArgs e)
        {

            if (GroupingList.SelectedItem!=null)
            {
                switch ((string)GroupingList.SelectedItem)
                {
                    case "Salary":
                        GroupingType.Content = (string)GroupingList.SelectedItem; //for range (ex. "{binding}-{binding+100}") datatrigger
                        lvUsers.ItemsSource = bl.GroupBySalary(SortBox.IsChecked == true);
                        break;

                    case "Age":
                        GroupingType.Content = (string)GroupingList.SelectedItem;
                        lvUsers.ItemsSource = bl.GroupByAgeGroup(SortBox.IsChecked == true);
                        break;
                }
            }

        }
    }
}
