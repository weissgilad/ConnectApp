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
using FirstFloor.ModernUI.Windows.Controls;
using BL;
using BE;

namespace PL_ModernUI.Contract
{
    /// <summary>
    /// Interaction logic for ContractEditPage.xaml
    /// </summary>
    public partial class ContractEditPage : UserControl
    {
        BE.Contract contract;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public ContractEditPage()
        {

            InitializeComponent();

            ContractList.ItemsSource = bl.GetContractList();
            contract = new BE.Contract();
            DataContext = contract;
            ContractList.SelectionChanged += ContractList_LoadForEdit;
            ComboContractList.SelectionChanged += ComboContractList_SelectionChanged;

            this.Loaded += UpdateLists;
            PickerEnd.SelectedDate = DateTime.Now;


        }


        private void ComboContractList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContractList.SelectedItem = ComboContractList.SelectedItem;
        }

        /// <summary>
        /// show details of selected item
        /// </summary>
        private void ContractList_LoadForEdit(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ContractList.SelectedItem != null)
                {
                    contract = (BE.Contract)(bl.GetContract((ContractList.SelectedItem as BE.Contract).ContractId).Clone());
                    DataContext = null;
                    DataContext = contract;
                }
            }
            catch (Exception ex)
            {
                StatusLabelText = ex.Message;
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
            }
        }

        /// <summary>
        /// checks all is ok and sends to bl to edit
        /// then resets everything
        /// </summary>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextId.Text == "0")
                    throw Exceptions.No_Item_Selected_Exception;
                if (bl.GetContract(contract.ContractId).Equals(contract))
                    throw Exceptions.No_Change_Exception;

                bl.EditContract(int.Parse(TextId.Text), contract);

                StatusLabelText = "Edit successful";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
            }
            catch (Exception ex)
            {
                StatusLabelText = ex.Message;
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
            }
            ContractList.ItemsSource = bl.GetContractList();
        }

        /// <summary>
        /// make sure everything is up to date
        /// </summary>
        private void UpdateLists(object sender, RoutedEventArgs e)
        {
            var source = bl.GetContractList();
            ContractList.ItemsSource = source;

            if (ContractList.Items.Count > 10) ComboContractList.Visibility = Visibility.Visible;
            else ComboContractList.Visibility = Visibility.Collapsed;


            if (!source.Exists(c => c.ContractId == int.Parse(TextId.Text)))
            {
                contract = new BE.Contract();
                DataContext = contract;
                PickerEnd.SelectedDate = DateTime.Now;
            }
        }
    }
}
