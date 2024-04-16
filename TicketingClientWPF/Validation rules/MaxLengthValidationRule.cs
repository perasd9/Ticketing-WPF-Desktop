using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TicketingClientWPF.Validation_rules
{
    internal class MaxLengthValidationRule : ValidationRule
    {
        public int Length { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value?.ToString().Length > Length)
                return new ValidationResult(false, $"Polje moze imati maksimum {Length} karakter/a");
            return new ValidationResult(true, null);
        }
    }
}
