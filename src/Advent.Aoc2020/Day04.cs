using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day04
    {
        public void Part1()
        {
            var entries = GetEntries(Input.GetLines(2020, 4));
            var validEntries = entries.Where(e => e.HasRequiredFields());
            Console.WriteLine(validEntries.Count());
        }
        public void Part2()
        {
            var entries = GetEntries(Input.GetLines(2020, 4));
            var validEntries = entries.Where(e => e.HasRequiredFields() && e.HasValidFields());
            Console.WriteLine(validEntries.Count());
        }

        private static IEnumerable<Entry> GetEntries(IEnumerable<string> lines)
        {
            var current = new Entry();
            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    yield return current;
                    current = new Entry();
                }
                else
                {
                    current.AddFields(line);
                }
            }

            yield return current;
        }

        private class Entry
        {
            private readonly Dictionary<string, string> fields = new Dictionary<string, string>();
            private readonly Regex hgtRegex = new Regex(@"^(\d+)(cm|in)$");
            private readonly Regex hclRegex = new Regex(@"^#([0-9a-f]{6})$");
            private readonly Regex eclRegex = new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$");
            private readonly Regex pidRegex = new Regex(@"^([0-9]{9})$");

            public void AddFields(string line)
            {
                foreach (string fieldData in line.Split(' '))
                {
                    string[] fieldParts = fieldData.Split(':');
                    fields[fieldParts[0]] = fieldParts[1];
                }
            }

            public bool HasRequiredFields()
            {
                return fields.ContainsKey("byr")
                    && fields.ContainsKey("iyr")
                    && fields.ContainsKey("eyr")
                    && fields.ContainsKey("hgt")
                    && fields.ContainsKey("hcl")
                    && fields.ContainsKey("ecl")
                    && fields.ContainsKey("pid");
            }

            public bool HasValidFields()
            {
                return HasValidByr()
                    && HasValidIyr()
                    && HasValidEyr()
                    && HasValidHgt()
                    && HasValidHcl()
                    && HasValidEcl()
                    && HasValidPid();
            }

            private bool HasValidByr() => int.TryParse(fields["byr"], out int byr) && byr >= 1920 && byr <= 2002;

            private bool HasValidIyr() => int.TryParse(fields["iyr"], out int iyr) && iyr >= 2010 && iyr <= 2020;

            private bool HasValidEyr() => int.TryParse(fields["eyr"], out int eyr) && eyr >= 2020 && eyr <= 2030;

            private bool HasValidHgt()
            {
                var match = hgtRegex.Match(fields["hgt"]);
                if (match.Success)
                {
                    if (match.Groups[2].Value == "cm")
                    {
                        return int.TryParse(match.Groups[1].Value, out int cm) && cm >= 150 && cm <= 193;
                    }

                    if (match.Groups[2].Value == "in")
                    {
                        return int.TryParse(match.Groups[1].Value, out int inches) && inches >= 59 && inches <= 76;
                    }
                }

                return false;
            }

            private bool HasValidHcl() => hclRegex.Match(fields["hcl"]).Success;

            private bool HasValidEcl() => eclRegex.Match(fields["ecl"]).Success;

            private bool HasValidPid() => pidRegex.Match(fields["pid"]).Success;
        }
    }
}
