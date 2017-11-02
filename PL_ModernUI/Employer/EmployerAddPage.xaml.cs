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
using BE;
using BL;

namespace PL_ModernUI.Employer
{
    /// <summary>
    /// Interaction logic for EmployerAddPage.xaml
    /// </summary>
    public partial class EmployerAddPage : UserControl
    {
        BE.Employer employer;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public EmployerAddPage()
        {

            InitializeComponent();


            employer = new BE.Employer();
            EmployerDetails.DataContext = employer;
            EmployerList.ItemsSource = bl.GetPrivateEmployerList();
            CompanyList.ItemsSource = bl.GetCompanyList();
            bl = FactoryBL.GetBL();
            ComboSpec.ItemsSource = Enum.GetValues(typeof(SpecType));
            this.Loaded += UpdateLists;

        }

        /// <summary>
        /// checks all is ok and sends to bl to add
        /// then resets everything
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!employer.IsCompany && (string.IsNullOrWhiteSpace(employer.FirstName) || string.IsNullOrWhiteSpace(employer.LastName)))
                    throw Exceptions.First_Or_Last_Name_Is_Missing_Exception;

                if (employer.IsCompany && string.IsNullOrWhiteSpace(employer.CompanyName))
                    throw Exceptions.Name_Is_Missing_Exception;

                if(string.IsNullOrWhiteSpace( employer.Address))
                    throw Exceptions.Address_Is_Missing_Exception;


                bl.AddEmployer(employer);
                StatusLabelText = "Added successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
                EmployerList.ItemsSource = bl.GetPrivateEmployerList();
                CompanyList.ItemsSource = bl.GetCompanyList();
                employer = new BE.Employer();
                EmployerDetails.DataContext = employer;

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
            EmployerList.ItemsSource = bl.GetPrivateEmployerList();
            CompanyList.ItemsSource = bl.GetCompanyList();
        }
    }
}
