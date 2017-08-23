namespace EuropaRTL
{
    namespace Utilities
    {
        public static partial class Algoritmhs
        {
            public static bool CheckLuhn(long n)
            {
                int s1 = n.Shatter(true).Subset(2).Arithmetic('+');
                int s2 = n.Shatter(true).Subset(1, -1, 2).ArithmeticRA('*', 2).ShatterAndSum().Arithmetic('+');
                return (s1 + s2) % 10 == 0 ? true : false;
            }
        }
    }
}
