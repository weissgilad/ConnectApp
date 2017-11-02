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
    /// Interaction logic for RemoveEmployerWindow.xaml
    /// </summary>
    public partial class RemoveEmployerWindow : Window
    {
        IBL bl;

        public RemoveEmployerWindow()
        {

            InitializeComponent();
            bl = FactoryBL.GetBL();

            comboBox.ItemsSource = bl.GetEmployerList();
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveEmployer((comboBox.SelectedItem as Employer).Id);
                MessageBox.Show("Removed Successfully");
            }
            catch
            {
                MessageBox.Show("Select Someone first");
            }
            comboBox.ItemsSource = bl.GetEmployerList();
            comboBox.Items.Refresh();
        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
