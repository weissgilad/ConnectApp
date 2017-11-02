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
    /// Interaction logic for EmployerEditPage.xaml
    /// </summary>
    public partial class EmployerEditPage : UserControl
    {
        BE.Employer employer;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public EmployerEditPage()
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

            ComboSpec.ItemsSource = Enum.GetValues(typeof(SpecType));
            

            this.Loaded += UpdateLists;




        }


        private void ComboEmployeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).Name == "ComboEmployeeList")
                EmployerList.SelectedItem = (sender as ComboBox).SelectedItem;
            else
                CompanyList.SelectedItem = (sender as ComboBox).SelectedItem;
        }

        /// <summary>
        /// show details of selected item
        /// </summary>
        private void EmployeeList_LoadForEdit(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((sender as ListView).SelectedItem != null)
                {
                    employer = (BE.Employer)(bl.GetEmployer(((sender as ListView).SelectedItem as BE.Employer).Id).Clone());
                    DataContext = null;
                    DataContext = employer;
                    EmployerList.ItemsSource = bl.GetPrivateEmployerList();
                    CompanyList.ItemsSource = bl.GetCompanyList();
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
        /// checks all is ok and sends to bl to edit
        /// then resets everything
        /// </summary>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextId.Text))
                    throw Exceptions.No_Person_Selected_Exception;
                if (bl.GetEmployer(TextId.Text).Equals(employer))
                    throw Exceptions.No_Change_Exception;

                if (!employer.IsCompany && (string.IsNullOrWhiteSpace(employer.FirstName) || string.IsNullOrWhiteSpace(employer.LastName)))
                    throw Exceptions.First_Or_Last_Name_Is_Missing_Exception;

                if (employer.IsCompany && string.IsNullOrWhiteSpace(employer.CompanyName))
                    throw Exceptions.Name_Is_Missing_Exception;

                if (string.IsNullOrWhiteSpace(employer.Address))
                    throw Exceptions.Address_Is_Missing_Exception;

                bl.EditEmployer(TextId.Text, employer);

                StatusLabelText = "Edit successful";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);

                EmployerList.ItemsSource = bl.GetPrivateEmployerList();
                CompanyList.ItemsSource = bl.GetCompanyList();

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
            if (CompanyList.Items.Count > 10) ComboCompanyList.Visibility = Visibility.Visible;
            else ComboCompanyList.Visibility = Visibility.Collapsed;

            if (EmployerList.Items.Count > 10) ComboEmployerList.Visibility = Visibility.Visible;
            else ComboEmployerList.Visibility = Visibility.Collapsed;

            EmployerList.ItemsSource = bl.GetPrivateEmployerList();
            CompanyList.ItemsSource = bl.GetCompanyList();

        }
    }
}
