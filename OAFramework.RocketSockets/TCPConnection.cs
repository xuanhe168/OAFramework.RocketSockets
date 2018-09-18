using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using OAFramework.RocketSockets.delegates;
using OAFramework.RocketSockets.loggers;
using OAFramework.RocketSockets.messages;
using OAFramework.RocketSockets.values;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace OAFramework.RocketSockets
{
    public class TCPConnection : Connection
    {
        public event ConnectedEventHandler Connected;
        public event ReceivedEventHandler Received;
        public event DisconnectedEventHandler Disconnected;

        public TCPConnection(Socket socket,long pid)
        {
            this._pid = pid;
            this.Token = string.Empty;
            this._Socket = socket;
            this._NetIdentifier = Guid.NewGuid().ToString("N");
            this._LocalEndPoint = socket.LocalEndPoint as IPEndPoint;
            this._RemoteEndPoint = socket.RemoteEndPoint as IPEndPoint;
        }

        public override void Start()
        {
            if (Connected != null) Connected(this);
            new Thread(() => {
                while (Socket != null && Socket.Connected)
                {
                    byte[] buffer = new byte[sizeof(int)];
                    try
                    {
                        Socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                        int size = BitConverter.ToInt32(buffer, 0);
                        buffer = new byte[size];
                        Socket.Receive(buffer);
                        string text = Encoding.UTF8.GetString(buffer);
                        if (Received != null) Received(this, Box.FromString(text));
                    }
                    catch (Exception e)
                    {
                        Log.d(e.ToString());
                        if(Disconnected != null) Disconnected(this);
                        Socket.Close();
                    }
                }
            }).Start();
        }

        public override void Send(Box box)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(box.ToString());
                int size = buffer.Length;
                buffer = BitConverter.GetBytes(size);
                Socket.Send(buffer);
                buffer = Encoding.UTF8.GetBytes(box.ToString());
                Socket.Send(buffer);
            }
            catch (Exception e)
            {
                Log.d(e.ToString());
                if (Disconnected != null) Disconnected(this);
            }
        }

        public override Box SendTwoWay(Box sendBox)
        {
            throw new NotImplementedException();
        }

        public override void PushedOut() => Send(Box.Obtain(Values.PUSHEDOUT, "You have been squeezed out of the system."));

        public override void KickOut() => Send(Box.Obtain(Values.KICKOUT, "You have been kicked off the system."));
    }
}
