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
    /// Interaction logic for SpecializationEditPage.xaml
    /// </summary>
    public partial class SpecializationEditPage : UserControl
    {
        BE.Specialization spec;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";

        public SpecializationEditPage()
        {
            InitializeComponent();

            SpecList.ItemsSource = bl.GetSpecializationList();

            spec = new BE.Specialization();
            DataContext = spec;

            SpecList.SelectionChanged += SpecList_LoadForEdit;
            ComboSpecList.SelectionChanged += ComboSpecList_SelectionChanged;

            ComboSpec.ItemsSource = Enum.GetValues(typeof(SpecType));

            this.Loaded += UpdateLists;

            TextMin.PreviewMouseDoubleClick += TextBoxSelectAll;
            TextMax.PreviewMouseDoubleClick += TextBoxSelectAll;
        }

        /// <summary>
        /// intuitively select all on double click
        /// </summary>
        private void TextBoxSelectAll(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void ComboSpecList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpecList.SelectedItem = ComboSpecList.SelectedItem;
        }

        /// <summary>
        /// show details of selected item
        /// </summary>
        private void SpecList_LoadForEdit(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SpecList.SelectedItem != null)
                {
                    spec = (BE.Specialization)(bl.GetSpecialization((SpecList.SelectedItem as BE.Specialization).Id).Clone());
                    DataContext = null;
                    DataContext = spec;
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
                int tmp;
                int.TryParse(TextId.Text, out tmp);
                if (tmp == 0)
                    throw Exceptions.No_Item_Selected_Exception;

                if (string.IsNullOrWhiteSpace(spec.SpecialtyName))
                    throw Exceptions.Name_Is_Missing_Exception;

                if (bl.GetSpecialization(tmp).Equals(spec))
                    throw Exceptions.No_Change_Exception;

                bl.EditSpecialty(tmp, spec);

                StatusLabelText = "Edit successful";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);

                SpecList.ItemsSource = bl.GetSpecializationList();
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
        }
    }
}
