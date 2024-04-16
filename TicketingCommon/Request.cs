using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingCommon
{
    [Serializable]
    public class Request
    {
        public Operation Operacija { get; set; }
        public object Parameter { get; set; }
    }
}
