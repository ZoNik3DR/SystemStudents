using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementSystemStudents.ValidationRules
{
    class MarkRule : ValidationRule
    {
        public MarkRule() { }

        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            if (!int.TryParse((string)value, out int val))
            {
                return new ValidationResult(false, "Cannot parse");
            }
            if (val< 1 || val > 10)
                return new ValidationResult(false, "Invalid range");
            return new ValidationResult(true, null);
        }
    }
}
