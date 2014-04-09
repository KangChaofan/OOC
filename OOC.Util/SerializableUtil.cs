using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OOC.Util
{
    public static class SerializableUtil
    {
        public static void DeepCopy(object source, out object target)
        {
            var ms = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(ms, source);
            ms.Seek(0, SeekOrigin.Begin);
            target = bf.Deserialize(ms);
            ms.Close();
        }

        public static void ShallowCopy(object source, out object target)
        {
            //TODO fix me.
            target = source;
        }
    }
}