using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent.AoC2020
{
    internal class Day2
    {
        private readonly Regex passwordRegex = new Regex(@"^(\d+)-(\d+) ([a-z]): ([a-z]*)$");

        public void Problem1()
        {
            var entries = Input.GetLines(2020, 2).Select(MapToPasswordEntry);
            Console.WriteLine(entries.Count(e => e.PassesPolicy1()));
        }

        public void Problem2()
        {
            var entries = Input.GetLines(2020, 2).Select(MapToPasswordEntry);
            Console.WriteLine(entries.Count(e => e.PassesPolicy2()));
        }

        private PasswordEntry MapToPasswordEntry(string input)
        {
            var match = passwordRegex.Match(input);
            return new PasswordEntry
            {
                Number1 = int.Parse(match.Groups[1].Value),
                Number2 = int.Parse(match.Groups[2].Value),
                Character = match.Groups[3].Value[0],
                Password = match.Groups[4].Value,
            };
        }

        private record PasswordEntry
        {
            public int Number1 { get; init; }
            public int Number2 { get; init; }
            public char Character { get; init; }
            public string Password { get; init; }

            public bool PassesPolicy1()
            {
                int count = Password.Count(c => c == Character);
                return count >= Number1 && count <= Number2;
            }

            public bool PassesPolicy2()
            {
                bool match1 = Password[Number1 - 1] == Character;
                bool match2 = Password[Number2 - 1] == Character;
                return match1 != match2;
            }
        }

    }
}
