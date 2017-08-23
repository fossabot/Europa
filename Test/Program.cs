using System;
using EuropaRTL.Utilities;
using Unosquare.Swan;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] ll = {
                49927398716,
                49927398717,
                1234567812345678,
                1234567812345670
            };
            foreach (var item in ll)
            {
                item.Base16(true).WriteLine();
                item.ToString().WriteLine();
                item.CheckLuhn().ToString().WriteLine();
            }
            long[] cd = { 25245, 48124, 88588, 25, 957828 };
            foreach (var item in cd)
            {
                item.GetLuhnDigit().ToString().WriteLine();
            }
            Console.ReadKey();
        }
    }
}
