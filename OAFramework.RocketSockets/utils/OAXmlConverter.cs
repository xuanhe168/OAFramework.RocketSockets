using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace OAFramework.RocketSockets.utils
{
    public static class OAXmlConverter
    {
        public static string ToString<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, obj);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string result = reader.ReadToEnd();
            writer.Close();
            return result;
        }
        public static T FromXml<T>(string xml)
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            result = (T)serializer.Deserialize(reader);
            return result;
        }
    }
}
