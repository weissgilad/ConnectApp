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
using BL;
using BE;

namespace PL
{
    /// <summary>
    /// Interaction logic for RemoveSpecializationWindow.xaml
    /// </summary>
    public partial class RemoveSpecializationWindow : Window
    {
        IBL bl;
        public RemoveSpecializationWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();

            comboBox.ItemsSource = bl.GetSpecializationList();
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveSpecialty((comboBox.SelectedItem as Specialization).Id);
                MessageBox.Show("Removed Successfully");
            }
            catch
            {
                MessageBox.Show("Select a specialization first");
            }
            comboBox.ItemsSource = bl.GetSpecializationList();
            comboBox.Items.Refresh();
        }
        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
