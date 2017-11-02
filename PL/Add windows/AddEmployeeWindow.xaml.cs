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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        Employee employee;
        IBL bl;

        public AddEmployeeWindow()
        {
            InitializeComponent();
            employee = new Employee();
            DataContext = employee;
            bl = FactoryBL.GetBL();
            bankcomboBox_Copy.ItemsSource = bl.GetAccountList();
            educcomboBox.ItemsSource = Enum.GetValues(typeof(Degree));
            speccomboBox_Copy.ItemsSource = bl.GetSpecializationList();
            speccomboBox_Copy.SelectionChanged += SpeccomboBox_Copy_SelectionChanged;
            
        }

        private void SpeccomboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employee.Specialty = (speccomboBox_Copy.SelectedItem as Specialization).Id; //easier than binding
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddEmployee(employee);
                MessageBox.Show("added successfully"); //will not reach if exception is thrown
                employee = new BE.Employee();
                this.DataContext = employee;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
