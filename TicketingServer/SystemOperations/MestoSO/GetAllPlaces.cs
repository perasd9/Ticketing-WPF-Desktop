using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;

namespace TicketingServer.SystemOperations.MestoSO
{
    public class GetAllPlaces : AbstractSystemOperation
    {
        public List<Mesto> Places { get; set; } = new List<Mesto>();
        readonly Mesto mesto;

        public GetAllPlaces(Mesto mesto)
        {
            this.mesto = mesto;
        }

        protected async override Task ExecuteSpecification()
        {
            Places = (await broker.GetAll(mesto).ToList(typeof(Mesto))).Select(entity => (Mesto)entity).ToList();
        }

        protected override Task Preconditions()
        {
            return Task.CompletedTask;
        }
    }
}
