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

            public static int SumDigits(this int n)
            {
                var work = n.Shatter();
                var pile = 0;
                foreach (var item in work)
                {
                    pile += item;
                }
                return pile;
            }

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
            /// Shatter a number to its digits
            /// </summary>
            /// <param name="reverse">Should the number be reversed before returning</param>
            /// <returns>An array containing the number digits</returns>
            public static int[] Shatter(this int n, bool reverse)
            {
                List<int> work = new List<int>();
                if (!(n > 9))
                    work.Add(n);
                else
                {
                    while (n > 0)
                    {
                        work.Add(n % 10);
                        n /= 10;
                    }
                }
                if (!reverse)
                    work.Reverse();
                return work.ToArray();
            }

            public static short[] Shatter(this short n, bool reverse) => Shatter(n, reverse);

            public static long[] Shatter(this long n, bool reverse)
            {
                List<long> work = new List<long>();
                if (!(n > 9))
                    work.Add(n);
                else
                {
                    while (n > 0)
                    {
                        work.Add(n % 10);
                        n /= 10;
                    }
                }
                if (!reverse)
                    work.Reverse();
                return work.ToArray();
            }

            public static int[] Shatter(this int n) => Shatter(n, false);

            public static string String(this long[] i) => System.String.Join("", new List<long>(i).ConvertAll(n => n.ToString()));

            public static string String(this short[] i) => System.String.Join("", new List<short>(i).ConvertAll(n => n.ToString()));

            public static int Int(this string i) => int.TryParse(i, out int rt) ? rt : throw new StackOverflowException("Number too big for a int try Long()");

            public static long Long(this string i) => long.TryParse(i, out long rt) ? rt : throw new StackOverflowException("Number is too big to be computed");

            /// <summary>
            /// Subset a list works like python eg. passing 2 and 5 will get elements 2 to 4
            /// </summary>
            /// <param name="sindex">Start Index</param>
            /// <param name="eindex">First element out of the list</param>
            /// <returns>a list containnig your subset</returns>
            /// <exception cref="IndexOutOfRangeException">When the indexes are out of the list range</exception>
            public static T[] Subset<T>(this T[] a, int sindex, uint eindex)
            {
                if (a.Length >= sindex || a.Length <= (eindex - 1)) throw new IndexOutOfRangeException($"Tryed to sublist list out of range, tried {sindex} and {eindex}, lenght was {a.Length}");
                List<T> sub = new List<T>();
                for (var i = sindex; i <= eindex; i++)
                {
                    sub.Add(a[i]);
                }
                return sub.ToArray();
            }

            /// <summary>
            /// Subset an array works like python eg. passing 2 and 5 will get elements 2 to 4
            /// </summary>
            /// <param name="sindex">Start Index</param>
            /// <param name="eindex">First element out of the list</param>
            /// <returns>a list containnig your subset</returns>
            /// <exception cref="IndexOutOfRangeException">When the indexes are out of the array range</exception>
            public static List<T> Subset<T>(this List<T> l, int sindex, uint eindex)
            {
                if (l.Count >= sindex || l.Count <= (eindex - 1)) throw new IndexOutOfRangeException($"Tryed to subset {l.ToString()} out of range, tried {sindex} and {eindex}, lenght was {l.Count}");
                List<T> sub = new List<T>();
                for (var i = sindex; i <= eindex; i++)
                {
                    sub.Add(l[i]);
                }
                return sub;
            }
        }
    }
}