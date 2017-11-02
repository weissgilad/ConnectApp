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
    /// Interaction logic for EmployeeRemovePage.xaml
    /// </summary>
    public partial class EmployeeRemovePage : UserControl
    {
        BE.Employee employee;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public EmployeeRemovePage()
        {
            InitializeComponent();
            EmployeeList.ItemsSource = bl.GetEmployeeList();
            employee = new BE.Employee();
            DataContext = employee;
            EmployeeList.SelectionChanged += EmployeeList_LoadForEdit;
            
 
            this.Loaded += UpdateLists;
        }

        private void EmployeeList_LoadForEdit(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (EmployeeList.SelectedItem != null)
                {
                    employee = (BE.Employee)(bl.GetEmployee((EmployeeList.SelectedItem as BE.Employee).Id));
                    DataContext = employee;
                }
            }
            catch (Exception ex)
            {
                StatusLabelText = ex.Message;
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
            }
        }

        /// <summary>
        /// make sure everything is up to date
        /// </summary>
        private void UpdateLists(object sender, RoutedEventArgs e)
        {
            EmployeeList.ItemsSource = bl.GetEmployeeList();
            if (TextId.Content!=null && !string.IsNullOrEmpty(TextId.Content.ToString()))
                employee = (BE.Employee)(bl.GetEmployee(TextId.Content.ToString()));
            DataContext = null;
            DataContext = employee;
        }

        /// <summary>
        /// check everything is ok and send to be removed
        /// </summary>
        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextId.Content == null)
                    throw BE.Exceptions.No_Person_Selected_Exception;
                bl.RemoveEmployee(employee.Id);
                EmployeeList.ItemsSource = bl.GetEmployeeList();
                employee = new BE.Employee();
                DataContext = employee;
                StatusLabelText = "Removed successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);

                if (bl.GetEmployeeList().Count > 10) ComboEmployeeList.Visibility = Visibility.Visible;
                else ComboEmployeeList.Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                StatusLabelText = ex.Message;
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);

            }

        }
    }
}
