using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicketingServer.Validator.Validator
{
    public abstract class GenericValidator<T> where T : class
    {
        public abstract List<string> Validate(T obj);

        public string ValidateLength(string obj, int length, int maxLength = 0)
        {
            if (string.IsNullOrWhiteSpace(obj))
                return "Morate proslediti vrednost za ime";

            if (obj.ToString().Length < length)
                return "Polje mora imati "+ length +" karaktera";

            if (maxLength > 0 && obj.ToString().Length > maxLength)
                return "Polje mora imati manje od " + maxLength + " karaktera";

            return null;
        }
        public string ValidateDateAfter(object value)
        {
            if (DateTime.TryParse(value?.ToString(), out DateTime date))
            {
                if (date < DateTime.Now)
                    return "Datum ne sme biti u proslosti!";
            }
            else
            {
                return "Datum nije u ispravnom formatu!";
            }
            return null;
        }
        public string ValidateDateBefore(object value)
        {
            if (DateTime.TryParse(value?.ToString(), out DateTime date))
            {
                if (date > DateTime.Now)
                    return "Datum ne sme biti u buducnosti!";
            }
            else
            {
                return "Datum nije u ispravnom formatu!";
            }
            return null;
        }
        public string ValidateDouble(object value)
        {
            if (!double.TryParse(value.ToString(), out double cena))
            {
                return "Polje mora sadrzati samo brojeve!";
            }
            else
            {
                if (cena < 0) return "Cena ne sme biti negativna!";
            }
            return null;
        }
        public string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Polje za email ne sme biti prazno!";

            Regex regex = new Regex(@"^\S+@\S+\.\S+$");

            if (regex.IsMatch(email) == false)
                return "Email nije u dobrom formatu!";
            return null;
        }
    }
}
