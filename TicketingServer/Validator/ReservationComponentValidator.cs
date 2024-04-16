using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator.Validator;

namespace TicketingServer.Validator
{
    internal class ReservationComponentValidator : GenericValidator<KomponentaRezervacije>
    {
        public override List<string> Validate(KomponentaRezervacije obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Objekat ne sme biti null");

            var errorMessages = new List<string>();

            var messages = ValidateDouble(obj.UkupanIznos);
            if (messages != null)
                errorMessages.Add(messages);

            //messages = ValidateLength(obj.Prezime, 4);
            //if (messages != null)
            //    errorMessages.Add(messages);

            //messages = ValidateLength(obj.Jmbg, 13);
            //if (messages != null)
            //    errorMessages.Add(messages);

            //messages = ValidateEmail(obj.Email);
            //if (messages != null)
            //    errorMessages.Add(messages);

            //messages = ValidateDateBefore(obj.DatumRodjenja);
            //if (messages != null)
            //    errorMessages.Add(messages);

            return errorMessages;
        }
    }
}
