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
    /// Interaction logic for AddSpecializationWindow.xaml
    /// </summary>
    public partial class AddSpecializationWindow : Window
    {
        Specialization specialization;
        IBL bl;

        public AddSpecializationWindow()
        {
            InitializeComponent();
            specialization = new Specialization();
            this.DataContext = specialization;
            bl = FactoryBL.GetBL();
            this.AreaCombobox.ItemsSource = Enum.GetValues(typeof(SpecType));
        }

        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddSpecialty(specialization);
                MessageBox.Show("added successfully"); //wont reach if exception is thrown
                specialization = new Specialization();
                this.DataContext = specialization;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
