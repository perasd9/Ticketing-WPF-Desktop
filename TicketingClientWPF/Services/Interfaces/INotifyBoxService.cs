using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingClientWPF.Services
{
    internal interface INotifyBoxService
    {
        void Show(string title, string message);
    }
}
