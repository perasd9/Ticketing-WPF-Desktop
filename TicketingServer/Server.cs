using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicketingServer
{
    internal class Server
    {
        Socket serverSocket;
        public void Start(object o)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings.Get("IP")), int.Parse(ConfigurationManager.AppSettings.Get("PORT")));
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);

            try
            {
                while (true)
                {
                    Socket client = serverSocket.Accept();
                    ClientHandler ch = new ClientHandler(client);
                    ClientHandler.CurrentClients.Add(ch);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ch.HandleRequests));
                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("------" + e.Message);
            }
        }

        internal void Stop()
        {
            serverSocket.Close();
        }
    }
}
