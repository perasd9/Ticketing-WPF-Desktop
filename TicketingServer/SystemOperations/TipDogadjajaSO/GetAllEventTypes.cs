using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.TipDogadjajaSO
{
    public class GetAllEventTypes : AbstractSystemOperation
    {
        public List<TipDogadjaja> EventTypes { get; set; } = new List<TipDogadjaja>();
        readonly TipDogadjaja tip;

        public GetAllEventTypes(TipDogadjaja tip)
        {
            this.tip = tip;
        }

        protected async override Task ExecuteSpecification()
        {
            EventTypes = (await broker.GetAll(tip).ToList(typeof(TipDogadjaja))).Select(entity => (TipDogadjaja)entity).ToList();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
