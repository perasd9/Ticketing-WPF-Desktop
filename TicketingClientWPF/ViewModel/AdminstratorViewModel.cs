using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using TicketingClient.Komunikacija;
using TicketingClientWPF.MVVM;
using TicketingClientWPF.Services;
using TicketingClientWPF.Validation_rules;
using TicketingClientWPF.View;
using TicketingClientWPF.View.Controls.Event;
using TicketingClientWPF.View.Controls.Reservation;
using TicketingClientWPF.View.Controls.User;
using TicketingCommon;
using TicketingCommon.Model;

namespace TicketingClientWPF.ViewModel
{
    internal class AdminstratorViewModel : ViewModelBase
    {
        //Controls
        private UCCreateUser createUser;
        private UCCreateEvent createEvent;
        private UCGetAllEvents getAllEvents;
        private UCGetAllReservations getAllReservations;
        private UCGetAllUsers getAllUsers;

        //Constructor
        public AdminstratorViewModel(INotifyBoxService boxService)
        {
            this.boxService = boxService;
        }
        private readonly INotifyBoxService boxService;


        //Commands
        public RelayCommand CreateEventCommand => new RelayCommand(execute => CreateEvent(), o => true);
        public RelayCommand CreateUserCommand => new RelayCommand(CreateUser, o => true);
        public RelayCommand DeleteEventCommand => new RelayCommand(execute => DeleteEvent(), o => true);
        public RelayCommand LoadReservationCommand => new RelayCommand(execute => LoadReservation(), o => true);
        public RelayCommand LoadUserCommand => new RelayCommand(execute => LoadUser(), o => true);


        //Binding elements
        //EVENT
        private SportskiDogadjaj ev = new SportskiDogadjaj() { DatumOdrzavanja = DateTime.Now };
        public SportskiDogadjaj Event
        {
            get { return ev; }
            set
            {
                ev = value;
                OnPropertyChanged(nameof(Event));
            }
        }

        private ObservableCollection<TipDogadjaja> eventTypes;
        public ObservableCollection<TipDogadjaja> EventTypes
        {
            get { return eventTypes; }
            set
            {
                eventTypes = value;
                OnPropertyChanged(nameof(EventTypes));
            }
        }

        private TipDogadjaja selectedEventType;
        public TipDogadjaja SelectedEventType
        {
            get { return selectedEventType; }
            set
            {
                selectedEventType = value;
                OnPropertyChanged(nameof(SelectedEventType));
            }
        }

        private string searchStringEvents;
        public string SearchStringEvents
        {
            get { return searchStringEvents; }
            set
            {
                searchStringEvents = value;
                OnPropertyChanged(nameof(SearchStringEvents));
            }
        }

        private ObservableCollection<SportskiDogadjaj> events;
        public ObservableCollection<SportskiDogadjaj> Events
        {
            get { return events; }
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private SportskiDogadjaj selectedEvent;
        public SportskiDogadjaj SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }


        //USER
        private Korisnik user = new Korisnik() { DatumRodjenja = DateTime.Now };
        public Korisnik User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private ObservableCollection<Mesto> places;
        public ObservableCollection<Mesto> Places
        {
            get { return places; }
            set
            {
                places = value;
                OnPropertyChanged(nameof(Places));
            }
        }

        private Mesto selectedPlace;
        public Mesto SelectedPlace
        {
            get { return selectedPlace; }
            set
            {
                selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }
        private string searchStringUsers;
        public string SearchStringUsers
        {
            get { return searchStringUsers; }
            set
            {
                searchStringUsers = value;
                OnPropertyChanged(nameof(SearchStringUsers));
            }
        }

        private ObservableCollection<Korisnik> users;
        public ObservableCollection<Korisnik> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private Korisnik selectedUser;
        public Korisnik SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }


        //RESERVATION
        private string searchStringReservations;
        public string SearchStringReservations
        {
            get { return searchStringReservations; }
            set
            {
                searchStringReservations = value;
                OnPropertyChanged(nameof(SearchStringReservations));
            }
        }

