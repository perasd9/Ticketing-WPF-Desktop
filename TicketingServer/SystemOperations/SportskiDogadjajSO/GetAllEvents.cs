using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.SportskiDogadjajSO
{
    public class GetAllEvents : AbstractSystemOperation
    {
        public List<SportskiDogadjaj> Events { get; set; } = new List<SportskiDogadjaj>();
        readonly SportskiDogadjaj dogadjaj;

        public GetAllEvents(SportskiDogadjaj dogadjaj)
        {
            this.dogadjaj = dogadjaj;
        }

        protected async override Task ExecuteSpecification()
        {
            Events = (await broker.GetAll(dogadjaj).Join(dogadjaj, new TipDogadjaja()).Join(dogadjaj, new Administrator())
                .Where(dogadjaj, " IsDeleted = 0").ToList(typeof(SportskiDogadjaj))).Select(entity => (SportskiDogadjaj)entity).ToList();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
