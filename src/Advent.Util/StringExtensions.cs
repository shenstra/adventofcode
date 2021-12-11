namespace Advent.Util
{
    public static class StringExtensions
    {
        public static int[] SplitToInts(this string input) => input.Split(',').Select(int.Parse).ToArray();
    }
}
