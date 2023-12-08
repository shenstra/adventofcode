namespace Advent.Util
{
    public interface IInput
    {
        IEnumerable<string> GetLines();

        public string GetSingleLine() => GetLines().Single();

        public IEnumerable<int> GetInts() => GetLines().Select(int.Parse);

        public int GetSingleInt() => GetInts().Single();

        public IEnumerable<long> GetLongs() => GetLines().Select(long.Parse);
    }
}