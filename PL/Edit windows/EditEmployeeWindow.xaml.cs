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

namespace PL
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        Employee employee;
        IBL bl;

        public EditEmployeeWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();
            employee = new Employee();
            DataContext = employee;
            bankcomboBox_Copy.ItemsSource = bl.GetAccountList();
            educcomboBox.ItemsSource = Enum.GetValues(typeof(Degree));
            speccomboBox_Copy.ItemsSource = bl.GetSpecializationList();
            speccomboBox_Copy.SelectionChanged += SpeccomboBox_Copy_SelectionChanged;
            idtextBox.LostFocus += IdtextBox_LoadForEdit;
        }

        private void IdtextBox_LoadForEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                employee = (Employee)(bl.GetEmployee(idtextBox.Text)).Clone();
                DataContext = employee;
                idtextBox.Foreground = Brushes.Black;
                idlabel.Foreground = Brushes.Black;
                bankcomboBox_Copy.SelectedIndex = employee.Account.BankNum - 1;
                speccomboBox_Copy.SelectedIndex = bl.GetSpecializationList().FindIndex(spec => spec.Id == employee.Specialty);

            }
            catch
            {
                employee = new Employee();
                string tmp = idtextBox.Text;
                DataContext = employee;

                idtextBox.Text = tmp;
                idtextBox.Foreground = Brushes.Red;
                idlabel.Foreground = Brushes.Red;
            }
        }

        private void SpeccomboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employee.Specialty = (speccomboBox_Copy.SelectedItem as Specialization).Id;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.EditEmployee(idtextBox.Text, employee);
                MessageBox.Show("Edit successfull");
                employee = new BE.Employee();
                DataContext = employee;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

