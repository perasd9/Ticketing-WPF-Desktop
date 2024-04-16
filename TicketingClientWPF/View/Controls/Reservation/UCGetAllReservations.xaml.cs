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

namespace TicketingClientWPF.View.Controls.Reservation
{
    /// <summary>
    /// Interaction logic for UCGetAllReservations.xaml
    /// </summary>
    public partial class UCGetAllReservations : UserControl
    {
        private FormMode mode;

        public event EventHandler GetAllReservations;
        public event EventHandler SearchReservations;


        public UCGetAllReservations(FormMode mode)
        {
            InitializeComponent();
            this.mode = mode;
            btnLoadReservation.Visibility = (mode == FormMode.LoginUser) ? Visibility.Visible : Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllReservations?.Invoke(this, new EventArgs());
        }

        private void txtNaziv_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchReservations?.Invoke(this, new EventArgs());
        }
    }
}
