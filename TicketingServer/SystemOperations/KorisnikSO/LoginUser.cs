using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.KorisnikSO
{
    public class LoginUser : AbstractSystemOperation
    {
        public Korisnik korisnik;

        public LoginUser(Korisnik korisnik) : base()
        {
            this.korisnik = korisnik;
        }

        protected override async Task ExecuteSpecification()
        {
            korisnik = (Korisnik)(await broker.GetAll(korisnik)
                .Where(korisnik, $"email = '{korisnik.Email}'")
                .ToList(typeof(Korisnik))).FirstOrDefault();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
