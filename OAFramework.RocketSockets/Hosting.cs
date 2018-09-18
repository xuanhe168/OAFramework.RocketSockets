using OAFramework.RocketSockets.delegates;
using OAFramework.RocketSockets.enums;
using OAFramework.RocketSockets.Interfaces;
using OAFramework.RocketSockets.messages;
using OAFramework.RocketSockets.values;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace OAFramework.RocketSockets
{
    public class Hosting : IHosting
    {
        #region Private fields
        private State _State { get; set; }
        private ProtocolType _Protocol { get; set; }
        private Socket _Socket { get; set; }
        private IPEndPoint _LocalEndPoint { get; set; }
        private List<Connection> _Connections { get; set; }
        #endregion

        #region Public fields
        public int MaximumConnections { get; set; }
        public ProtocolType Protocol { get => _Protocol; }
        public IPEndPoint LocalEndPoint { get => _LocalEndPoint; }
        public List<Connection> Connections { get => _Connections; }
        #endregion

        #region Public events
        public event ConnectedEventHandler Connected;
        public event ReceivedEventHandler Received;
        public event DisconnectedEventHandler Disconnected;
        #endregion

        #region Private members
        private void Connection_Disconnected(Connection connection) => Connections.Remove(connection);

        private void Connection_Received(Connection connection, Box inbox)
        {
            switch (inbox.what) {
                case Values.MESSAGE:
                    CallReceived(connection, inbox);
                    break;
                case Values.LOGIN:
                    LoginValidation(connection, inbox);
                    break;
                case Values.KICKOUT:
                    KickOut(inbox.obj);
                    break;
                case Values.PUSHEDOUT:
                    PushOut(inbox.obj);
                    break;
                case Values.FORWARD:
                    ForwardMessage(connection, inbox);
                    break;
            }
        }
        private void LoginValidation(Connection connection, Box inbox)
        {
            var oldConnection = Connections.Find(what => what.Token.Equals(inbox.obj));
            if (oldConnection != null) oldConnection.PushedOut();
            else connection.Token = inbox.obj;
        }
        private void ForwardMessage(Connection connection, Box inbox)
        {
            ForwardBox forward = ForwardBox.FromString(inbox.obj);
            Connection dest = Connections.Find(what => what.Token.Equals(forward.ReceiverToken));
            if (dest != null) dest.Send(Box.Obtain(Values.FORWARD, forward.ToString()));
        }
        private void CallReceived(Connection connection, Box inbox)
        {
            if (Received != null) Received(connection, inbox);
        }
        #endregion

        #region Public members
        public void Bind(IPEndPoint _IPEndPoint)
        {
            _LocalEndPoint = _IPEndPoint;
            MaximumConnections = 1000;
        }

        public void Launch(ProtocolType protocol)
        {
            _Protocol = protocol;
            _State = State.On;
            _Connections = new List<Connection>();
            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocol);
            _Socket.Bind(_LocalEndPoint);
            _Socket.Listen(MaximumConnections);

            new Thread(() => {
                while (_State == State.On)
                {
                    var socket = _Socket.Accept();
                    TCPConnection connection = new TCPConnection(socket, Connections.Count + 1);
                    connection.Connected += Connected;
                    connection.Received += Connection_Received;
                    connection.Disconnected += Disconnected;
                    connection.Disconnected += Connection_Disconnected;
                    connection.Start();
                    Connections.Add(connection);
                }
            }).Start();
        }

        public void Shutdown(Shutdown how)
        {
            if(how == enums.Shutdown.Safe)
            {
                while(Connections.Count > 0)
                {
                    int index = Connections.Count - 1;
                    Connections[index].Socket.Shutdown(SocketShutdown.Both);
                    Connections[index].Socket.Close();
                    Connections.RemoveAt(index);
                }
            }
            _Socket.Close();
            _Socket = null;
            _State = State.Off;
        }

        public void KickOut(string Token)
        {
            Connection oldConnection = Connections.Find(what => what.Token.Equals(Token));
            if (oldConnection != null) oldConnection.KickOut();
        }

        public void PushOut(string Token)
        {
            Connection oldConnection = Connections.Find(what => what.Token.Equals(Token));
            if (oldConnection != null) oldConnection.PushedOut();
        }
        #endregion
    }
}