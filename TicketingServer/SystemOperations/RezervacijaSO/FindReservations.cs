using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.RezervacijaSO
{
    public class FindReservations : AbstractSystemOperation
    {
        public List<Rezervacija> Reservations { get; set; } = new List<Rezervacija>();
        readonly Rezervacija reservation;
        readonly string kriteria;

        public FindReservations(string kriteria, Rezervacija reservation)
        {
            this.reservation = reservation;
            this.kriteria = kriteria;
        }
        protected async override Task ExecuteSpecification()
        {
            List<KomponentaRezervacije> listaKomponenti = new List<KomponentaRezervacije>();

            Reservations = (await broker.GetAll(reservation).Join(reservation, new Korisnik()).Where(reservation, kriteria).ToList(typeof(Rezervacija))).Select(entity => (Rezervacija)entity).ToList();
            foreach (var item in Reservations)
            {
                listaKomponenti = (await broker.GetAll(new KomponentaRezervacije()).Join(new KomponentaRezervacije(), new Rezervacija())
                    .Join(new KomponentaRezervacije(), new SportskiDogadjaj())
                    .Where(new KomponentaRezervacije(), $"rezervacijaId = {item.RezervacijaId}")
                    .ToList(typeof(KomponentaRezervacije))).Select(entity => (KomponentaRezervacije)entity).ToList();
                item.ListaKomponenti = listaKomponenti;

            }
        }
        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
