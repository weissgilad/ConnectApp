﻿using System;
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
    /// Interaction logic for RemoveEmployeeWindow.xaml
    /// </summary>
    public partial class RemoveEmployeeWindow : Window
    {
        IBL bl;
        public RemoveEmployeeWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();

            comboBox.ItemsSource = bl.GetEmployeeList();
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveEmployee((comboBox.SelectedItem as Employee).Id);
                MessageBox.Show("Removed Successfully");
            }
            catch
            {
                MessageBox.Show("Select Someone first");
            }
            comboBox.ItemsSource = bl.GetEmployeeList();
            comboBox.Items.Refresh();
        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
