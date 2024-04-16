using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.KorisnikSO
{
    public class GetUser : AbstractSystemOperation
    {
        public Korisnik Korisnik { get; private set; }

        public GetUser(Korisnik korisnik)
        {
            Korisnik = korisnik;
        }

        protected async override Task ExecuteSpecification()
        {   
            Korisnik = (Korisnik)(await broker.GetAll(Korisnik).Join(Korisnik, new Administrator()).Join(Korisnik, new Mesto())
                .Where(Korisnik, $"jmbg = '{Korisnik.Jmbg}'")
                .ToList(typeof(Korisnik))).FirstOrDefault();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}


