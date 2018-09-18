#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using OAFramework.RocketSockets.utils;

namespace OAFramework.RocketSockets.messages
{
    public class Box
    {
        public int what { get; set; }
        public string obj { get; set; }

        public static Box Obtain(int what,string obj)
        {
            Box box = new Box();
            box.what = what;
            box.obj = obj;
            return box;
        }

        public override string ToString() => OAXmlConverter.ToString<Box>(this);
        public static Box FromString(string xml) => OAXmlConverter.FromXml<Box>(xml);
    }
}