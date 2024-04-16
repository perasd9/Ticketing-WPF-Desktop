using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TicketingClientWPF.View;

namespace TicketingClientWPF.Services
{
    internal class NotifyBoxService : INotifyBoxService
    {
        public void Show(string title, string message)
        {
            //MessageBox.Show(message, title);
            CustomMBox customBox = new CustomMBox();
            customBox.ShowDialog(title, message);
        }
    }
}
