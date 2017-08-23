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
                item.ToString().WriteLine();
                Algoritmhs.CheckLuhn(item).ToString().WriteLine();
            }
            Console.ReadKey();
        }
    }
}
