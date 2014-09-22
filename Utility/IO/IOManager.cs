﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Utility.IO
{
    public class IOManager
    {
        public static bool CreatePath(String _Path)
        {
            if (!System.IO.Directory.Exists(_Path))
            {
                try
                {
                    System.IO.DirectoryInfo var_Info = System.IO.Directory.CreateDirectory(_Path);
                    return var_Info.Exists;
                }
                catch
                {
                    // Error!
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        public static bool SaveTextToFile(String _File, String _Text, bool _Append)
        {
            String var_Path = _File.Remove(_File.LastIndexOf("/"));

            if (CreatePath(var_Path))
            {
                try
                {
                    StreamWriter writer;

                    if (_Append)
                    {
                        writer = new StreamWriter(File.Open(_File, FileMode.Append));
                    }
                    else
                    {
                        writer = new StreamWriter(File.Open(_File, FileMode.CreateNew));
                    }

                    writer.Write(_Text);
                    writer.Flush();
                    writer.Close();
                    return true;
                }
                catch
                {
                    // Error!
                }
            }
            else
            {
                // Error!
            }
            return false;
        }

        public static bool SaveISerializeAbleObjectToFile(String _File, ISerializable _ISerializableObject)
        {
            String var_Path = _File.Remove(_File.LastIndexOf("/"));

            if (CreatePath(var_Path))
            {
                try
                {
                    Utility.Serializer.SerializeObject(_File, _ISerializableObject);
                    return true;
                }
                catch
                {
                    // Error!
                }
            }
            else
            {
                // Error!
            }
            return false;
        }
    }
}
