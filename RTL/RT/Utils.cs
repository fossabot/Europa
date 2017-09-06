using System;
using System.Collections.Generic;

namespace Europa.RTL.RunTime
{
    public static class Evo
    {
        public enum Operations
        {
            Sum,
            Sub,
            Mul,
            Div
        }

        public struct RangeConfig
        {
            public int Start { get; set; }
            public int Length { get; set; }
            public int Interval { get; set; }
            public Operations Op { get; set; }
        }

        public static int[] Range(int start, int length, int interval, Operations op)
        {
            if (interval == 0) throw new ArgumentException("Stepping by 0 would create an infinite loop");
            var work = new List<int>();
            switch (op)
            {
                case Operations.Sum:
                    if (start > length || (start + interval) > length) throw new ArgumentException("Can't have a start bigger than lenght");
                    for (var i = start; i <= length; i += interval)
                    {
                        work.Add(i);
                    }
                    break;

                case Operations.Sub:
                    for (var i = start; i <= length; i -= interval)
                    {
                        work.Add(i);
                    }
                    break;

                case Operations.Mul:
                    if (start > length || (start + interval) > length) throw new ArgumentException("Can't have a start bigger than lenght");
                    if (start == 0) throw new ArgumentException("Can't multiply by 0");
                    for (var i = start; i <= length; i *= interval)
                    {
                        work.Add(i);
                    }
                    break;

                case Operations.Div:
                    if (start == 0) throw new ArgumentException("Can't divide by 0");
                    for (var i = start; i <= length; i /= interval)
                    {
                        work.Add(i);
                    }
                    break;

                default:
                    break;
            }
            return work.ToArray();
        }

        public static int[] Range(RangeConfig config) => Range(config.Start, config.Length, config.Interval, config.Op);

        public static int[] Range(int length) => Range(0, length, 1, Operations.Sum);

        public static int[] Range(int start, int length, int interval) => Range(start, length, interval, Operations.Sum);
    }
}