using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for GroupYearByProfit.xaml
    /// </summary>
    public partial class GroupYearByProfit : UserControl
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
        public GroupYearByProfit()
        {
            InitializeComponent();
        }
    }
}
