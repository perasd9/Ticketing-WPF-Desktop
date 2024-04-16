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
    public class UpdateReservation : AbstractSystemOperation
    {
        readonly Rezervacija rezervacija;
        private readonly GenericValidator<Rezervacija> validator;

        public UpdateReservation(Rezervacija rezervacija) : base()
        {
            this.rezervacija = rezervacija;
            validator = new ReservationValidator();
        }

        protected async override Task ExecuteSpecification()
        {
            await broker.Update(rezervacija, rezervacija.RezervacijaId);
            foreach (var item in rezervacija.ListaKomponenti)
            {
                item.RezervacijaId = rezervacija.RezervacijaId;
                if (item.Status == Status.Modified)
                    await broker.Update(item, item.RezervacijaId);
                else if (item.Status == Status.Added)
                    await broker.AddAsync(item);
                else if (item.Status == Status.Deleted)
                    await broker.Delete(item);
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
