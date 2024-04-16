using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.SportskiDogadjajSO
{
    public class FindEvents : AbstractSystemOperation
    {
        public List<SportskiDogadjaj> Events { get; set; } = new List<SportskiDogadjaj>();
        readonly SportskiDogadjaj dogadjaj;
        readonly string kriteria;

        public FindEvents(string kriteria, SportskiDogadjaj dogadjaj)
        {
            this.dogadjaj = dogadjaj;
            this.kriteria = kriteria;
        }
        protected async override Task ExecuteSpecification()
        {
            Events = (await broker.GetAll(dogadjaj).Join(dogadjaj, new TipDogadjaja()).Join(dogadjaj, new Administrator())
                .Where(dogadjaj, kriteria + " and IsDeleted = 0").ToList(typeof(SportskiDogadjaj))).Select(entity => (SportskiDogadjaj)entity).ToList();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
