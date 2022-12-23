namespace Advent.Util
{
    public interface IInput
    {
        IEnumerable<string> GetLines();

        public string GetSingleLine() => GetLines().Single();

        public IEnumerable<int> GetInts() => GetLines().Select(s => int.Parse(s));

        public int GetSingleInt() => GetInts().Single();

        public IEnumerable<long> GetLongs() => GetLines().Select(s => long.Parse(s));
    }
}