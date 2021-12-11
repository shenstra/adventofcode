namespace Advent.Util
{
    public static class StringExtensions
    {
        public static int[] SplitToInts(this string input) => input.Split(',').Select(int.Parse).ToArray();
        public static string Sort(this string input) => string.Concat(input.OrderBy(c => c));
    }
}
