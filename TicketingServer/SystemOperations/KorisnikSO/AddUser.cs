using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator.Validator;
using TicketingServer.Validator;
using System.Linq;

namespace TicketingServer.SystemOperations.KorisnikSO
{
    public class AddUser : AbstractSystemOperation
    {
        readonly Korisnik korisnik;
        private readonly GenericValidator<Korisnik> validator;

        public AddUser(Korisnik korisnik) : base()
        {
            this.korisnik = korisnik;
            validator = new KorisnikValidator();
        }

        protected async override Task ExecuteSpecification()
        {
            await broker.AddAsync(korisnik);
        }

        protected override async Task Preconditions()
        {
            List<string> messages = validator.Validate(korisnik);
            foreach (var item in messages)
                Console.WriteLine(item);

            if (messages.Count > 0)
                throw new ArgumentException("Podaci nisu validni");

            List<Korisnik> allUsers = await Controller.Instance.GetAllUsers();

            foreach(var item in allUsers)
                if(item.Jmbg == korisnik.Jmbg || item.Email == korisnik.Email)
                    throw new ArgumentException("Korisnik sa tim JMBG-om ili Emailom vec postoji!");

        }
    }
}
