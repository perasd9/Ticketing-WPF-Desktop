using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using TicketingClient.Komunikacija;
using TicketingClientWPF.MVVM;
using TicketingClientWPF.Services;
using TicketingClientWPF.View;
using TicketingClientWPF.View.Controls.Login;
using TicketingCommon;
using TicketingCommon.Model;

namespace TicketingClientWPF.ViewModel
{
    internal class LoginViewModel : ViewModelBase
    {
        //Controls
        private UCLogin login;
        private UCLoginButtons loginButtons;
        private FormMode mode;

        //Constructor
        public LoginViewModel(INotifyBoxService boxService)
        {
            this.boxService = boxService;
        }
        private readonly INotifyBoxService boxService;

        //Commands
        public RelayCommand LoginAsAdminCommand => new RelayCommand(execute => LoginAsAdmin(), (o) => true);
        public RelayCommand LoginAsUserCommand => new RelayCommand(execute => LoginAsUser(), (o) => true);
        public RelayCommand LoginCommand => new RelayCommand(Login, (o) => true);

        //Creating controls
        internal UIElement CreateUCLogin(FormMode mode)
        {
            this.login = new UCLogin();

            this.mode = mode;
            return login;
        }
        internal UIElement CreateUCLoginButtons()
        {
            loginButtons = new UCLoginButtons();

            return loginButtons;
        }

        //Binding elements
        private string email = "";
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        //Methods for controls
        private void Login(object password)
        {
            try
            {
                if (mode == FormMode.LoginAdmin)
                {
                    try
                    {
                        Response res = Communication.Instance.LoginAdmin(Email, GetSHA1(((PasswordBox)password).Password));
                        if (res == null) throw new Exception();

                        if (res.Result != null)
                        {
                            Communication.Instance.SetAdmin((Administrator)res.Result);
                            AdministratorView av = new AdministratorView();
                            MainCoordinator.Instance.SetAdministratorView(av);
                            MainCoordinator.Instance.loginView.Visibility = Visibility.Collapsed;
                            boxService.Show("Prijava admina", res.Message);
                            av.ShowDialog();
                        }
                        else
                        {
                            boxService.Show("Prijava admina", res.Message);
                            return;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    try
                    {
                        Response res = Communication.Instance.LoginUser(Email, GetSHA1(((PasswordBox)password).Password));
                        if (res == null) throw new Exception();

                        if (res.Result != null)
                        {
                            Communication.Instance.SetUser((Korisnik)res.Result);
                            UserView uv = new UserView();
                            MainCoordinator.Instance.SetUserView(uv);
                            MainCoordinator.Instance.loginView.Visibility = Visibility.Collapsed;
                            boxService.Show("Prijava korisnika", res.Message);
                            uv.ShowDialog();
                        }
                        else
                        {
                            boxService.Show("Prijava korisnika", res.Message);
                            return;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

                MainCoordinator.Instance.loginView.Close();
            }
            catch (Exception)
            {
                Debug.WriteLine("--------- LoginVM login");
            }
        }
        //SHA
        private static byte[] GetSHA1(string password)
        {
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
        }
        //SHA
        private void LoginAsAdmin()
        {
            MainCoordinator.Instance.ShowLoginPanel(FormMode.LoginAdmin);
        }
        private void LoginAsUser()
        {
            MainCoordinator.Instance.ShowLoginPanel(FormMode.LoginUser);
        }

    }
}
