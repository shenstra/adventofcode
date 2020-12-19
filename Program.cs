using System;
using System.Diagnostics;

namespace Advent
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Starting...");
            var sw = Stopwatch.StartNew();
            try
            {
                new AoC2020.Day19().Part1();
                new AoC2020.Day19().Part2();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops, exception thrown:\n{ex}");
            }
            sw.Stop();
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms");
        }
    }
}