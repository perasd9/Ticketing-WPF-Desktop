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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView()
        {
            InitializeComponent();
            odjaviSeMenu.Click += (sender, args) => MainCoordinator.Instance.Logout();
            pretraziRezervacijeMenu.Click += (sender, args) => MainCoordinator.Instance.ShowGetAllReservations();
            rezervisiMenu.Click += (sender, args) => MainCoordinator.Instance.ShowCreateAndUpdateReservation(FormMode.Rezervisi);
            pretraziDogadjajeMenu.Click += (sender, args) => MainCoordinator.Instance.ShowGetAllEvents();
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
            if (MainCoordinator.Instance.userView.WindowState != WindowState.Maximized)
                MainCoordinator.Instance.userView.WindowState = WindowState.Maximized;
            else
                MainCoordinator.Instance.userView.WindowState = WindowState.Normal;
        }
    }
}
