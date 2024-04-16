using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon;
using TicketingCommon.Communication;
using TicketingCommon.Model;

namespace TicketingServer
{
    internal class ClientHandler
    {
        private Socket socket;
        private Sender sender;
        private Receiver receiver;
        bool kraj = false;
        public string AdministratorEmail { get; set; }
        public string UserEmail { get; set; }
        public static List<ClientHandler> CurrentClients = new List<ClientHandler>();

        public ClientHandler(Socket socket)
        {
            this.socket = socket;
            sender = new Sender(socket);
            receiver = new Receiver(socket);
        }

        public async void HandleRequests(object obj)
        {
            try
            {
                while (!kraj)
                {
                    Request req = receiver.Receive<Request>();
                    Response res = new Response();

                    try
                    {
                        switch (req.Operacija)
                        {
                            case Operation.PrijavaNaSistem:
                                {
                                    res = await Controller.Instance.Login(req.Parameter);

                                    if (res.SignalUspesno)
                                    {
                                        if (res.Result is Administrator)
                                            AdministratorEmail = ((Administrator)res.Result).Email;
                                        else
                                            UserEmail = ((Korisnik)res.Result).Email;
                                    }

                                    break;
                                }
                            case Operation.OdjavaSaSistema:
                                {
                                    Controller.Instance.Logout(req.Parameter);
                                    res.SignalUspesno = true;
                                    if (CurrentClients.Contains(this)) CurrentClients.Remove(this);
                                    break;
                                }
                            case Operation.DodajKorisnika:
                                {
                                    await Controller.Instance.AddUser((Korisnik)req.Parameter);
                                    res.Message = "Uspesno ste kreirali korisnika";
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.IzmeniRezervaciju:
                                {
                                    await Controller.Instance.UpdateReservation((Rezervacija)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.KreirajDogadjaj:
                                {
                                    await Controller.Instance.AddEvent((SportskiDogadjaj)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.ObrisiDogadjaj:
                                {
                                    await Controller.Instance.DeleteEvent((SportskiDogadjaj)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.ObrisiKorisnika:
                                {
                                    await Controller.Instance.DeleteUser((Korisnik)req.Parameter);
                                    res.Message = "Uspesno ste obrisali korisnika";
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.PronadjiDogadjaje:
                                {
                                    res.Result = await Controller.Instance.FindEvents((string)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.PronadjiKorisnike:
                                {
                                    res.Result = await Controller.Instance.FindUsers((string)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.PronadjiRezervacije:
                                {
                                    res.Result = await Controller.Instance.FindReservations((string)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.SacuvajRezervaciju:
                                {
                                    await Controller.Instance.AddReservation((Rezervacija)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.UcitajDogadjaje:
                                {
                                    res.Result = await Controller.Instance.GetAllEvents();
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.UcitajKorisnika:
                                {
                                    res.Result = await Controller.Instance.GetUser((Korisnik)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.UcitajKorisnike:
                                {
                                    res.Result = await Controller.Instance.GetAllUsers();
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.UcitajMesta:
                                {
                                    res.Result = await Controller.Instance.GetAllPlaces();
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.UcitajTipoveDogadjaja:
                                {
                                    res.Result = await Controller.Instance.GetAllEventTypes();
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.UcitajRezervacije:
                                {
                                    res.Result = await Controller.Instance.GetAllReservations();
                                    res.SignalUspesno = true;
                                    break;
                                }
                            case Operation.UcitajRezervaciju:
                                {
                                    res.Result = await Controller.Instance.GetReservation((Rezervacija)req.Parameter);
                                    res.SignalUspesno = true;
                                    break;
                                }

                        }

                    }
                    catch (ArgumentException e)
                    {
                        res.Message = e.Message;
                        Debug.WriteLine("------ClientHandler " + e.Message);
                    }
                    catch (Exception e)
                    {
                        res.Message = "Neuspesno izvrsena operacija na serveru!";
                        Debug.WriteLine("------ClientHandler " + e.Message);
                    }
                    finally
                    {
                        sender.Send(res);
                    }
                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("------ClientHandler " + e.Message);
            }
            catch (IOException e)
            {
                Debug.WriteLine("------ClientHandler " + e.Message);
            }
            catch (SerializationException e)
            {
                Debug.WriteLine("------ClientHandler " + e.Message);
            }
        }

        internal void Stop()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
    }
}
