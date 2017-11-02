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

namespace PL_ModernUI.Specialization
{
    /// <summary>
    /// Interaction logic for SpecializationRemovePage.xaml
    /// </summary>
    public partial class SpecializationRemovePage : UserControl
    {
            BE.Specialization spec;
            IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public SpecializationRemovePage()
        {
        
            InitializeComponent();
            SpecList.ItemsSource = bl.GetSpecializationList();
            spec = new BE.Specialization();
            DataContext = spec;
            SpecList.SelectionChanged += SpecList_LoadForEdit;
            ComboSpecList.SelectionChanged += ComboSpecList_SelectionChanged;

            this.Loaded += UpdateLists;



        }


        private void ComboSpecList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpecList.SelectedItem = ComboSpecList.SelectedItem;
        }

        private void SpecList_LoadForEdit(object sender, SelectionChangedEventArgs e)
        {
            if (SpecList.SelectedItem != null)
            {
                spec = (BE.Specialization)(bl.GetSpecialization((SpecList.SelectedItem as BE.Specialization).Id).Clone());
                DataContext = spec;
            }
        }

        /// <summary>
        /// check everything is ok and send to be removed
        /// </summary>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if ((int)TextId.Content == 0)
                    throw BE.Exceptions.No_Item_Selected_Exception;
                bl.RemoveSpecialty((int)TextId.Content);
                StatusLabelText = "Removed successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
                SpecList.ItemsSource = bl.GetSpecializationList();
                spec = new BE.Specialization();
                DataContext = spec;
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
            SpecList.ItemsSource = bl.GetSpecializationList();

            if (SpecList.Items.Count > 10) ComboSpecList.Visibility = Visibility.Visible;
            else ComboSpecList.Visibility = Visibility.Collapsed;

            if (SpecList.SelectedItem != null)
                spec = (BE.Specialization)(bl.GetSpecialization((SpecList.SelectedItem as BE.Specialization).Id).Clone());
            DataContext = spec;
        }
    }
}
