using System;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuropaRTL.Utilities;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            long t = 61789372994;
            Console.WriteLine(t.Shatter(true).String());
            Console.WriteLine(t.Shatter(false).String());
            Console.WriteLine(t.Shatter(false).String().Long().ToString());
            t.Shatter(true).Snapshot(".bin");
            Console.ReadKey();
        }
    }
}
