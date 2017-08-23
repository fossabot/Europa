using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuropaRTL
{
    namespace Utilities
    {
        public static partial class Extensions
        {
            /// <summary>
            /// Shatter a number to its digits
            /// </summary>
            /// <param name="reverse">Should the number be reversed before returning</param>
            /// <returns>An array containing the number digits</returns>
            public static int[] Shatter(this int n, bool reverse)
            {
                Stack<int> work = new Stack<int>();
                if (!(n > 9))
                    work.Push(n);
                else
                {
                    for (; n > 0; n /= 10)
                        work.Push(n % 10);
                }
                if (reverse)
                    work.Reverse();
                return work.ToArray();
            }

            public static short[] Shatter(this short n, bool reverse) => Shatter(n, reverse);

            public static int[] Shatter(this long n, bool reverse)
            {
                Stack<int> work = new Stack<int>();
                if (!(n > 9))
                    work.Push((int)n);
                else
                {
                    for (; n > 0; n /= 10)
                        work.Push((int)(n % 10));
                }
                if (reverse)
                    work.Reverse();
                return work.ToArray();
            }

            public static int[] Shatter(this int n) => Shatter(n, false);
            public static int[] Shatter(this long n) => Shatter(n, false);

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

            public static long SumDigits(this long n)
            {
                var work = n.Shatter();
                var pile = 0;
                foreach (var item in work)
                {
                    pile += item;
                }
                return pile;
            }

            public static int Factorial(this int n) => n == 0 ? 1 : n * Factorial(n - 1);

            public static long Factorial(this long n) => n == 0 ? 1 : n * Factorial(n - 1);

            public static int Int(this string i) => int.TryParse(i, out int rt) ? rt : throw new StackOverflowException("Number too big for a int try Long()");

            public static long Long(this string i) => long.TryParse(i, out long rt) ? rt : throw new StackOverflowException("Number is too big to be computed");
        }
    }
}
