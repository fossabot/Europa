/*
 * For licensing information check LICENSE file
 * 
 */
namespace EuropaRTL
{
    namespace Utilities
    {
        using System;
        using System.Collections.Generic;
        using System.IO;
        using System.IO.Compression;
        using System.Linq;
        using System.Runtime.Serialization;
        using System.Runtime.Serialization.Formatters.Binary;
        using System.Security.Cryptography;

        public enum OS
        {
            Windows = 0,
            Linux,
            MacOS,
            Unix,
            Other
        }
        /// <summary>
        /// This class contains a lot of Extensions some generic some type specific most of them are self explanatory, those wich are not are documented properly
        /// </summary>
        public static partial class Extensions
        {
            /// <summary>
            /// Serializes an object
            /// </summary>
            /// <returns>A byte[] containing the serialized object</returns>
            public static byte[] Bytes<T>(this T i)
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    try
                    {
                        bf.Serialize(ms, i);
                        return ms.ToArray();
                    }
                    catch (SerializationException e)
                    {
                        throw new Exception("Object not serializable", e);
                    }
                }
            }
            /// <summary>
            /// !!!Stream is flushed but kept open!!!
            /// </summary>
            /// <exception cref="ArgumentException"></exception>
            /// <param name="cl">The compression level to use</param>
            /// <param name="data">The data to be used</param>
            /// <returns>A GZip stream thats open and with a clean buffer</returns>
            public static GZipStream Compress<T>(this T i, CompressionLevel cl, byte[] data) where T : Stream
            {
                var gzs = new GZipStream(i, cl);
                gzs.Write(data, 0, data.Length);
                gzs.Flush();
                return gzs;
            }

            public static byte[] Hash<T>(this T io) => SHA256.Create().ComputeHash(io.Bytes());
            public static string Snapshot<T>(this T obj, string ext)
            {
                var hash = obj.Hash();
                var image = new List<byte>(Guid.NewGuid().ToByteArray().Concat(obj.Bytes()).ToArray());
                string file = $"{Environment.CurrentDirectory}\\~~{Guid.NewGuid().ToString("N")}~~{ext}";
                FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
                var imgtmp = image.ToArray();
                var cs = fs.Compress(CompressionLevel.Fastest, imgtmp);
                cs.Dispose();
                fs.Dispose();
                File.SetAttributes(file, FileAttributes.Hidden | FileAttributes.System);
                return file;
            }
            /// <summary>
            /// The os to assume on OS dependent utilities
            /// </summary>
            public static OS os = OS.Windows;
            public static char customSep = '\0';
            /// <summary>
            /// Forma a path to a file from a string default to windows style paths
            /// </summary>
            /// <remarks>Make sure to set os property or it will default to windows</remarks>
            public static string FormPath(this string i, char separator)
            {
                if (os == OS.Windows)
                    i.Replace(separator, '\\');
                else if (os == OS.Linux || os == OS.MacOS || os == OS.Unix)
                    i.Replace(separator, '/');
                else if (os == OS.Other)
                {
                    i.Replace(separator, customSep);
                }
                return i;
            }
        }
    }
}