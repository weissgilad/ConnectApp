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
    /// Interaction logic for RemoveContractWindow.xaml
    /// </summary>
    public partial class RemoveContractWindow : Window
    {
        IBL bl;
        public RemoveContractWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();

            comboBox.ItemsSource = bl.GetContractList();
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveContract((comboBox.SelectedItem as Contract).ContractId);
                MessageBox.Show("Removed Successfully");
            }
            catch
            {
                MessageBox.Show("Select a contract first");
            }
            comboBox.ItemsSource = bl.GetContractList();
            comboBox.Items.Refresh();
        }


        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