        private ObservableCollection<Rezervacija> reservations;
        public ObservableCollection<Rezervacija> Reservations
        {
            get { return reservations; }
            set
            {
                reservations = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }

        private Rezervacija selectedReservation;

        public Rezervacija SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        //Controls initializing
        internal UIElement CreateUCCreateEvent()
        {
            createEvent = new UCCreateEvent();
            createEvent.GetAllEventTypes += (sender, args) => GetAllEventTypes();
            Event = new SportskiDogadjaj();

            return createEvent;
        }

        internal UserControl CreateUCCreateUser()
        {
            createUser = new UCCreateUser();
            createUser.GetAllPlaces += (sender, args) => GetAllPlaces();
            createUser.PasswordChanged += (sender, args) => PasswordChanged();
            PasswordChanged();
            User = new Korisnik();

            return createUser;
        }

        internal UIElement CreateUCGetAllEventTypes(FormMode loginAdmin)
        {
            getAllEvents = new UCGetAllEvents(loginAdmin);
            getAllEvents.GetAllEvents += (sender, args) => GetAllEvents();
            getAllEvents.SearchEvents += (sender, args) => SearchEvents();

            return getAllEvents;
        }

        internal UIElement CreateUCGetAllReservations(FormMode loginAdmin)
        {
            getAllReservations = new UCGetAllReservations(loginAdmin);
            getAllReservations.GetAllReservations += (sender, args) => GetAllReservations();
            getAllReservations.SearchReservations += (sender, args) => SearchReservations();

            return getAllReservations;
        }

        internal UIElement CreateUCGetAlUsers()
        {
            getAllUsers = new UCGetAllUsers();
            getAllUsers.GetAllUsers += (sender, args) => GetAllUsers();
            getAllUsers.SearchUsers += (sender, args) => SearchUsers();

            return getAllUsers;
        }


        //Control method for events on controls
        private void GetAllEventTypes()
        {
            EventTypes = new ObservableCollection<TipDogadjaja>(Communication.Instance.GetAllEventTypes());
            SelectedEventType = EventTypes.FirstOrDefault();
        }
        private void CreateEvent()
        {
            Event.AdminId = Communication.Instance.admin.AdminId;
            Event.TipDogadjajaId = SelectedEventType == null ? 0 : SelectedEventType.TipDogadjajaId;
            if (Communication.Instance.CreateEvent(Event))
            {
                Event = new SportskiDogadjaj();
                boxService.Show("Kreiranje dogadjaja", "Uspesno ste kreirali sportski dogadjaj");
            }
            else
                boxService.Show("Kreiranje dogadjaja", "Nije moguce kreirati sportski dogadjaj");
        }
        private void CreateUser(object password)
        {
            User.AdminId = Communication.Instance.admin.AdminId;
            User.MestoId = SelectedPlace == null ? 0 : SelectedPlace.MestoId;

            User.Sifra = GetSHA1(((PasswordBox)password).Password);

            Response res = Communication.Instance.CreateUser(User);
            if (res != null)
            {
                if (res.SignalUspesno)
                {
                    User = new Korisnik();
                    ((PasswordBox)password).Password = "";
                }
                else
                    boxService.Show("Kreiranje korisnika", "Nije moguce kreirati korisnika");

                boxService.Show("Kreiranje korisnika", res.Message);
            }
        }
        //
        #region SHA1 Hashing
        private static byte[] GetSHA1(string password)
        {
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
        }
        #endregion
        //
        private void GetAllPlaces()
        {
            Places = new ObservableCollection<Mesto>(Communication.Instance.GetAllPlaces());
            SelectedPlace = Places.FirstOrDefault();
        }
        private void DeleteEvent()
        {
            if(getAllEvents.dgvEvents.SelectedItem == null)
            {
                boxService.Show("Brisanje dogadjaja", "Niste odabrali nijedan dogadjaj!");
                return;
            }
            if (Communication.Instance.DeleteEvent(SelectedEvent))
            {
                boxService.Show("Brisanje dogadjaja", "Sistem je obrisao sportski dogadjaj");
                getAllEvents.dgvEvents.ItemsSource = Communication.Instance.GetAllEvents();
                RemoveColumns(getAllEvents.dgvEvents);
            }
            else
            {
                boxService.Show("Brisanje dogadjaja", "Nije moguce obrisati sportski dogadjaj");
            }
        }
        private void GetAllEvents()
        {
            Events = new ObservableCollection<SportskiDogadjaj>(Communication.Instance.GetAllEvents());
            SelectedEvent = Events.FirstOrDefault();
            RemoveColumns(getAllEvents.dgvEvents);
        }
        private void SearchEvents()
        {
            Events = new ObservableCollection<SportskiDogadjaj>(Communication.Instance.GetEventsBySearch("nazivDogadjaja LIKE '%" + SearchStringEvents + "%'"));
            RemoveColumns(getAllEvents.dgvEvents);
        }
        private void LoadReservation()
        {
            if (getAllReservations.dgvReservations.SelectedItem == null)
            {
                boxService.Show("Ucitavaje rezervacije", "Niste odabrali nijednu rezervaciju!");
                return;
            }
            LoadDetailsView loadDetailsView = new LoadDetailsView();
            MainCoordinator.Instance.SetDetailsView(loadDetailsView);

            Rezervacija res = Communication.Instance.LoadReservation(SelectedReservation);

            if(res != null)
            {
                MainCoordinator.Instance.ShowDetailsReservation(res);
                boxService.Show("Ucitavaje rezervacije", "Prikaz podataka o rezervaciji");
                loadDetailsView.ShowDialog();
            }
            else
                boxService.Show("Ucitavaje rezervacije", "Sistem ne moze da prikaze podatke o rezervaciji");
        }
        private void GetAllReservations()
        {
            Reservations = new ObservableCollection<Rezervacija>(Communication.Instance.GetAllReservations());
            SelectedReservation = Reservations.FirstOrDefault();
            RemoveColumns(getAllReservations.dgvReservations);
        }
        private void SearchReservations()
        {
            Reservations = new ObservableCollection<Rezervacija>(Communication.Instance.GetAllReservationsBySearch("jmbg LIKE '%" + SearchStringReservations + "%'"));
            RemoveColumns(getAllReservations.dgvReservations);
        }
        private void SearchUsers()
        {
            Users = new ObservableCollection<Korisnik>(Communication.Instance.GetUsersBySearch("ime LIKE '%" + SearchStringUsers + "%'"));
            RemoveColumns(getAllUsers.dgvUsers);
        }

        private void GetAllUsers()
        {
            Users = new ObservableCollection<Korisnik>(Communication.Instance.GetAllUsers());
            SelectedUser = Users.FirstOrDefault();
            RemoveColumns(getAllUsers.dgvUsers);
        }
        private void LoadUser()
        {
            if (getAllUsers.dgvUsers.SelectedItem == null)
            {
                boxService.Show("Ucitavanje korisnika", "Niste odabrali nijednog korisnika!");
                return;
            }
            LoadDetailsView loadDetailsView = new LoadDetailsView();
            MainCoordinator.Instance.SetDetailsView(loadDetailsView);

            Korisnik user = Communication.Instance.LoadUser(SelectedUser);

            if (user != null)
            {
                MainCoordinator.Instance.ShowDetailsUser(user, Places);
                boxService.Show("Ucitavanje korisnika", "Prikaz podataka o korisniku");
                loadDetailsView.ShowDialog();
                getAllUsers.dgvUsers.ItemsSource = Communication.Instance.GetAllUsers();
                RemoveColumns(getAllUsers.dgvUsers);
            }
            else
                boxService.Show("Ucitavanje korisnika", "Sistem ne moze da prikaze podatke o korisniku");
        }

        //Helper methods
        private void RemoveColumns(DataGrid dgvDogadjaji)
        {
            if (getAllEvents?.dgvEvents == dgvDogadjaji)
            {
                foreach(var item in dgvDogadjaji.Columns)
                {
                    if(item.Header.ToString() == "DogadjajId")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if(item.Header.ToString() == "TipDogadjajaId")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if(item.Header.ToString() == "AdminId")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "TableName")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "Parameters")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "UpdateParameters")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "IsDeleted")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                }
                
            }
            else if(getAllReservations?.dgvReservations == dgvDogadjaji)
            {
                foreach (var item in dgvDogadjaji.Columns)
                {
                    if (item.Header.ToString() == "RezervacijaId")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "ListaKomponenti")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "TableName")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "Parameters")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "UpdateParameters")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                }
            }
            else if (getAllUsers?.dgvUsers == dgvDogadjaji)
            {
                foreach (var item in dgvDogadjaji.Columns)
                {
                    if (item.Header.ToString() == "AdminId")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "MestoId")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "Sifra")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "TableName")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "Parameters")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "UpdateParameters")
                    {
                        dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                }
            }
        }
        private void PasswordChanged()
       {
            ValidationRule validation = new LengthValidationRule() { Length = 3 };
            ValidationResult result = validation.Validate(createUser.txtLozinka.Password, null);

            if (!result.IsValid)
            {
                createUser.txtPasswordError.Text = result.ErrorContent.ToString();
            }
            else
                createUser.txtPasswordError.Text = "";
        }

    }
}
