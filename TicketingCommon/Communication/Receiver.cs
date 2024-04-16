using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace TicketingCommon.Communication
{
    public class Receiver
    {
        Socket _soket;
        NetworkStream _soketStream;
        BinaryFormatter _formatter;

        public Receiver(Socket soket)
        {
            _soket = soket;
            _soketStream = new NetworkStream(_soket);
            _formatter = new BinaryFormatter();
        }

        public T Receive<T>()
        {
            return (T)_formatter.Deserialize(_soketStream);
        }
    }
}
