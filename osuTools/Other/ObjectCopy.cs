using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace osuTools.ExtraMethods
{
    public static class ObjectCopy
    {
        public static object DeepCopy(object obj)
        {
            MemoryStream mstream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mstream,obj);
            mstream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(mstream);
        }
        public static T DeepCopy<T>(T obj)
        {
            MemoryStream mstream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mstream, obj);
            mstream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(mstream);
        }
    }
}