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
using TicketingClientWPF.View.Controls.Reservation;
using TicketingClientWPF.View.Controls.User;
using TicketingCommon;
using TicketingCommon.Model;

namespace TicketingClientWPF.ViewModel
{
    internal class LoadDetailsViewModel : ViewModelBase
    {
        private UCCreateAndUpdateReservation createAndUpdateReservation;
        private UCUserDetails userDetails;

        //Constructor
        public LoadDetailsViewModel(INotifyBoxService boxService)
        {
            this.boxService = boxService;
        }
        private readonly INotifyBoxService boxService;

        //Commands
        public RelayCommand CreateUpdateReservationCommand => new RelayCommand(execute => CreateUpdateReservation(), o => true);
        public RelayCommand DeleteUserCommand => new RelayCommand(execute => DeleteUser(), o => true);
        public RelayCommand AddTicketCommand => new RelayCommand(execute => AddTicket(), o => true);
        public RelayCommand DeleteTicketCommand => new RelayCommand(execute => DeleteTicket(), o => true);


        //Binding elements
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
        private ObservableCollection<Mesto> places = new ObservableCollection<Mesto>();
        public ObservableCollection<Mesto> Places
        {
            get { return places; }
            set
            {
                places = value;
                OnPropertyChanged(nameof(Places));
            }
        }

