using OAFramework.RocketSockets.delegates;
using OAFramework.RocketSockets.enums;
using System;
using System.Net;

namespace OAFramework.RocketSockets.Interfaces
{
    interface ITCPClient
    {
        /// <summary>
        /// 发送消息给服务器。
        /// </summary>
        /// <param name="message">文本消息</param>
        void Send(String message);
        /// <summary>
        /// 发送消息给朋友。
        /// </summary>
        /// <param name="token">标识</param>
        /// <param name="message">文本消息</param>
        void Send(String token, String message);
        /// <summary>
        /// 收到新消息从朋友。
        /// </summary>
        event ReceivedMessageFromHostEventHandler ReceivedMessageFromFriend;
        /// <summary>
        /// 收到消息从服务器。
        /// </summary>
        event ReceivedMessageFromHostEventHandler ReceivedMessageFromHost;
        /// <summary>
        /// 已在别处登录。
        /// </summary>
        event PushedOutEventHandler PushedOut;
        /// <summary>
        /// 被系统强制踢下线。
        /// </summary>
        event KickOutEventHandler KickedOut;
        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="_RemoteEndPoint">服务器地址</param>
        /// <param name="_Token">标识</param>
        void Init(IPEndPoint _RemoteEndPoint, String _Token);
        /// <summary>
        /// 关闭连接。
        /// </summary>
        /// <param name="how">怎样关闭的</param>
        void Shutdown(Shutdown how);
    }
}
