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

namespace PL_ModernUI.Contract
{
    /// <summary>
    /// Interaction logic for ContractAddPage.xaml
    /// </summary>
    public partial class ContractAddPage : UserControl
    {
        BE.Contract contract, EmailContract;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";

        public ContractAddPage()
        {
            InitializeComponent();

            SuccessLabel.DataContext = StatusLabelText;

            comboEmployeeId.ItemsSource = bl.GetEmployeeList();
            comboEmployerId.ItemsSource = bl.GetEmployerList();
            ContractList.ItemsSource = bl.GetContractList();

            contract = new BE.Contract();
            ContractDetails.DataContext = contract;

            contract.StartDate = DateTime.Now;
            contract.EndDate = DateTime.Now + new TimeSpan(365, 0, 0, 0);

            this.Loaded += UpdateLists;
            comboEmployeeId.SelectionChanged += ComboEmployeeId_SelectionChanged;
            PickerStart.SelectedDateChanged += PickerStart_SelectedDateChanged;

            TextGross.PreviewMouseDoubleClick += TextBoxSelectAll;

            ContractDetails.GotFocus += Email_LostFocus;
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Email.IsFocused)
                if (Email.Opacity != 0)
                    Animations.FadeOut(Email);
        }


        /// <summary>
        /// intuitively select all on double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSelectAll(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        /// <summary>
        /// set tooltip for wage textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboEmployeeId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboEmployeeId.SelectedItem != null)
            {
                BE.Specialization tmp = bl.GetSpecialization((comboEmployeeId.SelectedItem as BE.Employee).Specialty);
                TextGross.ToolTip = "Wage for this employee is between " + tmp.MinimumSalary + "-" + tmp.MaximumSalary;
            }
        }

        /// <summary>
        /// add year to whatever is chosen in begin date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            PickerEnd.SelectedDate = PickerStart.SelectedDate + new TimeSpan(365, 0, 0, 0);
        }

        /// <summary>
        /// checks all is ok and sends to bl to add
        /// then resets everything
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                if (comboEmployeeId.SelectedIndex == -1 || comboEmployerId.SelectedIndex == -1)
                    throw Exceptions.Contract_Person_Not_Selected_Exceptions;

                bl.AddContract(contract);
                EmailContract = contract;

                StatusLabelText = "Added successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);

                Animations.FadeIn(Email);
                EmployerEmail.Text = null;
                EmployeeEmail.Text = null;

                ContractList.ItemsSource = bl.GetContractList();
                contract = new BE.Contract();
                contract.StartDate = DateTime.Now;
                contract.EndDate = DateTime.Now + new TimeSpan(365, 0, 0, 0);

                ContractDetails.DataContext = null;
                ContractDetails.DataContext = contract;

                comboEmployeeId.Items.Refresh();
                comboEmployerId.Items.Refresh();
                TextGross.ToolTip = null;
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
            ContractList.ItemsSource = bl.GetContractList();
            if (comboEmployerId.SelectedIndex != -1)
            {
                BE.Employer tmper = comboEmployerId.SelectedItem as BE.Employer;
                comboEmployerId.ItemsSource = bl.GetEmployerList();
                comboEmployerId.Items.Refresh();
                comboEmployerId.SelectedIndex = bl.GetEmployerList().FindIndex(emp => emp.Id == tmper.Id);
            }
            else comboEmployerId.ItemsSource = bl.GetEmployerList();

            if (comboEmployeeId.SelectedIndex != -1)
            {
                BE.Employee tmpee = comboEmployeeId.SelectedItem as BE.Employee;
                comboEmployeeId.ItemsSource = bl.GetEmployeeList();
                comboEmployeeId.Items.Refresh();
                comboEmployeeId.SelectedIndex = bl.GetEmployeeList().FindIndex(emp => emp.Id == tmpee.Id);
            }
            else comboEmployeeId.ItemsSource = bl.GetEmployeeList();

            if (comboEmployeeId.SelectedItem != null)
            {
                BE.Specialization tmp = bl.GetSpecialization((comboEmployeeId.SelectedItem as BE.Employee).Specialty);
                TextGross.ToolTip = "Wage for this employee is between " + tmp.MinimumSalary + "-" + tmp.MaximumSalary;
            }


        }


        /// <summary>
        /// email button
        /// </summary>
        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployerEmail.Text == "" && EmployeeEmail.Text == "")
            {
                StatusLabelText = "No email inputted";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
            }
            else
            {
                Animations.FadeOut(Email);
                Animations.OpacityAnimation(SendingLabel);
                BackgroundWorker Worker = new BackgroundWorker();
                Worker.DoWork += Worker_DoWork;
                Worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// try to send email to requested email addresses
        /// </summary>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while (i < 2) //try 2 times
            {
                try
                {
                    string emper = "";
                    string empee = "";

                    Dispatcher.Invoke(() => //so thread can access controls
                    {
                        emper = EmployerEmail.Text;
                        empee = EmployeeEmail.Text;
                    }
                        );
                    bl.mail("Connect Them All Inc.", EmailContract, emper, empee);
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.Message == "No internet")
                    {
                        Dispatcher.Invoke(() => //so thread can access controls
                        {
                            Animations.FadeOut(SendingLabel);//make it go away
                            StatusLabelText = ex.Message;
                            SuccessLabel.DataContext = StatusLabelText;
                            Animations.OpacityAnimation(SuccessLabel);
                        }
                        );
                        i = 5;
                        break;
                    }

                    Thread.Sleep(500);
                    i++;
                }
            }
            Dispatcher.Invoke(() => //so thread can access controls
            {
                if (i < 2)
                    Animations.OpacityAnimation(EmailSuccessLabel);
                else if (i < 5)
                {
                    StatusLabelText = "something went wrong, sorry. Maybe you are connected to JCT?";
                    SuccessLabel.DataContext = StatusLabelText;
                    Animations.OpacityAnimation(SuccessLabel);
                }
            }
                    );
        }
    }
}