        private Mesto selectedPlace = new Mesto();
        public Mesto SelectedPlace
        {
            get { return selectedPlace; }
            set
            {
                selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }

        //RESERVATIONS
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


        //Control initializing
        internal UIElement CreateUCCreateAndUpdateReservation(FormMode mode, Rezervacija reservation)
        {
            createAndUpdateReservation = new UCCreateAndUpdateReservation(mode, reservation);
            Reservation = reservation;
            reservationComponents = new ObservableCollection<KomponentaRezervacije>(reservation.ListaKomponenti);
            createAndUpdateReservation.btnCreateUpdateResevation.Content = "Azuriraj";
            createAndUpdateReservation.GetAllEvents += (sender, args) => GetAllEvents();
            createAndUpdateReservation.CellEditing += (sender, args) => CellEditing(args);
            currentComponent = new KomponentaRezervacije();

            ReservationComponents = (reservation != null) ? new ObservableCollection<KomponentaRezervacije>(reservation.ListaKomponenti) : ReservationComponents;
            createAndUpdateReservation.dgvComponentes.ItemsSource = ReservationComponents;

            RemoveColumnsComponent(createAndUpdateReservation.dgvComponentes);

            return createAndUpdateReservation;
        }

        internal UIElement CreateUCUserDetails(Korisnik korisnik)
        {
            userDetails = new UCUserDetails();
            userDetails.txtLozinka.Password = korisnik.Sifra.ToString();
            places.Add(korisnik.Mesto);
            SelectedPlace = korisnik.Mesto;

            return userDetails;
        }

        //Control methods for events
        private void DeleteUser()
        {
            Response res = Communication.Instance.DeleteUser(User);

            if (res != null)
            {
                if (res.SignalUspesno)
                {
                    MainCoordinator.Instance.loadDetailsView.Close();
                }
                else
                {
                    boxService.Show("Brisanje korisnka", "Nije moguce obrisati korisnika");
                }
                    boxService.Show("Brisanje korisnika", res.Message);
            }

        }
                //update
        private void CreateUpdateReservation()
        {
            reservation.ListaKomponenti = reservationComponents.ToList();

            if (Communication.Instance.UpdateReservation(Reservation))
            {
                boxService.Show("Izmena rezervacije", "Rezervacija je uspesno izmenjena");
                reservationComponents.Clear();
                MainCoordinator.Instance.loadDetailsView.Close();
            }
            else
                boxService.Show("Izmena rezervacije", "Neuspesna izmena rezervacije");
        }
        private void AddTicket()
        {
            KomponentaRezervacije component = new KomponentaRezervacije()
            {
                BrojKarata = int.Parse(createAndUpdateReservation.txtBrojKarata.Text),
                UkupanIznos = double.Parse(createAndUpdateReservation.txtCenaKarte.Text) * int.Parse(createAndUpdateReservation.txtBrojKarata.Text),
                SportskiDogadjaj = SelectedEvent,
                DogadjajId = SelectedEvent.DogadjajId,
                Status = Status.Added
            };
            createAndUpdateReservation.txtUkupnaCena.Text = (double.Parse(createAndUpdateReservation.txtUkupnaCena.Text) + component.UkupanIznos).ToString();

            foreach (var item in ReservationComponents)
            {
                if (item.SportskiDogadjaj.Equals(component.SportskiDogadjaj) && item.Status != Status.Deleted)
                {
                    item.BrojKarata += component.BrojKarata;
                    item.UkupanIznos += component.UkupanIznos;
                    if (item.Status != Status.Added)
                        item.Status = Status.Modified;

                    createAndUpdateReservation.dgvComponentes.ItemsSource = null;
                    createAndUpdateReservation.dgvComponentes.ItemsSource = new ObservableCollection<KomponentaRezervacije>(ReservationComponents.Where(comp => comp.Status != Status.Deleted));

                    RemoveColumnsComponent(createAndUpdateReservation?.dgvComponentes);

                    return;
                }
            }
            int serialNumber  = 0;
            foreach(var item in ReservationComponents)
            {
                serialNumber++;
                if ((serialNumber != item.RbKomponente || item.Status == Status.Deleted) && serialNumber > 0)
                {
                    component.RbKomponente = serialNumber;
                    break;
                }
            }
            if (component.RbKomponente == 0) component.RbKomponente = reservationComponents.Count() + 1;
            ReservationComponents.Add(component);
            createAndUpdateReservation.dgvComponentes.ItemsSource = null;
            createAndUpdateReservation.dgvComponentes.ItemsSource = new ObservableCollection<KomponentaRezervacije>(ReservationComponents.Where(comp => comp.Status != Status.Deleted));

            RemoveColumnsComponent(createAndUpdateReservation?.dgvComponentes);

        }
        private void DeleteTicket()
        {
            var komponentaRez = CurrentComponent;

            komponentaRez.Status = Status.Deleted;
            createAndUpdateReservation.dgvComponentes.ItemsSource = null;
            createAndUpdateReservation.dgvComponentes.ItemsSource = new ObservableCollection<KomponentaRezervacije>(ReservationComponents.Where(comp => comp.Status != Status.Deleted));
            //ReservationComponents.Remove(komponentaRez);
            //ova jedna linija ispod je samo zato sto ako se desi da je current component trenutno izbrisana komponenata onda ce se current postaviti na null i dzabe smo krecili
            currentComponent = new KomponentaRezervacije();
            createAndUpdateReservation.txtUkupnaCena.Text = (double.Parse(createAndUpdateReservation.txtUkupnaCena.Text) - komponentaRez.UkupanIznos).ToString();

            RemoveColumnsComponent(createAndUpdateReservation?.dgvComponentes);
        }
        private void GetAllEvents()
        {
            Events = new ObservableCollection<SportskiDogadjaj>(Communication.Instance.GetAllEvents());
            SelectedEvent = Events.FirstOrDefault();
            RemoveColumnsComponent(createAndUpdateReservation.dgvComponentes);
        }
        private void CellEditing(EventArgs args)
        {
            reservationComponents[((DataGridCellEditEndingEventArgs)args).Row.GetIndex()].Status = Status.Modified;
        }



        //Helper methods
        private void RemoveColumnsComponent(DataGrid dgvKomponenteRezervacije)
        {
            if (createAndUpdateReservation?.dgvComponentes == dgvKomponenteRezervacije)
            {
                foreach (var item in dgvKomponenteRezervacije.Columns)
                {
                    if (item.Header.ToString() == "RezervacijaId")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "RbKomponente")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "DogadjajId")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "Status")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "BrojKarata")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].IsReadOnly = false;
                    }
                    if (item.Header.ToString() == "TableName")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "Parameters")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                    if (item.Header.ToString() == "UpdateParameters")
                    {
                        dgvKomponenteRezervacije.Columns[item.DisplayIndex].Visibility = Visibility.Hidden;
                    }
                }

            }
        }
    }
}
