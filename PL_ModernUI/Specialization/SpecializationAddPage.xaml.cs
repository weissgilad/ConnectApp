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
    /// Interaction logic for SpecializationAddPage.xaml
    /// </summary>
    public partial class SpecializationAddPage : UserControl
    {
        BE.Specialization spec;
        IBL bl = FactoryBL.GetBL();
        string StatusLabelText = "";
        public SpecializationAddPage()
        {
            InitializeComponent();
            spec = new BE.Specialization();
            SpecDetails.DataContext = spec;

            ComboSpec.ItemsSource = Enum.GetValues(typeof(SpecType));
            SpecList.ItemsSource = bl.GetSpecializationList();
            bl = FactoryBL.GetBL();
            this.Loaded += UpdateLists;
            TextMin.PreviewMouseDoubleClick += TextBoxSelectAll;
            TextMax.PreviewMouseDoubleClick += TextBoxSelectAll;

        }

        /// <summary>
        /// intuitively select all on double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSelectAll(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        /// <summary>
        /// checks all is ok and sends to bl to add
        /// then resets everything
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                bl.AddSpecialty(spec);
                StatusLabelText = "Added successfully";
                SuccessLabel.DataContext = StatusLabelText;
                Animations.OpacityAnimation(SuccessLabel);
                SpecList.ItemsSource = bl.GetSpecializationList(); ;
                spec = new BE.Specialization();
                SpecDetails.DataContext = spec;

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
        }

    }
}
