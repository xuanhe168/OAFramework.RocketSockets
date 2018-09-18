#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using OAFramework.RocketSockets.utils;

namespace OAFramework.RocketSockets.messages
{
    public class ForwardBox
    {
        public string SenderToken;
        public string ReceiverToken;
        public string Content;
        public static ForwardBox Obtain(string Sender,string Receiver,string Content)
        {
            ForwardBox box = new ForwardBox();
            box.SenderToken = Sender;
            box.ReceiverToken = Receiver;
            box.Content = Content;
            return box;
        }

        public override string ToString() => OAXmlConverter.ToString<ForwardBox>(this);
        public static ForwardBox FromString(string xml) => OAXmlConverter.FromXml<ForwardBox>(xml);
    }
}
