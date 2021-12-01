namespace Advent.Util
{
    public static class Input
    {
        public static IEnumerable<string> GetLines(int year, int day) => File.ReadLines($"input/{year}/day{day:00}.txt");

        public static string GetSingleLine(int year, int day) => GetLines(year, day).Single();

        public static IEnumerable<int> GetInts(int year, int day) => GetLines(year, day).Select(s => int.Parse(s));

        public static int GetSingleInt(int year, int day) => GetInts(year, day).Single();

        public static IEnumerable<long> GetLongs(int year, int day) => GetLines(year, day).Select(s => long.Parse(s));
    }
}
