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
using System.Windows.Shapes;
using TicketingClient.Komunikacija;
using TicketingClientWPF.ViewModel;

namespace TicketingClientWPF.View
{
    /// <summary>
    /// Interaction logic for AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : Window
    {
        public AdministratorView()
        {
            InitializeComponent();
            searchReservationsMenu.Click += (sender, args) => MainCoordinator.Instance.ShowGetAllReservations();
            searchEventsMenu.Click += (sender, args) => MainCoordinator.Instance.ShowGetAllEvents();
            createEventMenu.Click += (sender, args) => MainCoordinator.Instance.ShowAddEventPanel();
            searchUsersMenu.Click += (sender, args) => MainCoordinator.Instance.ShowGetAllUsers();
            createUserMenu.Click += (sender, args) => MainCoordinator.Instance.ShowAddUserPanel();
            odjaviSeMenu.Click += (sender, args) => MainCoordinator.Instance.Logout();
        }
        public void ChangePanel(UIElement control)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(control);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Communication.Instance.CloseSockets();
            MainCoordinator.Instance.administratorView?.Close();
            MainCoordinator.Instance.userView?.Close();
            MainCoordinator.Instance.loginView?.Close();
            System.Environment.Exit(0);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_MiniMaximize(object sender, RoutedEventArgs e)
        {
            if (MainCoordinator.Instance.administratorView.WindowState != WindowState.Maximized)
                MainCoordinator.Instance.administratorView.WindowState = WindowState.Maximized;
            else
                MainCoordinator.Instance.administratorView.WindowState = WindowState.Normal;
        }
    }
}
