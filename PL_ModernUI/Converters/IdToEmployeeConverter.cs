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
    class IdToEmployeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty((string)value))
                return FactoryBL.GetBL().GetEmployee((string)value);
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BE.Employee)
                return (value as BE.Employee).Id;
            else return null;
        }
    }
}
