using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator.Validator;

namespace TicketingServer.Validator
{
    internal class ReservationValidator : GenericValidator<Rezervacija>
    {
        public override List<string> Validate(Rezervacija obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Objekat ne sme biti null");

            var errorMessages = new List<string>();

            var messages = ValidateDouble(obj.UkupnaCena);
            if (messages != null)
                errorMessages.Add(messages);

            messages = ValidateDateBefore(obj.DatumRezervacije);
            if (messages != null)
                errorMessages.Add(messages);

            messages = ValidateDouble(obj.PDVStopa);
            if (messages != null)
                errorMessages.Add(messages);


            return errorMessages;
        }
    }
}
