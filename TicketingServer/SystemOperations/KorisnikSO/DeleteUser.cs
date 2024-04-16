using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.KorisnikSO
{
    public class DeleteUser : AbstractSystemOperation
    {
        readonly Korisnik korisnik;

        public DeleteUser(Korisnik korisnik) : base()
        {
            this.korisnik = korisnik;
        }
        protected async override Task ExecuteSpecification()
        {
            await broker.Delete(korisnik);
        }

        protected override async Task Preconditions()
        {
            List<Rezervacija> allReservations = await Controller.Instance.GetAllReservations();

            foreach (var item in allReservations)
                if (item.Jmbg == korisnik.Jmbg)
                    throw new ArgumentException("Korisnik vec ima rezeracije i ne moze biti obrisan");
        }
    }
}
