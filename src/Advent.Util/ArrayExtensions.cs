namespace Advent.Util
{
    public static class ArrayExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T[,] array)
        {
            foreach (var item in array)
            {
                yield return item;
            }
        }
    }
}
