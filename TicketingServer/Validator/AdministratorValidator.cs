using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator.Validator;

namespace TicketingServer.Validator
{
    internal class AdministratorValidator : GenericValidator<Administrator>
    {
        public override List<string> Validate(Administrator obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Objekat ne sme biti null");

            var errorMessages = new List<string>();

            var messages = ValidateLength(obj.Ime, 4);
            if (messages != null)
                errorMessages.Add(messages);

            messages = ValidateLength(obj.Prezime, 4);
            if (messages != null)
                errorMessages.Add(messages);

            messages = ValidateEmail(obj.Email);
            if (messages != null)
                errorMessages.Add(messages);

            return errorMessages;
        }
    }
}
