using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicketingClientWPF.Validation_rules
{
    internal class DateAfterValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(DateTime.TryParse(value?.ToString(), out DateTime date))
            {
                if (date < DateTime.Now)
                    return new ValidationResult(false, "Datum ne sme biti u proslosti!");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Datum nije u ispravnom formatu!");
            }
        }
    }
}
