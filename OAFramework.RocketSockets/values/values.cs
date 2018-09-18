#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace OAFramework.RocketSockets.values
{
    public static class Values
    {
        /// <summary>
        /// 普通消息。
        /// Ordinary message.
        /// </summary>
        public const int MESSAGE = 1;
        /// <summary>
        /// 客户端登录消息。
        /// Client login of the message.
        /// </summary>
        public const int LOGIN = 2;
        /// <summary>
        /// 踢下线消息。
        /// Kick-out message.
        /// </summary>
        public const int KICKOUT = 3;
        /// <summary>
        /// Being squeezed off the line.
        /// 被几下线了。
        /// </summary>
        public const int PUSHEDOUT = 4;
        /// <summary>
        /// Message forward.
        /// 转发消息。
        /// </summary>
        public const int FORWARD = 5;
    }
}
