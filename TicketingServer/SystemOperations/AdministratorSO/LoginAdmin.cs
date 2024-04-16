using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingServer.Validator;

namespace TicketingServer.SystemOperations.AdministratorSO
{
    public class LoginAdmin : AbstractSystemOperation
    {
        public Administrator admin;

        public LoginAdmin(Administrator admin)
        {
            this.admin = admin;
        }
        protected override async Task ExecuteSpecification()
        {
            admin = (Administrator)(await broker.GetAll(admin).Where(admin, $"email = '{admin.Email}'")
                .ToList(typeof(Administrator))).FirstOrDefault();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
