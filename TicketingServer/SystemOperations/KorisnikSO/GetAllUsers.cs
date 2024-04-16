using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.KorisnikSO
{
    public class GetAllUsers : AbstractSystemOperation
    {
        public List<Korisnik> Users { get; set; } = new List<Korisnik>();
        readonly Korisnik korisnik;

        public GetAllUsers(Korisnik korisnik)
        {
            this.korisnik = korisnik;
        }

        protected async override Task ExecuteSpecification()
        {
            Users = (await broker.GetAll(korisnik).Join(korisnik, new Administrator()).Join(korisnik, new Mesto()).ToList(typeof(Korisnik))).Select(entity => (Korisnik)entity).ToList();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
