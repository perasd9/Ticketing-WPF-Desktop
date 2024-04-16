using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TicketingCommon.Communication
{
    public class Sender
    {
        Socket _soket;
        NetworkStream _soketStream;
        BinaryFormatter _formatter;

        public Sender(Socket soket)
        {
            _soket = soket;
            _soketStream = new NetworkStream(_soket);
            _formatter = new BinaryFormatter();
        }

        public void Send<T>(T parameter)
        {
            _formatter.Serialize(_soketStream, parameter);
        }
    }
}
