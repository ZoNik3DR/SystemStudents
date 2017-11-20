using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ManagementSystemStudents.Converters
{
    class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) //source to property
        {
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string date = (string)value;
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return value = result.Year;
            else return int.Parse(date);
        }
    }
}
