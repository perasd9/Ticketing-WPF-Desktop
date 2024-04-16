using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.RezervacijaSO
{
    public class GetReservation : AbstractSystemOperation
    {
        public Rezervacija Reservation { get; private set; }

        public GetReservation(Rezervacija reservation)
        {
            Reservation = reservation;
        }

        protected async override Task ExecuteSpecification()
        {
            List<KomponentaRezervacije> listaKomponenti = new List<KomponentaRezervacije>();

            Reservation = (Rezervacija)(await broker.GetAll(Reservation).Join(Reservation, new Korisnik())
                .Where(Reservation, $"rezervacijaId = {Reservation.RezervacijaId}")
                .ToList(typeof(Rezervacija))).FirstOrDefault();

                listaKomponenti = (await broker.GetAll(new KomponentaRezervacije()).Join(new KomponentaRezervacije(), new Rezervacija())
                    .Join(new KomponentaRezervacije(), new SportskiDogadjaj())
                    .Where(new KomponentaRezervacije(), $"rezervacijaId = {Reservation.RezervacijaId}")
                    .ToList(typeof(KomponentaRezervacije))).Select(entity => (KomponentaRezervacije)entity).ToList();

            Reservation.ListaKomponenti = listaKomponenti;

        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
