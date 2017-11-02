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
    /// Interaction logic for AddContractWindow.xaml
    /// </summary>
    public partial class AddContractWindow : Window
    {
        IBL bl;
        Contract contract;
        public AddContractWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();
            contract = new Contract();
            DataContext = contract;
        }

        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddContract(contract);
                MessageBox.Show("added successfully"); //will not reach if exception is thrown
                contract = new Contract();
                DataContext = contract; //to show blank contract

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Closebutton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
