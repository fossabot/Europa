using System;
using System.Collections.Generic;

namespace EuropaRTL
{
    namespace Utilities
    {
        public static partial class Extensions
        {
            #region Subsets
            public static List<T> Subset<T>(this List<T> l, int sindex, int eindex, int interval)
            {
                if (sindex < l.Count && eindex > l.Count) throw new IndexOutOfRangeException($"Tryed to subset {l.ToString()} out of range, tried {sindex} and {eindex}, lenght was {l.Count}");
                List<T> sub = new List<T>();
                if(eindex < 0)
                    eindex = l.Count;
                for (int i = sindex; i < eindex; i++)
                {
                    sub.Add(l[i]);
                }
                return sub;
            }

            public static T[] Subset<T>(this T[] a, int sindex, int eindex, int interval)
            {
                if (sindex < a.Length && eindex > a.Length) throw new IndexOutOfRangeException($"Tryed to subset {a.ToString()} out of range, tried {sindex} and {eindex}, lenght was {a.Length}");
                List<T> sub = new List<T>();
                if (eindex < 0)
                    eindex = a.Length;
                for (int i = sindex; i < eindex; i += interval)
                {
                    sub.Add(a[i]);
                }
                return sub.ToArray();
            }

            public static T[] Subset<T>(this T[] a, int sindex, int eindex) => Subset(a, sindex, eindex, 1);

            public static List<T> Subset<T>(this List<T> l, int sindex, int eindex) => Subset(l, sindex, eindex, 1);

            public static T[] Subset<T>(this T[] a, int interval) => Subset(a, 0, -1, interval);

            #endregion

            #region StringConv

            public static string String(this int[] i) => System.String.Join("", new List<int>(i).ConvertAll(n => n.ToString()));

            public static string String(this int[] i, string separator) => System.String.Join(separator, new List<int>(i).ConvertAll(n => n.ToString()));

            public static string String(this long[] i) => System.String.Join("", new List<long>(i).ConvertAll(n => n.ToString()));

            public static string String(this long[] i, string separator) => System.String.Join(separator, new List<long>(i).ConvertAll(n => n.ToString()));

            #endregion

            #region Operations

            public static int Arithmetic(this int[] a, char op)
            {
                var work = 0;
                if (op == '+')
                {
                    foreach (var item in a)
                        work += item;
                }
                else if (op == '-')
                {
                    work = a[0];
                    foreach (var item in a.Subset(0, -1))
                        work -= item;
                }
                else if (op == '*')
                {
                    work = a[0];
                    foreach (var item in a.Subset(0, -1))
                        work *= item;
                }
                else if (op == '/')
                {
                    work = a[0];
                    foreach (var item in a.Subset(0, -1))
                        work /= item;
                }
                return work;
            }

            public static int Arithmetic(this int[] a, char op, int val)
            {
                var work = 0;
                if (op == '+')
                {
                    foreach (var item in a)
                        work = item + val;
                }
                else if (op == '-')
                {
                    foreach (var item in a)
                        work = item - val;
                }
                else if (op == '*')
                {
                    foreach (var item in a)
                        work = item * val;
                }
                else if (op == '/')
                {
                    foreach (var item in a)
                        work = item / val;
                }
                return work;
            }

            public static int[] ArithmeticRA(this int[] a, char op, int val)
            {
                var work = new List<int>();
                if (op == '+')
                {
                    foreach (var item in a)
                        work.Add(item + val);
                }
                else if (op == '-')
                {
                    foreach (var item in a)
                        work.Add(item - val);
                }
                else if (op == '*')
                {
                    foreach (var item in a)
                        work.Add(item * val);
                }
                else if (op == '/')
                {
                    foreach (var item in a)
                        work.Add(item / val);
                }
                return work.ToArray();
            }

            public static int[] ShatterAndSum(this int[] a)
            {
                var work = new List<int>();
                foreach (var item in a)
                {
                    work.Add(item.Shatter().Arithmetic('+'));
                }
                return work.ToArray();
            }

            #endregion

        }
    }
}
