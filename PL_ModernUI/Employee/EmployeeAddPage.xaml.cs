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
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace PL_ModernUI.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeAddPage.xaml
    /// </summary>
    public partial class EmployeeAddPage : UserControl
    {
        private readonly BackgroundWorker Worker = new BackgroundWorker();
        BE.Employee employee;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        IEnumerable<IGrouping<string, IGrouping<string, BankAccount>>> Banks;
        public EmployeeAddPage()
        {
            InitializeComponent();

            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.RunWorkerAsync();
            prog.IsVisibleChanged += Prog_IsVisibleChanged;

            employee = new BE.Employee();
            EmployeeDetails.DataContext = employee;
            EmployeeList.ItemsSource = bl.GetEmployeeList();

            ComboEdu.ItemsSource = Enum.GetValues(typeof(Degree));
            ComboSpec.ItemsSource = bl.GetSpecializationList();
            this.Loaded += UpdateLists;

        }

        /// <summary>
        /// progress visibility will change when its fade-out animation is complete
        /// </summary>
        private void Prog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (prog.Visibility == Visibility.Collapsed)
                Animations.FadeIn(BankInfo);
        }

        /// <summary>
        /// when worker has successfully downloaded banks
        /// </summary>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ComboBank.ItemsSource = Banks;
            ComboBank.DisplayMemberPath = "Key";
            Animations.FadeOut(prog);//start progress fade-out animation
        }


        /// <summary>
        /// method will be in a seperate thread using background worker<para/>
        /// try to get bank info. will fail if download is already in progress, or if there is no internet access.
        /// method will retry every 500ms 25 times.<para/>
        /// after 25 times, will print error message, wait 2sec and start over (25 times etc)
        /// </summary>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while (Banks == null) //if download is complete, banks will point to it
            {
                try
                {
                    Banks = bl.GroupBankByNameAndCity(); //will succeed if download is not already in progress
                }
                catch
                {
                    if (i > 25)
                    {
                        Dispatcher.Invoke(() => //so thread can access controls
                            {
                                StatusLabelText = "There seems to be a problem with the network.\n\t\t   Retrying...";
                                SuccessLabel.DataContext = StatusLabelText;
                                Animations.OpacityAnimation(SuccessLabel);
                            }
                        );
                        i = 0;
                        Thread.Sleep(2000); //wait 2 sec
                    }
                    Thread.Sleep(500); //wait 0.5 sec
                    i++;
                }
            }
        }

        /// <summary>
        /// checks all is ok and sends to bl to add
        /// then resets everything
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(TextId.Text))
                    throw Exceptions.No_Person_Selected_Exception;
                
                if (string.IsNullOrWhiteSpace(employee.FirstName) || string.IsNullOrWhiteSpace(employee.LastName))
                    throw Exceptions.First_Or_Last_Name_Is_Missing_Exception;

                if (string.IsNullOrWhiteSpace(employee.Address))
                    throw Exceptions.Address_Is_Missing_Exception;

                if (ComboAddress.SelectedItem == null)
                    throw new Exception("Choose a bank");

                bl.AddEmployee(employee);

                ComboBank.SelectedItem = null;

                StatusLabelText = "Added successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);

                EmployeeList.ItemsSource = bl.GetEmployeeList();

                employee = new BE.Employee();
                EmployeeDetails.DataContext = employee;

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

            if (ComboSpec.SelectedIndex != -1)
            {
                BE.Specialization tmp = ComboSpec.SelectedItem as BE.Specialization;
                ComboSpec.Items.Refresh();
                ComboSpec.ItemsSource = bl.GetSpecializationList();
                ComboSpec.SelectedIndex = bl.GetSpecializationList().FindIndex(spec => spec.Id == tmp.Id);
            }
            else ComboSpec.ItemsSource = bl.GetSpecializationList();
        }

    }
}
