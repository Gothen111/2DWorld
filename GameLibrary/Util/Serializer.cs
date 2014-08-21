using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameLibrary.Util
{
    public class Serializer
    {
        public static void SerializeObject(string filename, ISerializable objectToSerialize)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                using (var gZipStream = new GZipStream(stream, CompressionMode.Compress))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(gZipStream.BaseStream, objectToSerialize);
                }
            }
        }

        public static ISerializable DeSerializeObject(string filename)
        {
            ISerializable objectToSerialize;
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    objectToSerialize = (ISerializable)bFormatter.Deserialize(gZipStream.BaseStream);
                }
            }
            return objectToSerialize;
        }

        public static string SerializeObjectToString(ISerializable objectToSerialize)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };
            return Compress(Newtonsoft.Json.JsonConvert.SerializeObject(objectToSerialize, settings));
        }

        public static T DeserializeObjectFromString<T>(string objectToDeserialize)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Decompress(objectToDeserialize), settings);
        }

        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
