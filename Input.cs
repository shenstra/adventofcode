using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent
{
    public static class Input
    {
        public static IEnumerable<string> GetLines(int year, int day)
        {
            string filePath = $"input/{year}/day{day}.txt";
            return File.ReadLines(filePath);
        }

        public static string GetLine(int year, int day)
        {
            return GetLines(year, day).Single();
        }

        public static IEnumerable<int> GetInts(int year, int day)
        {
            return GetLines(year, day)
                .Select(s => int.Parse(s));
        }

        public static IEnumerable<long> GetLongs(int year, int day)
        {
            return GetLines(year, day)
                .Select(s => long.Parse(s));
        }
    }
}
