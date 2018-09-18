#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using OAFramework.RocketSockets.messages;
using System;

namespace OAFramework.RocketSockets.delegates
{
    public delegate void ConnectedEventHandler(Connection connection);
    public delegate void ReceivedEventHandler(Connection connection, Box inBox);
    public delegate void DisconnectedEventHandler(Connection connection);

    public delegate void ReceivedMessageFromHostEventHandler(String message);
    public delegate void ReceivedMessageFromFriendEventHandler(String token, String message);
    public delegate void PushedOutEventHandler();
    public delegate void KickOutEventHandler();
}
