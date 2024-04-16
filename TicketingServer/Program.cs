using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketingCommon.Model;
using TicketingServer.SystemOperations.RezervacijaSO;

namespace TicketingServer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new FrmServer());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("-------App.xaml, unchecked exc: " + ex.Message);
            }
        }
    }
}
