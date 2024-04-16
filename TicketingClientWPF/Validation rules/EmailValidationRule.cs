using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicketingClientWPF.Validation_rules
{
    internal class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                MailAddress email = new MailAddress(value?.ToString());

                return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                Debug.WriteLine("---------" + "Email validation argument");
                return new ValidationResult(false, "Polje mora biti u formatu example@example.com");
            }

        }
    }
}
