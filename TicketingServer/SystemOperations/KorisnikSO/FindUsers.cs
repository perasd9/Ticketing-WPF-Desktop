using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.KorisnikSO
{
    public class FindUsers : AbstractSystemOperation
    {
        public List<Korisnik> Users { get; set; } = new List<Korisnik>();
        readonly Korisnik korisnik;
        readonly string kriteria;

        public FindUsers(string kriteria, Korisnik korisnik)
        {
            this.korisnik = korisnik;
            this.kriteria = kriteria;
        }
        protected async override Task ExecuteSpecification()
        {
            Users = (await broker.GetAll(korisnik).Join(korisnik, new Administrator()).Join(korisnik, new Mesto()).Where(korisnik, kriteria).ToList(typeof(Korisnik))).Select(entity => (Korisnik)entity).ToList();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
