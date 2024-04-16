using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator;
using TicketingServer.Validator.Validator;

namespace TicketingServer.SystemOperations.RezervacijaSO
{
    public class AddReservation : AbstractSystemOperation
    {
        readonly Rezervacija rezervacija;
        private readonly GenericValidator<Rezervacija> validator;

        public AddReservation(Rezervacija rezervacija) : base()
        {
            this.rezervacija = rezervacija;
            validator = new ReservationValidator();
        }

        protected async override Task ExecuteSpecification()
        {
            await broker.AddAsync(rezervacija);
            foreach(var item in rezervacija.ListaKomponenti)
            {
                item.RezervacijaId = rezervacija.RezervacijaId;
                await broker.AddAsync(item);
            }
        }

        protected override Task Preconditions()
        {
            List<string> messages = validator.Validate(rezervacija);
            foreach (var item in messages)
                Console.WriteLine(item);

            if (messages.Count > 0)
                throw new ArgumentException("Podaci nisu validni");

            return Task.CompletedTask;
        }
    }
}
