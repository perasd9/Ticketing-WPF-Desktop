using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Windows;
using TicketingCommon;
using TicketingCommon.Communication;
using TicketingCommon.Model;

namespace TicketingClient.Komunikacija
{
    internal class Communication
    {
        private static Communication instance;
        private static readonly object InstanceLock = new object();

        public static Communication Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                            instance = new Communication();
                    }
                }
                return instance;
            }
        }
        private Communication()
        {

        }

        private Socket socket;
        private Sender sender;
        private Receiver receiver;
        public Administrator admin;
        public Korisnik user;

        public bool Connect()
        {
            try
            {
                if (socket == null)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2222);
                    socket.Connect(endPoint);
                    sender = new Sender(socket);
                    receiver = new Receiver(socket);
                }
                return true;
            }
            catch (SocketException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                return false;
            }
        }
        internal void CloseSockets()
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.OdjavaSaSistema });
                bool t = receiver.Receive<Response>().SignalUspesno;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        internal List<SportskiDogadjaj> GetAllEvents()
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.UcitajDogadjaje });
                return (List<SportskiDogadjaj>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<SportskiDogadjaj>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<SportskiDogadjaj>();
            }
        }

        internal Response CreateUser(Korisnik korisnik)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.DodajKorisnika, Parameter = korisnik });
                return receiver.Receive<Response>();
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return null;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return null;
            }
        }

        internal List<Mesto> GetAllPlaces()
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.UcitajMesta });
                return (List<Mesto>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<Mesto>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<Mesto>();
            }
        }

        internal List<Rezervacija> GetAllReservations()
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.UcitajRezervacije });
                return (List<Rezervacija>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<Rezervacija>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<Rezervacija>();
            }
        }

        internal List<Rezervacija> GetAllReservationsBySearch(string searchString)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.PronadjiRezervacije, Parameter = searchString });
                return (List<Rezervacija>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<Rezervacija>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<Rezervacija>();
            }
        }

        internal List<Korisnik> GetAllUsers()
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.UcitajKorisnike });
                return (List<Korisnik>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<Korisnik>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<Korisnik>();
            }
        }

        internal List<Korisnik> GetUsersBySearch(string searchString)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.PronadjiKorisnike, Parameter = searchString });
                return (List<Korisnik>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<Korisnik>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<Korisnik>();
            }
        }

        internal Response DeleteUser(Korisnik korisnik)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.ObrisiKorisnika, Parameter = korisnik });
                return receiver.Receive<Response>();
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return null;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return null;
            }
        }

        internal Korisnik LoadUser(Korisnik korisnik)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.UcitajKorisnika, Parameter = korisnik });
                return (Korisnik)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new Korisnik();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new Korisnik();
            }
        }

        internal List<SportskiDogadjaj> GetEventsBySearch(string searchString)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.PronadjiDogadjaje, Parameter = searchString });
                return (List<SportskiDogadjaj>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<SportskiDogadjaj>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<SportskiDogadjaj>();
            }
        }

        internal bool DeleteEvent(SportskiDogadjaj selectedEvent)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.ObrisiDogadjaj, Parameter = selectedEvent });
                return receiver.Receive<Response>().SignalUspesno;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return false;
            }
        }

        internal List<TipDogadjaja> GetAllEventTypes()
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.UcitajTipoveDogadjaja });
                return (List<TipDogadjaja>)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new List<TipDogadjaja>();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new List<TipDogadjaja>();
            }
        }

        internal bool CreateEvent(SportskiDogadjaj sportskiDogadjaj)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.KreirajDogadjaj, Parameter = sportskiDogadjaj });
                return receiver.Receive<Response>().SignalUspesno;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return false;
            }
        }

        internal Response LoginAdmin(string email, byte[] password)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.PrijavaNaSistem, Parameter = new Administrator() { Email = email, Sifra = password } });
                return receiver.Receive<Response>();
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return null;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return null;
            }
        }
        internal bool Logout()
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.OdjavaSaSistema });
                return receiver.Receive<Response>().SignalUspesno;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return false;
            }
        }

        internal Response LoginUser(string email, byte[] password)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.PrijavaNaSistem, Parameter = new Korisnik() { Email = email, Sifra = password } });
                return receiver.Receive<Response>();
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return null;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return null;
            }
        }

        internal void SetAdmin(Administrator admin)
        {
            this.admin = admin;
        }

        internal void SetUser(Korisnik kor)
        {
            this.user = kor;
        }

        internal Rezervacija LoadReservation(Rezervacija selectedReservation)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.UcitajRezervaciju, Parameter = selectedReservation });
                return (Rezervacija)receiver.Receive<Response>().Result;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return new Rezervacija();
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return new Rezervacija();
            }
        }

        internal bool SaveReservation(Rezervacija rezervacija)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.SacuvajRezervaciju, Parameter = rezervacija });
                return receiver.Receive<Response>().SignalUspesno;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return false;
            }
        }

        internal bool UpdateReservation(Rezervacija rezervacija)
        {
            try
            {
                sender.Send(new Request() { Operacija = Operation.IzmeniRezervaciju, Parameter = rezervacija });
                return receiver.Receive<Response>().SignalUspesno;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze uspesno da vrati podatke");
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine("------Communication " + e.Message);
                MessageBox.Show("Server ne moze da primi zahtev");
                return false;
            }
        }
    }
}
