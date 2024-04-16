using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingCommon;
using TicketingCommon.Model;
using TicketingServer.SystemOperations;
using TicketingServer.SystemOperations.AdministratorSO;
using TicketingServer.SystemOperations.KorisnikSO;
using TicketingServer.SystemOperations.MestoSO;
using TicketingServer.SystemOperations.RezervacijaSO;
using TicketingServer.SystemOperations.SportskiDogadjajSO;
using TicketingServer.SystemOperations.TipDogadjajaSO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TicketingServer
{
    internal class Controller
    {
        private static Controller instance;
        private static readonly object InstanceLock = new object();
        private static Administrator currentAdmin;
        private static Korisnik currentUser;

        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                            instance = new Controller();
                    }
                }
                return instance;
            }
        }
        private Controller()
        {

        }

        public async Task<Response> Login(object o)
        {
            AbstractSystemOperation login;
            Response res = new Response();
            if (o is Administrator)
            {
                foreach (var item in ClientHandler.CurrentClients)
                {
                    if (((Administrator)o).Email == item.AdministratorEmail)
                    {
                        res.Message = "Vec postoji prijavljeni korisnik sa tim kredencijalima";
                        res.Result = null;
                        res.SignalUspesno = false;

                        return res;
                    }
                }
                login = new LoginAdmin(o as Administrator);
                await login.ExecuteOperation();

                if(MatchSHA1(((Administrator)o).Sifra, ((LoginAdmin)login)?.admin?.Sifra)){
                    res.Result = ((LoginAdmin)login).admin;
                    res.SignalUspesno = res.Result != null;
                }
                else
                {
                    res.SignalUspesno = false;
                }
                res.Message = res.SignalUspesno ? "Uspesno ste se prijavili na sistem!" : "Nije moguce prijavljivanje na sistem!";
                return res;
            }
            else
            {
                foreach (var item in ClientHandler.CurrentClients)
                {
                    if (((Korisnik)o).Email == item.UserEmail)
                    {
                        res.Message = "Vec postoji prijavljeni korisnik sa tim kredencijalima";
                        res.Result = null;
                        res.SignalUspesno = false;

                        return res;
                    }
                }
                login = new LoginUser(o as Korisnik);
                await login.ExecuteOperation();

                if (MatchSHA1(((Korisnik)o).Sifra, ((LoginUser)login)?.korisnik?.Sifra))
                {
                    res.Result = ((LoginUser)login).korisnik;
                    res.SignalUspesno = res.Result != null;
                }
                else
                {
                    res.SignalUspesno = false;
                }
                res.Message = res.SignalUspesno ? "Uspesno ste se prijavili na sistem!" : "Nije moguce prijavljivanje na sistem!";
                return res;
            }

        }
        //SHA
        private static bool MatchSHA1(byte[] p1, byte[] p2)
        {
            bool result = false;
            if (p1 != null && p2 != null)
            {
                if (p1.Length == p2.Length)
                {
                    result = true;
                    for (int i = 0; i < p1.Length; i++)
                    {
                        if (p1[i] != p2[i])
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        //SHA
        public void Logout(object o)
        {
            if (o is Administrator) currentAdmin = null;
            else currentUser = null;
        }
        public async Task<List<Korisnik>> GetAllUsers()
        {
            AbstractSystemOperation getAllUsers = new GetAllUsers(new Korisnik());
            await getAllUsers.ExecuteOperation();

            return ((GetAllUsers)getAllUsers).Users;
        }
        public async Task<List<Rezervacija>> GetAllReservations()
        {
            AbstractSystemOperation getAllReservations = new GetAllReservations(new Rezervacija());
            await getAllReservations.ExecuteOperation();

            return ((GetAllReservations)getAllReservations).Reservations;
        }
        public async Task<List<SportskiDogadjaj>> GetAllEvents()
        {
            AbstractSystemOperation getAllEvents = new GetAllEvents(new SportskiDogadjaj());
            await getAllEvents.ExecuteOperation();

            return ((GetAllEvents)getAllEvents).Events;
        }
        public async Task<List<TipDogadjaja>> GetAllEventTypes()
        {
            AbstractSystemOperation getAllEventTypes = new GetAllEventTypes(new TipDogadjaja());
            await getAllEventTypes.ExecuteOperation();

            return ((GetAllEventTypes)getAllEventTypes).EventTypes;
        }
        public async Task<List<Mesto>> GetAllPlaces()
        {
            AbstractSystemOperation getAllPlaces = new GetAllPlaces(new Mesto());
            await getAllPlaces.ExecuteOperation();

            return ((GetAllPlaces)getAllPlaces).Places;
        }
        public async Task<List<Korisnik>> FindUsers(string kriteria)
        {
            AbstractSystemOperation findUSers = new FindUsers(kriteria, new Korisnik());
            await findUSers.ExecuteOperation();

            return ((FindUsers)findUSers).Users;
        }
        public async Task<List<Rezervacija>> FindReservations(string kriteria)
        {
            AbstractSystemOperation findReservations = new FindReservations(kriteria, new Rezervacija());
            await findReservations.ExecuteOperation();

            return ((FindReservations)findReservations).Reservations;
        }
        public async Task<List<SportskiDogadjaj>> FindEvents(string kriteria)
        {
            AbstractSystemOperation findEvents = new FindEvents(kriteria, new SportskiDogadjaj());
            await findEvents.ExecuteOperation();

            return ((FindEvents)findEvents).Events;
        }
        public async Task AddUser(Korisnik user)
        {
            AbstractSystemOperation addUser = new AddUser(user);
            await addUser.ExecuteOperation();
        }
        public async Task AddReservation(Rezervacija reservation)
        {
            AbstractSystemOperation addReservation = new AddReservation(reservation);
            await addReservation.ExecuteOperation();
        }
        public async Task AddEvent(SportskiDogadjaj ev)
        {
            AbstractSystemOperation addEvent = new AddEvent(ev);
            await addEvent.ExecuteOperation();
        }
        public async Task UpdateReservation(Rezervacija reservation)
        {
            AbstractSystemOperation updateReservation = new UpdateReservation(reservation);
            await updateReservation.ExecuteOperation();
        }
        public async Task DeleteUser(Korisnik user)
        {
            AbstractSystemOperation deleteUser = new DeleteUser(user);
            await deleteUser.ExecuteOperation();
        }
        public async Task DeleteEvent(SportskiDogadjaj ev)
        {
            AbstractSystemOperation deleteEvent = new DeleteEvent(ev);
            await deleteEvent.ExecuteOperation();
        }
        internal async Task<Korisnik> GetUser(Korisnik parameter)
        {
            AbstractSystemOperation getUser = new GetUser(parameter);
            await getUser.ExecuteOperation();

            return ((GetUser)getUser).Korisnik;
        }
        internal async Task<object> GetReservation(Rezervacija parameter)
        {
            AbstractSystemOperation getReservation = new GetReservation(parameter);
            await getReservation.ExecuteOperation();

            return ((GetReservation)getReservation).Reservation;
        }
    }
}
