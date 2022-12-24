namespace Advent.Util
{
    public static class ArrayExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2368:Public methods should not have multidimensional array parameters", Justification = "This is an extension method to convert two-dimensional arrays into enumerables")]
        public static IEnumerable<T> AsEnumerable<T>(this T[,] array)
        {
            foreach (var item in array)
            {
                yield return item;
            }
        }
    }
}
