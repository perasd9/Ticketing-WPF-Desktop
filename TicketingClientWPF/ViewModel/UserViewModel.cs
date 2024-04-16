using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TicketingClient.Komunikacija;
using TicketingClientWPF.MVVM;
using TicketingClientWPF.Services;
using TicketingClientWPF.View;
using TicketingClientWPF.View.Controls.Event;
using TicketingClientWPF.View.Controls.Reservation;
using TicketingCommon.Model;

namespace TicketingClientWPF.ViewModel
{
    internal class UserViewModel : ViewModelBase
    {
        private int count = 1;

        //Controls
        private UCCreateAndUpdateReservation createAndUpdateReservation;
        private UCGetAllEvents getAllEvents;
        private UCGetAllReservations getAllReservations;

        //Constructor
        public UserViewModel(INotifyBoxService boxService)
        {
            this.boxService = boxService;
        }
        private readonly INotifyBoxService boxService;

        //Commands
        public RelayCommand AddTicketCommand => new RelayCommand(execute => AddTicket(), o => true);
        public RelayCommand DeleteTicketCommand => new RelayCommand(execute => DeleteTicket(), o => true);
        public RelayCommand CreateUpdateReservationCommand => new RelayCommand(execute => CreateUpdateReservation(), o => true);
        public RelayCommand LoadReservationCommand => new RelayCommand(execute => LoadReservation(), o => true);


        //Binding elements
        //EVENT
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

        private ObservableCollection<SportskiDogadjaj> events = new ObservableCollection<SportskiDogadjaj>();
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

        private ObservableCollection<Rezervacija> reservations = new ObservableCollection<Rezervacija>();
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

        private Rezervacija reservation = new Rezervacija();
        public Rezervacija Reservation
        {
            get { return reservation; }
            set
            {
                reservation = value;
                OnPropertyChanged(nameof(Reservation));
            }
        }

        private ObservableCollection<KomponentaRezervacije> reservationComponents = new ObservableCollection<KomponentaRezervacije>();
        public ObservableCollection<KomponentaRezervacije> ReservationComponents
        {
            get { return reservationComponents; }
            set
            {
                reservationComponents = value;
                OnPropertyChanged(nameof(ReservationComponents));
            }
        }

        private KomponentaRezervacije currentComponent = new KomponentaRezervacije();
        public KomponentaRezervacije CurrentComponent
        {
            get { return currentComponent; }
            set { currentComponent = value; OnPropertyChanged(nameof(CurrentComponent)); }
        }


        //Controls initializing
        internal UIElement CreateUCCreateAndUpdateReservation(FormMode mode)
        {
            Reservation.PDVStopa = 20;
            Reservation.DatumRezervacije = DateTime.Now;
            Reservation.UkupnaCena = 0;

            createAndUpdateReservation = new UCCreateAndUpdateReservation(mode);
            createAndUpdateReservation.btnCreateUpdateResevation.Content = "Rezervisi";
            reservationComponents.Clear();
            count = 1;
            createAndUpdateReservation.GetAllEvents += (sender, args) => GetAllEvents();
            RemoveColumns(createAndUpdateReservation.dgvComponentes);

            return createAndUpdateReservation;
        }

        internal UIElement CreateUCGetAllEventTypes(FormMode loginUser)
        {
            getAllEvents = new UCGetAllEvents(loginUser);
            getAllEvents.GetAllEvents += (sender, args) => GetAllEvents();
            getAllEvents.SearchEvents += (sender, args) => SearchEvents();

            return getAllEvents;
        }

        internal UIElement CreateUCGetAllReservations(FormMode loginUser)
        {
            getAllReservations = new UCGetAllReservations(loginUser);
            getAllReservations.GetAllReservations += (sender, args) => GetAllReservations();
            getAllReservations.SearchReservations += (sender, args) => SearchReservations();

            return getAllReservations;
        }

