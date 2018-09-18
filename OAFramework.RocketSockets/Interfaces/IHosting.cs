using OAFramework.RocketSockets.delegates;
using OAFramework.RocketSockets.enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace OAFramework.RocketSockets.Interfaces
{
    interface IHosting
    {
        IPEndPoint LocalEndPoint { get; }
        List<Connection> Connections { get; }

        event ConnectedEventHandler Connected;
        event ReceivedEventHandler Received;
        event DisconnectedEventHandler Disconnected;

        void Bind(IPEndPoint _IPEndPoint);
        void Launch(ProtocolType protocol);
        void Shutdown(Shutdown how);

        void KickOut(String Token);
        void PushOut(String Token);
    }
}