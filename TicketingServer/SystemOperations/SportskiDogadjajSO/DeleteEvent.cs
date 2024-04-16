using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.SportskiDogadjajSO
{
    public class DeleteEvent : AbstractSystemOperation
    {
        readonly SportskiDogadjaj dogadjaj;

        public DeleteEvent(SportskiDogadjaj dogadjaj) : base()
        {
            this.dogadjaj = dogadjaj;
        }
        protected override async Task ExecuteSpecification()
        {
            dogadjaj.IsDeleted = true;
            await broker.Update(dogadjaj, dogadjaj.DogadjajId);
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
