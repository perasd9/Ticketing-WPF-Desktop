using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingCommon.Model
{
    public interface IEntity
    {
        string TableName { get;}
        string Parameters { get;}
        string UpdateParameters { get; }

    }
}
