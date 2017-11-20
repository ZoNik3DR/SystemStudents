using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementSystemStudents.ValidationRules
{
    public class YearRule : ValidationRule
    {

        public YearRule() { }

        public override ValidationResult Validate(object value,
                                                  CultureInfo culture)
        {
            string date = (string)value;
            DateTime result;
            if (DateTime.TryParse(date, out result))
                 value = result.Year;
            else value=int.Parse(date);

            if (((int)value < 2010) || ((int)value > DateTime.Now.Year))
            {
                return new ValidationResult(false, "Out of range");
            }
            return new ValidationResult(true, null);
        }

    }
}
