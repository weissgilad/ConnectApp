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
    /// Interaction logic for EditEmployerWindow.xaml
    /// </summary>
    public partial class EditEmployerWindow : Window
    {
            Employer employer;
            IBL bl;

        public EditEmployerWindow()
        {
            InitializeComponent();
            employer = new Employer();
            DataContext = employer;
            bl = FactoryBL.GetBL();
            CompanyTextBox.Visibility = CompanyLabel.Visibility = Visibility.Hidden;
            checkBox.Click += CheckBox_Click;

            speccomboBox_Copy.ItemsSource = Enum.GetValues(typeof(SpecType));
            speccomboBox_Copy.SelectionChanged += SpeccomboBox_Copy_SelectionChanged;
            idtextBox.LostFocus += IdtextBox_LoadForEdit;

        }

        private void IdtextBox_LoadForEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                employer = (Employer)bl.GetEmployer(idtextBox.Text).Clone();
                DataContext = employer;
                if (checkBox.IsChecked == true) CompanyTextBox.Visibility = CompanyLabel.Visibility = Visibility.Visible;
                else CompanyTextBox.Visibility = CompanyLabel.Visibility = Visibility.Hidden;
                idtextBox.Foreground = Brushes.Black;
                idlabel.Foreground = Brushes.Black;
                speccomboBox_Copy.SelectedIndex = (int)employer.Specialty;

            }
            catch
            {
                employer = new Employer();
                string tmp = idtextBox.Text;
                DataContext = employer;
                CompanyTextBox.Visibility = CompanyLabel.Visibility = Visibility.Hidden;
                idtextBox.Text = tmp;
                idtextBox.Foreground = Brushes.Red;
                idlabel.Foreground = Brushes.Red;
            }
        }
        private void SpeccomboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employer.Specialty = (SpecType)speccomboBox_Copy.SelectedItem;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true) CompanyTextBox.Visibility = CompanyLabel.Visibility = Visibility.Visible;
            else CompanyTextBox.Visibility = CompanyLabel.Visibility = Visibility.Hidden;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.EditEmployer(employer.Id,employer);
                MessageBox.Show("Edit successfull");
                CompanyTextBox.Visibility = CompanyLabel.Visibility = Visibility.Hidden;
                employer = new BE.Employer();
                DataContext = employer;

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
