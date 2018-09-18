using OAFramework.RocketSockets;
using OAFramework.RocketSockets.messages;
using System;
using System.Net;
using System.Net.Sockets;

namespace RocketSocketsTestUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any, 1106);

            Console.Title = EndPoint.ToString();

            Hosting hosting = new Hosting();

            hosting.Connected += Socket_Connected;
            hosting.Disconnected += Socket_Disconnected;
            hosting.Received += Socket_Received;

            hosting.Bind(EndPoint);
            hosting.Launch(ProtocolType.Tcp);

            //hosting.PushOut("");
            //hosting.KickOut("");

            Console.WriteLine("The RocketSockets was started...");
            Console.ReadKey(true);
        }

        private static void Socket_Received(Connection connection, Box inBox)
        {
            Console.WriteLine("Received message:{0}\nfrom:{1}\n" , inBox.obj, connection.RemoteEndPoint.ToString());
            connection.Send(Box.Obtain(inBox.what, "You Enter: " + inBox.obj));
        }

        private static void Socket_Disconnected(Connection connection)
        {
            Console.WriteLine("Disconnected from:{0}\n",connection.RemoteEndPoint.ToString());
        }

        private static void Socket_Connected(Connection connection)
        {
            Console.WriteLine("New connected from:{0}\n",connection.RemoteEndPoint.ToString());
        }
    }
}