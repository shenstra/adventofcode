using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public static class Input
    {
        public static IEnumerable<string> GetLines(int year, int day) => File.ReadLines($"input/{year}/day{day}.txt");

        public static string GetSingleLine(int year, int day) => GetLines(year, day).Single();

        public static IEnumerable<int> GetInts(int year, int day) => GetLines(year, day).Select(s => int.Parse(s));

        public static IEnumerable<long> GetLongs(int year, int day) => GetLines(year, day).Select(s => long.Parse(s));
    }
}
