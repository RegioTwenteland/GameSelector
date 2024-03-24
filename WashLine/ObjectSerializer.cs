using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System;

namespace WashLine
{
    internal static class ObjectSerializer
    {
        private const byte separator = 0x24; // '$'

        public static byte[] Serialize(object obj)
        {
            if (obj is null) return null;

            var type = obj.GetType();

            using (var memoryStream = new MemoryStream())
            using (var reader = new StreamReader(memoryStream))
            {
                var serializer = new DataContractSerializer(type);
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                var typeName = Encoding.UTF8.GetBytes(type.FullName);
                var data = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                return typeName.Concat(new byte[] { separator }).Concat(data).ToArray();
            }
        }

        private static Type FindFirstType(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var theType = assembly.GetType(name);
                if (theType != null)
                {
                    return theType;
                }
            }

            return null;
        }

        public static object Deserialize(byte[] blob)
        {
            if (blob is null) return null;
            if (blob.Length == 0) return null;

            var typeBlob = blob.TakeWhile(x => x != separator).ToArray();
            var type = FindFirstType(Encoding.UTF8.GetString(typeBlob));

            blob = blob.Skip(typeBlob.Length + 1).ToArray();

            using (var stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(blob));
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractSerializer(type);
                return deserializer.ReadObject(stream);
            }
        }
    }
}
