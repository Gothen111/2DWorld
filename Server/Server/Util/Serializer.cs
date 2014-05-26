using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server.Util
{
    public class Serializer
    {
        public static void SerializeObject(string filename, ISerializable objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            var gZipStream = new GZipStream(stream, CompressionMode.Compress);
            
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(gZipStream.BaseStream, objectToSerialize);
            stream.Close();
        }

        public static ISerializable DeSerializeObject(string filename)
        {
            ISerializable objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            var gZipStream = new GZipStream(stream, CompressionMode.Decompress);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (ISerializable)bFormatter.Deserialize(gZipStream.BaseStream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
