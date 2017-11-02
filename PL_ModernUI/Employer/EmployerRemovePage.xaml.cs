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
    /// Interaction logic for EmployerRemovePage.xaml
    /// </summary>
    public partial class EmployerRemovePage : UserControl
    {
        BE.Employer employer;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public EmployerRemovePage()
        {
            InitializeComponent();
            EmployerList.ItemsSource = bl.GetPrivateEmployerList();
            CompanyList.ItemsSource = bl.GetCompanyList();

            employer = new BE.Employer();
            DataContext = employer;

            EmployerList.SelectionChanged += EmployeeList_LoadForEdit;
            ComboEmployerList.SelectionChanged += ComboEmployeeList_SelectionChanged;
            CompanyList.SelectionChanged += EmployeeList_LoadForEdit;
            ComboCompanyList.SelectionChanged += ComboEmployeeList_SelectionChanged;

           

            this.Loaded += UpdateLists;

        }


        private void ComboEmployeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).Name == "ComboEmployeeList")
                EmployerList.SelectedItem = (sender as ComboBox).SelectedItem;
            else
                CompanyList.SelectedItem = (sender as ComboBox).SelectedItem;
        }

        private void EmployeeList_LoadForEdit(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                employer = (BE.Employer)(bl.GetEmployer(((sender as ListView).SelectedItem as BE.Employer).Id).Clone());
                DataContext = employer;
                EmployerList.ItemsSource = bl.GetPrivateEmployerList();
                CompanyList.ItemsSource = bl.GetCompanyList();
            }
            catch { }
        }

        /// <summary>
        /// check everything is ok and send to be removed
        /// </summary>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextId.Content == null)
                    throw Exceptions.No_Person_Selected_Exception;
                bl.RemoveEmployer(employer.Id);
                StatusLabelText = "Removed successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);

                EmployerList.ItemsSource = bl.GetPrivateEmployerList();
                CompanyList.ItemsSource = bl.GetCompanyList();
                employer = new BE.Employer();
                DataContext = employer;
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
            if (!(string.IsNullOrEmpty((string)TextId.Content)))
            {
                employer = (BE.Employer)(bl.GetEmployer((string)TextId.Content).Clone());
                DataContext = employer;
            }

            if (CompanyList.Items.Count > 10) ComboCompanyList.Visibility = Visibility.Visible;
            else ComboCompanyList.Visibility = Visibility.Collapsed;

            if (EmployerList.Items.Count > 10) ComboEmployerList.Visibility = Visibility.Visible;
            else ComboEmployerList.Visibility = Visibility.Collapsed;

            EmployerList.ItemsSource = bl.GetPrivateEmployerList();
            CompanyList.ItemsSource = bl.GetCompanyList();
        }
    }
}
