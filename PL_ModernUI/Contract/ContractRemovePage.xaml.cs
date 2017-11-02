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
using BE;
using BL;
using FirstFloor.ModernUI.Windows.Controls;

namespace PL_ModernUI.Contract
{
    /// <summary>
    /// Interaction logic for ContractRemovePage.xaml
    /// </summary>
    public partial class ContractRemovePage : UserControl
    {
        BE.Contract contract;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public ContractRemovePage()
        {

            InitializeComponent();
            ContractList.ItemsSource = bl.GetContractList();
            contract = new BE.Contract();
            DataContext = contract;
            ContractList.SelectionChanged += ContractList_LoadForEdit;
            ComboContractList.SelectionChanged += ComboContractList_SelectionChanged;
            
            this.Loaded += UpdateLists;



        }


        private void ComboContractList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContractList.SelectedItem = ComboContractList.SelectedItem;
        }

        private void ContractList_LoadForEdit(object sender, SelectionChangedEventArgs e)
        {
            if (ContractList.SelectedItem != null)
            {
                contract = (BE.Contract)(bl.GetContract((ContractList.SelectedItem as BE.Contract).ContractId).Clone());
                DataContext = contract;
            }
        }

        /// <summary>
        /// check everything is ok and send to be removed
        /// </summary>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (TextId.Text == "0")
                    throw Exceptions.No_Item_Selected_Exception;
                bl.RemoveContract(int.Parse(TextId.Text));
                DataContext = null;
                StatusLabelText = "Removed successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
                ContractList.ItemsSource = bl.GetContractList();
            }
            catch (Exception ex)
            {
                StatusLabelText = ex.Message;
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
            }
        }

        /// <summary>
        /// make sure everything is up to date
        /// </summary>
        private void UpdateLists(object sender, RoutedEventArgs e)
        {
            ContractList.ItemsSource = bl.GetContractList();

            if (ContractList.Items.Count > 10) ComboContractList.Visibility = Visibility.Visible;
            else ComboContractList.Visibility = Visibility.Collapsed;

            if (ContractList.SelectedItem != null)
                contract = (BE.Contract)(bl.GetContract((ContractList.SelectedItem as BE.Contract).ContractId).Clone());
            DataContext = contract;
        }
    }
}
