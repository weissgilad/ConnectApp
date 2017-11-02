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
using System.Windows.Shapes;
using BE;
using BL;

enum Choice
{
    By_Salary,
    By_Age,
    By_Specialization_Area,
    Year_By_Profit,
    Profit_By_Year

}

namespace PL
{

    /// <summary>
    /// Interaction logic for GroupingWindow.xaml
    /// </summary>
    public partial class GroupingWindow : Window
    {
        IBL bl;
        public GroupingWindow()
        {
            InitializeComponent();
            comboBox.ItemsSource = Enum.GetValues(typeof(Choice));
            bl = FactoryBL.GetBL();
        }

        private void Groupbutton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
                MessageBox.Show("Please select a grouping method");
            else
            {
                switch ((Choice)comboBox.SelectedItem)
                {
                    case Choice.By_Salary:
                        object uc = new GroupBySalary();
                        (uc as GroupBySalary).Source = bl.GroupBySalary(checkBox.IsChecked == true);
                        page.Content = uc;
                        break;

                    case Choice.By_Age:
                        uc = new GroupByAge();
                        (uc as GroupByAge).Source = bl.GroupByAgeGroup(checkBox.IsChecked == true);
                        page.Content = uc;
                        break;

                    case Choice.By_Specialization_Area:
                        uc = new GroupBySpec();
                        (uc as GroupBySpec).Source = bl.GroupBySpecializationArea(checkBox.IsChecked == true);
                        page.Content = uc;
                        break;

                    case Choice.Year_By_Profit:
                        uc = new GroupYearByProfit();
                        (uc as GroupYearByProfit).Source = bl.GroupByYearlyProfit(checkBox.IsChecked == true);
                        page.Content = uc;
                        break;
                }
            }

        }
    }
}

