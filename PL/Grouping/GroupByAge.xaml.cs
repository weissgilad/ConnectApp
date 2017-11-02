using System;
using System.Collections.Generic;
using System.Collections;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for GroupByAge.xaml
    /// </summary>
    public partial class GroupByAge : UserControl
    {
        private IEnumerable source;

        public IEnumerable Source
        {
            get { return source; }
            set
            {
                source = value;
                lvUsers.ItemsSource = source;
            }
        }
        public GroupByAge()
        {
            InitializeComponent();
        }
    }
}
