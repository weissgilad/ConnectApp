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
    /// Interaction logic for GroupBySpec.xaml
    /// </summary>
    public partial class GroupBySpec : UserControl
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
        public GroupBySpec()
        {
            InitializeComponent();
        }
    }
}
