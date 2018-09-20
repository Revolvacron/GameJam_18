using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CopyHelper
{
    public static T DeepCopy<T>(T item)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, item);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }
    }
}