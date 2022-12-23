namespace Advent.Util
{
    public class Input : IInput
    {
        private readonly int year;
        private readonly int day;

        public Input(int year, int day)
        {
            this.year = year;
            this.day = day;
        }

        public IEnumerable<string> GetLines() => File.ReadLines($"input/{year}/day{day:00}.txt");
    }
}
