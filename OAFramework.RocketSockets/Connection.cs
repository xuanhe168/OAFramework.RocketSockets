using OAFramework.RocketSockets.Interfaces;
using OAFramework.RocketSockets.messages;
using System.Net;
using System.Net.Sockets;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace OAFramework.RocketSockets
{
    public abstract class Connection:IConnection
    {
        protected long _pid { get; set; }
        protected Socket _Socket { get; set; }
        protected string _NetIdentifier { get; set; }
        protected IPEndPoint _LocalEndPoint { get; set; }
        protected IPEndPoint _RemoteEndPoint { get; set; }


        public long PID { get; }
        public string Token { get; set; }
        public Socket Socket { get => _Socket; }
        public string NetIdentifier { get => _NetIdentifier; }
        public IPEndPoint LocalEndPoint { get => _LocalEndPoint; }
        public IPEndPoint RemoteEndPoint { get => _RemoteEndPoint; }

        public abstract void Start();

        public abstract void Send(Box box);
        public abstract Box SendTwoWay(Box box);
        public abstract void PushedOut();
        public abstract void KickOut();
    }
}
