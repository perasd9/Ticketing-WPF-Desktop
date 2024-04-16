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
    /// Interaction logic for UCGetAllUsers.xaml
    /// </summary>
    public partial class UCGetAllUsers : UserControl
    {
        public event EventHandler GetAllUsers;
        public event EventHandler SearchUsers;
        public UCGetAllUsers()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllUsers?.Invoke(this, new EventArgs());
        }

        private void txtNaziv_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchUsers?.Invoke(this, new EventArgs());
        }
    }
}
