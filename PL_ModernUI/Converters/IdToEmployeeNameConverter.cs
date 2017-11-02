using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BL;
using BE;
using System.Globalization;

namespace PL_ModernUI
{
    class IdToEmployeeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IBL bl = FactoryBL.GetBL();
            if (!string.IsNullOrEmpty((string)value))
            {
                BE.Employee tmp = bl.GetEmployee((string)value);
                return tmp.FirstName + " " + tmp.LastName;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as BE.Employee).Id;
        }
    }
}
