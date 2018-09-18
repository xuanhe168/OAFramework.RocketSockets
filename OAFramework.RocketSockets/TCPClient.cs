using OAFramework.RocketSockets.delegates;
using OAFramework.RocketSockets.enums;
using OAFramework.RocketSockets.Interfaces;
using OAFramework.RocketSockets.messages;
using OAFramework.RocketSockets.values;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace OAFramework.RocketSockets
{
    public class TCPClient : ITCPClient
    {
        private State _State { get; set; }
        private string _Token { get; set; }
        private Socket _Socket { get; set; }
        private IPEndPoint _RemoteEndPoint { get; set; }

        public string Token { get => _Token;}
        public Socket Socket { get => _Socket; }
        public IPEndPoint RemoteEndPoint { get => _RemoteEndPoint; }

        public event ReceivedMessageFromFriendEventHandler ReceivedMessageFromFriend;
        public event ReceivedMessageFromHostEventHandler ReceivedMessageFromHost;
        public event PushedOutEventHandler PushedOut;
        public event KickOutEventHandler KickedOut;

        private void FromHost(Box inbox)
        {
            if (ReceivedMessageFromFriend != null) ReceivedMessageFromHost(inbox.obj);
        }

        private void FromFriend(Box inbox)
        {
            ForwardBox forward = ForwardBox.FromString(inbox.obj);
            if(ReceivedMessageFromFriend != null)ReceivedMessageFromFriend()
        }

        private void PushOut()
        {
            if (PushedOut != null) PushedOut();
        }

        private void KickOut()
        {
            if (KickedOut != null) KickedOut();
        }

        private void Processer(Box inbox)
        {
            switch (inbox.what) {
                case Values.MESSAGE:
                    FromHost(inbox);
                    break;
                case Values.FORWARD:
                    FromFriend(inbox);
                    break;
                case Values.PUSHEDOUT:
                    PushOut();
                    break;
                case Values.KICKOUT:
                    KickOut();
                    break;
            }
        }

        public void Init(IPEndPoint _RemoteEndPoint, string _Token)
        {
            _State = State.On;
            this._Token = _Token;
            this._RemoteEndPoint = _RemoteEndPoint;
            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try {
                _Socket.Connect(_RemoteEndPoint);
                new Thread(() => {
                    while (_State == State.On)
                    {
                        byte[] buffer = new byte[sizeof(int)];
                        _Socket.Receive(buffer, 0, buffer.Length,SocketFlags.None);
                        int size = BitConverter.ToInt32(buffer, 0);
                        buffer = new byte[size];
                        _Socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                        string xml = Encoding.UTF8.GetString(buffer);
                        Box inbox = Box.FromString(xml);
                        Processer(inbox);
                    }
                }).Start();
            }
            catch(Exception e) { throw e; }
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }

        public void Send(string token, string message)
        {
            throw new NotImplementedException();
        }

        public void Shutdown(Shutdown how)
        {
            throw new NotImplementedException();
        }
    }
}
