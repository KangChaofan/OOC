using System.Runtime.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;

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
            return string.Join(",", array);
        }

        public static string FromArrayList<T>(List<T[]> array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T[] row in array)
            {
                sb.Append(FromArray(row));
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public static string[] ToArray(string s)
        {
            return s.Split(',');
        }

        public static List<string[]> ToArrayList(string s)
        {
            List<string[]> lines = new List<string[]>();
            foreach (string line in s.Split('\n')) {
                if (line.Length == 0) continue;
                lines.Add(line.Split(','));
            }
            return lines;
        }
    }
}