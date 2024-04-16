using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicketingClientWPF.View.Controls.Event
{
    /// <summary>
    /// Interaction logic for UCGetAllEvents.xaml
    /// </summary>
    public partial class UCGetAllEvents : UserControl
    {
        public event EventHandler GetAllEvents;
        public event EventHandler SearchEvents;
        public UCGetAllEvents(FormMode mode)
        {
            InitializeComponent();
            btnDeleteEvent.Visibility = (mode == FormMode.LoginUser) ? Visibility.Hidden : Visibility.Visible;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllEvents?.Invoke(this, EventArgs.Empty);
        }

        private void txtNaziv_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchEvents?.Invoke(this, EventArgs.Empty);
        }
    }
}
