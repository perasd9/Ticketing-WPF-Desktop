using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator.Validator;

namespace TicketingServer.Validator
{
    internal class SportskiDogadjajValidator : GenericValidator<SportskiDogadjaj>
    {
        public override List<string> Validate(SportskiDogadjaj obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Objekat ne sme biti null");

            var errorMessages = new List<string>();

            var messages = ValidateLength(obj.NazivDogadjaja, 4);
            if (messages != null)
                errorMessages.Add(messages);

            messages = ValidateLength(obj.OpisDogadjaja, 4);
            if (messages != null)
                errorMessages.Add(messages);

            messages = ValidateDouble(obj.CenaKarte);
            if (messages != null)
                errorMessages.Add(messages);

            messages = ValidateDateAfter(obj.DatumOdrzavanja);
            if (messages != null)
                errorMessages.Add(messages);

            return errorMessages;
        }
    }
}
