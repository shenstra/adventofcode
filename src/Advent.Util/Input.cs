namespace Advent.Util
{
    public class Input(int year, int day) : IInput
    {
        public IEnumerable<string> GetLines() => File.ReadLines($"input/{year}/day{day:00}.txt");
    }
}
