using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent.AoC2015
{
    internal class Day8
    {
        private readonly Regex escapeSequenceRegex = new Regex(@"(\\\\|\\""|\\x[0-9a-f]{2})");
        private readonly Regex escapableRegex = new Regex(@"(\\|"")");

        public void Part1()
        {
            var lines = Input.GetLines(2015, 8);
            Console.WriteLine(lines.Sum(l => l.Length - DummyUnescape(l).Length));
        }
        public void Part2()
        {
            var lines = Input.GetLines(2015, 8);
            Console.WriteLine(lines.Sum(l => DummyEscape(l).Length - l.Length));
        }

        private string DummyUnescape(string line) => escapeSequenceRegex.Replace(line[1..^1], "?");

        private string DummyEscape(string line) => $"\"{escapableRegex.Replace(line, "??")}\"";
    }
}
