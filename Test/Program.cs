using System;
using Unosquare.Swan;
using static Europa.RTL.RunTime.Evo;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                foreach (var item in Range(10, 1, 10, Operations.Mul))
                {
                    item.ToString().WriteLine();
                }
            }
            catch (Exception e)
            {
                e.Message.WriteLine();
            }
            Console.ReadKey();
        }
    }
}