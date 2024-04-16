using System;
using System.Collections.ObjectModel;
using System.Windows;
using TicketingClient.Komunikacija;
using TicketingClientWPF.Services;
using TicketingClientWPF.View;
using TicketingCommon.Model;

namespace TicketingClientWPF.ViewModel
{
    internal class MainCoordinator
    {
        private static MainCoordinator instance;
        private static readonly object InstanceLock = new object();
        public static MainCoordinator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                            instance = new MainCoordinator();
                    }
                }
                return instance;
            }
        }

        public AdministratorView administratorView;
        public LoadDetailsView loadDetailsView;
        public LoginView loginView;
        public UserView userView;

        internal void SetAdministratorView(AdministratorView av)
        {
            administratorView = av;
        }
        internal void SetUserView(UserView uv)
        {
            userView = uv;
        }
        internal void SetDetailsView(LoadDetailsView ld)
        {
            loadDetailsView = ld;
        }
        private AdminstratorViewModel adminstratorViewModel;
        private LoadDetailsViewModel loadDetailsViewModel;
        private LoginViewModel loginViewModel;
        private UserViewModel userViewModel;
        private INotifyBoxService boxService;

        private MainCoordinator()
        {
            boxService = new NotifyBoxService();
            adminstratorViewModel = new AdminstratorViewModel(boxService);
            loadDetailsViewModel = new LoadDetailsViewModel(boxService);
            loginViewModel = new LoginViewModel(boxService);
            userViewModel = new UserViewModel(boxService);
        }

        public void StartLoginForm()
        {
            loginView = new LoginView
            {
                DataContext = loginViewModel
            };
            loginView.ChangePanel(loginViewModel.CreateUCLoginButtons());
            if (Communication.Instance.Connect())
            {
                loginView.Show();
                loginView.DataContext = loginViewModel;
            }
            else
                boxService.Show("Pokretanje", "Server nije pokrenut!");
        }


        internal void ShowLoginPanel(FormMode mode)
        {
            loginView.DataContext = loginViewModel;
            loginView?.ChangePanel(loginViewModel.CreateUCLogin(mode));
        }

        internal void ShowAddUserPanel()
        {
            administratorView.DataContext = adminstratorViewModel;
            administratorView?.ChangePanel(adminstratorViewModel.CreateUCCreateUser());
        }

        internal void ShowAddEventPanel()
        {
            administratorView.DataContext = adminstratorViewModel;
            administratorView?.ChangePanel(adminstratorViewModel.CreateUCCreateEvent());
        }

        internal void ShowGetAllEvents()
        {
            if (administratorView != null)
                administratorView.DataContext = adminstratorViewModel;
            if(userView != null)
                userView.DataContext = userViewModel;
            administratorView?.ChangePanel(adminstratorViewModel.CreateUCGetAllEventTypes(FormMode.LoginAdmin));
            userView?.ChangePanel(userViewModel.CreateUCGetAllEventTypes(FormMode.LoginUser));
        }

        internal void ShowCreateAndUpdateReservation(FormMode mode)
        {
            userView.DataContext = userViewModel;
            userView?.ChangePanel(userViewModel.CreateUCCreateAndUpdateReservation(mode));
        }

        internal void ShowGetAllReservations()
        {
            if (administratorView != null)
                administratorView.DataContext = adminstratorViewModel;
            if (userView != null)
                userView.DataContext = userViewModel;
            administratorView?.ChangePanel(adminstratorViewModel.CreateUCGetAllReservations(FormMode.LoginAdmin));
            userView?.ChangePanel(userViewModel.CreateUCGetAllReservations(FormMode.LoginUser));
        }

        internal void ShowGetAllUsers()
        {
            administratorView.DataContext = adminstratorViewModel;
            administratorView?.ChangePanel(adminstratorViewModel.CreateUCGetAlUsers());
        }

        internal void ShowDetailsUser(Korisnik korisnik, ObservableCollection<Mesto> places)
        {
            loadDetailsView.DataContext = loadDetailsViewModel;
            loadDetailsViewModel.User = korisnik;
            loadDetailsView?.ChangePanel(loadDetailsViewModel.CreateUCUserDetails(korisnik));
        }

        internal void ShowDetailsReservation(Rezervacija reservation)
        {
            loadDetailsView.DataContext = loadDetailsViewModel;
            loadDetailsView?.ChangePanel(loadDetailsViewModel.CreateUCCreateAndUpdateReservation(FormMode.Azuriraj, reservation));
        }

        internal void Logout()
        {
            if (Communication.Instance.Logout())
            {
                userView?.Close();
                administratorView?.Close();
                loginView = new LoginView
                {
                    DataContext = loginViewModel
                };
                loginViewModel.Email = "";
                loginView.ChangePanel(loginViewModel.CreateUCLoginButtons());
                loginView.ShowDialog();
            }
            else
            {
                boxService.Show("Odjavljivanje", "Nije moguce odjaviti klijenta!");
            }
        }

    }

}
