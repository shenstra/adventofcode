namespace Advent.Aoc2015
{
    public class Day08
    {
        private readonly IInput input;
        private readonly Regex escapeSequenceRegex = new(@"(\\\\|\\""|\\x[0-9a-f]{2})");
        private readonly Regex escapableRegex = new(@"(\\|"")");

        public Day08(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var lines = input.GetLines();
            Console.WriteLine(lines.Sum(l => l.Length - DummyUnescape(l).Length));
        }
        public void Part2()
        {
            var lines = input.GetLines();
            Console.WriteLine(lines.Sum(l => DummyEscape(l).Length - l.Length));
        }

        private string DummyUnescape(string line) => escapeSequenceRegex.Replace(line[1..^1], "?");

        private string DummyEscape(string line) => $"\"{escapableRegex.Replace(line, "??")}\"";
    }
}
