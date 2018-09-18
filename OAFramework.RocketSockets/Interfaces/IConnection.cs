using OAFramework.RocketSockets.messages;
using System.Net;

namespace OAFramework.RocketSockets.Interfaces
{
    interface IConnection
    {
        long PID { get; }
        string Token { get; set; }
        string NetIdentifier { get; }
        IPEndPoint LocalEndPoint { get;}
        IPEndPoint RemoteEndPoint { get;}

        void Start();
        void Send(Box box);
        Box SendTwoWay(Box box);
    }
}
