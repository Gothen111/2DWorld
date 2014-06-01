using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameLibrary.Util
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

        public static string SerializeObjectToString(ISerializable objectToSerialize)
        {
            string result = "";
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    using (GZipStream gZipStream = new GZipStream(streamReader.BaseStream, CompressionMode.Compress))
                    {
                        BinaryFormatter bFormatter = new BinaryFormatter();
                        bFormatter.Serialize(gZipStream.BaseStream, objectToSerialize);
                        result = gZipStream.ToString();
                    }
                }
            }

            return result;
        }

        public static ISerializable DeserializeObjectFromString(string objectToDeserialize)
        {
            ISerializable objectToSerialize;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter stream = new StreamWriter(memoryStream))
                {
                    stream.Write(objectToDeserialize);
                    using (var gZipStream = new GZipStream(stream.BaseStream, CompressionMode.Decompress))
                    {
                        BinaryFormatter bFormatter = new BinaryFormatter();
                        objectToSerialize = (ISerializable)bFormatter.Deserialize(gZipStream.BaseStream);
                    }
                }
            }
            return objectToSerialize;
        }
    }
}
