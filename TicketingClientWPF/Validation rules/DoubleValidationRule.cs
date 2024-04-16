using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicketingClientWPF.Validation_rules
{
    internal class DoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!double.TryParse(value.ToString(), out double cena))
            {
                return new ValidationResult(false, "Polje mora sadrzati samo brojeve!");
            }
            else
            {
                if (cena < 0) return new ValidationResult(false, "Cena ne sme biti negativna!");
                return new ValidationResult(true, null);

            }
        }
    }
}
