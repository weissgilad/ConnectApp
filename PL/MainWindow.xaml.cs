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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Window addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Window addEmployerWindow = new AddEmployerWindow();
            addEmployerWindow.ShowDialog();
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Window addContractWindow = new AddContractWindow();
            addContractWindow.ShowDialog();
        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            Window addSpecWindow = new AddSpecializationWindow();
            addSpecWindow.ShowDialog();
        }

        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            Window editEmployeeWindow = new EditEmployeeWindow();
            editEmployeeWindow.ShowDialog();
        }

        private void button_Copy5_Click(object sender, RoutedEventArgs e)
        {
            Window editEmployerWindow = new EditEmployerWindow();
            editEmployerWindow.ShowDialog();
        }

        private void button_Copy6_Click(object sender, RoutedEventArgs e)
        {
            Window editContractWindow = new EditContractWindow();
            editContractWindow.ShowDialog();
        }

        private void button_Copy7_Click(object sender, RoutedEventArgs e)
        {
            Window editSpecializationWindow = new EditSpecializationWindow();
            editSpecializationWindow.ShowDialog();
        }

        private void button_Copy8_Click(object sender, RoutedEventArgs e)
        {
            Window removeEmployeeWindow = new RemoveEmployeeWindow();
            removeEmployeeWindow.ShowDialog();
        }

        private void button_Copy9_Click(object sender, RoutedEventArgs e)
        {
            Window removeEmployerWindow = new RemoveEmployerWindow();
            removeEmployerWindow.ShowDialog();
        }

        private void button_Copy10_Click(object sender, RoutedEventArgs e)
        {
            Window removeContractWindow = new RemoveContractWindow();
            removeContractWindow.ShowDialog();
        }

        private void button_Copy11_Click(object sender, RoutedEventArgs e)
        {
            Window removeSpecializationWindow = new RemoveSpecializationWindow();
            removeSpecializationWindow.ShowDialog();
        }

        private void button_Copy12_Click(object sender, RoutedEventArgs e)
        {
            Window groupingWindow = new GroupingWindow();
            groupingWindow.ShowDialog();
        }
    }
}
