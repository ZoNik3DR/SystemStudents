using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementSystemStudents.ValidationRules
{

    public class NameRule : ValidationRule
    {
        public NameRule() { }

        public override ValidationResult Validate(object value, CultureInfo culture)
        { 
            string str = (string)value;
            if (str.Length < 3)
                return new ValidationResult(false, "String must has at least 3 symbols");
            foreach (char c in str)
                if (!((Char.IsLetter(c))))
                    return new ValidationResult(false, "String contains prohibited symbols");
            return new ValidationResult(true, null);
        }

    }
}

