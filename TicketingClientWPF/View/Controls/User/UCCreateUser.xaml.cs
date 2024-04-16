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

namespace TicketingClientWPF.View.Controls.User
{
    /// <summary>
    /// Interaction logic for UCCreateUser.xaml
    /// </summary>
    public partial class UCCreateUser : UserControl
    {
        public event EventHandler GetAllPlaces;
        public event EventHandler PasswordChanged;

        public UCCreateUser()
        {
            InitializeComponent();
            txtDatumRodjenja.SelectedDate = DateTime.Now;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllPlaces?.Invoke(this, new EventArgs());
        }

        private void txtLozinka_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordChanged?.Invoke(sender, new EventArgs());
        }
    }
}
