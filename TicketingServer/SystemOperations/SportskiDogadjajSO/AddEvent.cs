using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator;
using TicketingServer.Validator.Validator;

namespace TicketingServer.SystemOperations.SportskiDogadjajSO
{
    public class AddEvent : AbstractSystemOperation
    {
        readonly SportskiDogadjaj dogadjaj;
        private readonly GenericValidator<SportskiDogadjaj> validator;

        public AddEvent(SportskiDogadjaj dogadjaj) : base()
        {
            this.dogadjaj = dogadjaj;
            validator = new SportskiDogadjajValidator();
        }

        protected async override Task ExecuteSpecification()
        {
            await broker.AddAsync(dogadjaj);
        }

        protected override Task Preconditions()
        {
            List<string> messages = validator.Validate(dogadjaj);
            foreach (var item in messages)
                Console.WriteLine(item);

            if (messages.Count > 0)
                throw new ArgumentException("Podaci nisu validni");

            return Task.CompletedTask;
        }
    }
}
