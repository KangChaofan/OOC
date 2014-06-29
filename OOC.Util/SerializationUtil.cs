using System.Runtime.Serialization;
using System.IO;
using System.Text;

namespace OOC.Util
{
    public class SerializationUtil
    {
        public static string Serialize(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(string xmlString)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    return (T)serializer.ReadObject(ms);
                }
            }
            catch
            {
                return default(T);
            }
        }

        public static string FromArray<T>(T[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T element in array)
            {
                sb.Append(element);
                sb.Append(',');
            }
            return sb.ToString();
        }

        public static string[] ToArray(string s)
        {
            return s.Split(',');
        }
    }
}