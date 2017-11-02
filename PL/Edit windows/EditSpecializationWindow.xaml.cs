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
    /// Interaction logic for EditSpecializationWindow.xaml
    /// </summary>
    public partial class EditSpecializationWindow : Window
    {
        Specialization specialization;
        IBL bl;

        public EditSpecializationWindow()
        {
            InitializeComponent();
            specialization = new Specialization();
            DataContext = specialization;
            bl = FactoryBL.GetBL();
            
            AreaCombobox.ItemsSource = Enum.GetValues(typeof(SpecType));
            IDTextBox_Copy.ItemsSource = bl.GetSpecializationList();
            IDTextBox_Copy.DropDownClosed += IdtextBox_LoadForEdit;
        }

        private void IdtextBox_LoadForEdit(object sender, EventArgs e)
        {
            try
            {
                specialization = (Specialization)bl.GetSpecialization((IDTextBox_Copy.SelectedItem as Specialization).Id).Clone();
                DataContext = specialization;
            }
            catch { }
        }

        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.EditSpecialty(specialization.Id, specialization);
                MessageBox.Show("Edit successfull");
                specialization = new Specialization();
                DataContext = specialization;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
