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
    class IdToEmployerNameConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IBL bl = FactoryBL.GetBL();
            if (!string.IsNullOrEmpty((string)value))
            {
                BE.Employer tmp = bl.GetEmployer((string)value);
                if (tmp.IsCompany)
                    return tmp.CompanyName;
                return tmp.FirstName +" "+ tmp.LastName;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as BE.Employer).Id;
        }
    }
}
