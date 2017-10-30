using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementSystemStudents.ValidationRules
{
    class GroupRule : ValidationRule
    {
        public GroupRule() { }

        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            string str = (string)value;
            if(str=="Undefined")
                return new ValidationResult(false, "Set group num");
            if (str.Length < 3)
                return new ValidationResult(false, "String must has at least 3 symbols");
           foreach (char c in str)
                if (!( Char.IsLetterOrDigit(c)))
                    return new ValidationResult(false, "String contains prohibited symbols");
            return new ValidationResult(true, null);
        }

    }
}
