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
using TicketingCommon.Model;

namespace TicketingClientWPF.View.Controls.Reservation
{
    /// <summary>
    /// Interaction logic for UCCreateAndUpdateReservation.xaml
    /// </summary>
    public partial class UCCreateAndUpdateReservation : UserControl
    {
        public event EventHandler GetAllEvents;
        public event EventHandler CellEditing;

        public UCCreateAndUpdateReservation(FormMode mode, Rezervacija reservation = null)
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllEvents?.Invoke(this, new EventArgs());
        }

        private void cmbSportskiDogadjaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCenaKarte.Text = ((SportskiDogadjaj)cmbSportskiDogadjaj.SelectedItem)?.CenaKarte.ToString();
        }

        private void dgvComponentes_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            CellEditing?.Invoke(sender, e);
        }
    }
}
