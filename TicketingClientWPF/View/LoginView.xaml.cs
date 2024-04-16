using System.Windows;
using System.Windows.Input;
using TicketingClient.Komunikacija;
using TicketingClientWPF.ViewModel;

namespace TicketingClientWPF.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
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
            if (MainCoordinator.Instance.loginView.WindowState != WindowState.Maximized)
                MainCoordinator.Instance.loginView.WindowState = WindowState.Maximized;
            else
                MainCoordinator.Instance.loginView.WindowState = WindowState.Normal;
        }
    }
}
