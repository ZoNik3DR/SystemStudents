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
            int year;
            if (!int.TryParse((string)value, out year))
            {
                return new ValidationResult(false, "Cannot parse");
            }
            if ((year < 2010) || (year > DateTime.Now.Year))
            {
                return new ValidationResult(false, "Out of range");
            }
            return new ValidationResult(true, null);
        }

    }
}
