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
    /// Interaction logic for EditContractWindow.xaml
    /// </summary>
    public partial class EditContractWindow : Window
    {
        IBL bl;
        Contract contract;
        public EditContractWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();
            contract = new Contract();
            DataContext = contract;
            idtextBox.ItemsSource = bl.GetContractList();

            idtextBox.DropDownClosed += IdtextBox_LoadForEdit;
        }

        private void IdtextBox_LoadForEdit(object sender, EventArgs e)
        {
            try
            {
                contract = (Contract)bl.GetContract((idtextBox.SelectedItem as Contract).ContractId).Clone();
                DataContext = contract;
            }
            catch { }
        }

        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                bl.EditContract(contract.ContractId, contract);
                MessageBox.Show("Edited Successfully");
                contract = new Contract();
                DataContext = contract;

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