        //Control methods for events
        private void GetAllEvents()
        {
            Events = new ObservableCollection<SportskiDogadjaj>(Communication.Instance.GetAllEvents());
            SelectedEvent = Events.FirstOrDefault();
            RemoveColumns(getAllEvents?.dgvEvents);
            RemoveColumns(createAndUpdateReservation?.dgvComponentes);
        }
        private void SearchEvents()
        {
            Events = new ObservableCollection<SportskiDogadjaj>(Communication.Instance.GetEventsBySearch("nazivDogadjaja LIKE '%" + SearchStringEvents + "%'"));
            RemoveColumns(getAllEvents.dgvEvents);
        }
        private void AddTicket()
        {
            if (SelectedEvent == null)
            {
                boxService.Show("Dodavanje komponente", "Nijedan dogadjaj nije selektovan!");
                return;
            }
            RemoveColumns(createAndUpdateReservation?.dgvComponentes);
            double.TryParse(createAndUpdateReservation.txtCenaKarte.Text, out double ticketPrice);

            KomponentaRezervacije component = new KomponentaRezervacije()
            {
                BrojKarata = int.Parse(createAndUpdateReservation.txtBrojKarata.Text),
                UkupanIznos = ticketPrice * int.Parse(createAndUpdateReservation.txtBrojKarata.Text),
                SportskiDogadjaj = SelectedEvent,
                DogadjajId = SelectedEvent == null ? 0 : SelectedEvent.DogadjajId,
            };
            createAndUpdateReservation.txtUkupnaCena.Text = (double.Parse(createAndUpdateReservation.txtUkupnaCena.Text) + component.UkupanIznos).ToString();

            foreach (var item in ReservationComponents)
            {
                if (item.SportskiDogadjaj.Equals(component.SportskiDogadjaj))
                {
                    item.BrojKarata += component.BrojKarata;
                    item.UkupanIznos += component.UkupanIznos;

                    createAndUpdateReservation.dgvComponentes.ItemsSource = null;
                    createAndUpdateReservation.dgvComponentes.ItemsSource = ReservationComponents;
                    RemoveColumns(createAndUpdateReservation?.dgvComponentes);

                    return;
                }
            }
            component.RbKomponente = count++;
            ReservationComponents.Add(component);
            RemoveColumns(createAndUpdateReservation?.dgvComponentes);
        }
        private void DeleteTicket()
        {
            if (createAndUpdateReservation.dgvComponentes.SelectedItem == null)
            {
                boxService.Show("Brisanje komponente", "Niste odabrali nijednu komponentu!");
                return;
            }
            var komponentaRez = CurrentComponent;

            foreach (var item in ReservationComponents)
            {
                if (item.RbKomponente > komponentaRez.RbKomponente)
                {
                    item.RbKomponente--;
                    createAndUpdateReservation.dgvComponentes.Items.Refresh();
                }
            }
            count--;
            ReservationComponents.Remove(komponentaRez);
            createAndUpdateReservation.txtUkupnaCena.Text = (double.Parse(createAndUpdateReservation.txtUkupnaCena.Text) - komponentaRez.UkupanIznos).ToString();
        }
        private void CreateUpdateReservation()
        {
            reservation.Jmbg = Communication.Instance.user.Jmbg;
            reservation.ListaKomponenti = reservationComponents.ToList();
            if (Communication.Instance.SaveReservation(Reservation))
            {
                boxService.Show("Kreiranje rezervacije", "Sistem je zapamtio rezervaciju");
                Reservation = new Rezervacija();
                count = 1;
                reservationComponents.Clear();
            }
            else
                boxService.Show("Kreiranje rezervacije", "Sistem ne moze da zapamti rezervaciju");
        }
        private void LoadReservation()
        {
            if (getAllReservations.dgvReservations.SelectedItem == null)
            {
                boxService.Show("Ucitavaje rezervacije", "Niste odabrali nijednu rezervaciju!");
                return;
            }
            LoadDetailsView loadDetails = new LoadDetailsView();
            MainCoordinator.Instance.SetDetailsView(loadDetails);
            Rezervacija r = Communication.Instance.LoadReservation(SelectedReservation);

            if (r != null)
            {
                MainCoordinator.Instance.ShowDetailsReservation(r);
                boxService.Show("Ucitavanje rezervacije", "Prikaz podataka o rezervaciji");
                loadDetails.ShowDialog();
                Reservations = new ObservableCollection<Rezervacija>(Communication.Instance.GetAllReservationsBySearch("jmbg LIKE '%" + Communication.Instance.user.Jmbg + "%'"));
                RemoveColumns(getAllReservations.dgvReservations);
            }
            else
                boxService.Show("Ucitvanje rezervacije", "Sistem ne moze da prikaze podatke o rezervaciji");
        }
        private void GetAllReservations()
        {
            Reservations = new ObservableCollection<Rezervacija>(Communication.Instance.GetAllReservationsBySearch("jmbg LIKE '%" + Communication.Instance.user.Jmbg + "%'"));
            SelectedReservation = Reservations.FirstOrDefault();
            RemoveColumns(getAllReservations.dgvReservations);
        }
        private void SearchReservations()
        {
            Reservations = new ObservableCollection<Rezervacija>(Communication.Instance.GetAllReservationsBySearch("jmbg LIKE '%" + SearchStringReservations + "%'"));
            RemoveColumns(getAllReservations.dgvReservations);
        }


        //Helper methods
        private void RemoveColumns(DataGrid dgvDogadjaji)
        {
            if(dgvDogadjaji != null)
            {
                if (getAllEvents?.dgvEvents == dgvDogadjaji)
                {
                    foreach (var item in dgvDogadjaji.Columns)
                    {
                        if (item.Header.ToString() == "DogadjajId")
                        {
                            dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                        }
                        if (item.Header.ToString() == "TipDogadjajaId")
                        {
                            dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                        }
                        if (item.Header.ToString() == "AdminId")
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
                else if (getAllReservations?.dgvReservations == dgvDogadjaji)
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
                else if (createAndUpdateReservation?.dgvComponentes == dgvDogadjaji)
                {
                    foreach (var item in dgvDogadjaji.Columns)
                    {
                        if (item.Header.ToString() == "RezervacijaId")
                        {
                            dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                        }
                        if (item.Header.ToString() == "Status")
                        {
                            dgvDogadjaji.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                        }
                        if (item.Header.ToString() == "DogadjajId")
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
        }
    }
}
