using OAFramework.RocketSockets.Interfaces;
using System;
using System.Net;

namespace OAFramework.RocketSockets
{
    class UDPConnection // :IConnection
    {
        public long PID => throw new NotImplementedException();

        public string Token { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string NetIdentifier => throw new NotImplementedException();

        public IPEndPoint LocalEndPoint => throw new NotImplementedException();

        public IPEndPoint RemoteEndPoint => throw new NotImplementedException();

    }
}
