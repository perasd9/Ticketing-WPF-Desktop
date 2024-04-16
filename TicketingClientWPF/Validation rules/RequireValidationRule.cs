using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicketingClientWPF.Validation_rules
{
    internal class RequireValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length == 0)
                return new ValidationResult(false, "Polje je obavezno");
            return new ValidationResult(true, null);
        }
    }
}
