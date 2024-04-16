using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.RezervacijaSO
{
    public class GetAllReservations : AbstractSystemOperation
    {
        public List<Rezervacija> Reservations { get; set; } = new List<Rezervacija>();
        readonly Rezervacija reservation;

        public GetAllReservations(Rezervacija reservation)
        {
            this.reservation = reservation;
        }

        protected async override Task ExecuteSpecification()
        {
            List<KomponentaRezervacije> listaKomponenti = new List<KomponentaRezervacije>();

            Reservations = (await broker.GetAll(reservation).Join(reservation, new Korisnik()).ToList(typeof(Rezervacija))).Select(entity => (Rezervacija)entity).ToList();
            foreach(var item in Reservations)
            {
                listaKomponenti = (await broker.GetAll(new KomponentaRezervacije()).Join(new KomponentaRezervacije(), new Rezervacija())
                    .Join(new KomponentaRezervacije(), new SportskiDogadjaj())
                    .Where(new KomponentaRezervacije(),$"rezervacijaId = {item.RezervacijaId}")
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
