using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TicketingClientWPF.ViewModel;

namespace TicketingClientWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                MainCoordinator.Instance.StartLoginForm();
            }
            catch (Exception ex)
            {

                Debug.WriteLine("-------App.xaml, unchecked exc: " + ex.Message);
            }
        }
    }
}
