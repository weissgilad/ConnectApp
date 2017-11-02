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
    class IdToSpecializationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            IBL bl = FactoryBL.GetBL();
            if ((int)value != 0)
                return bl.GetSpecialization((int)value);
            else return null;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (value as BE.Specialization).Id;
            else return 0;
        }
    }
}
