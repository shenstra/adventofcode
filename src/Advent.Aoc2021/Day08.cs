namespace Advent.Aoc2021
{
    public class Day08(IInput input)
    {
        public void Part1()
        {
            var lines = input.GetLines().Select(ParseLine);
            int matches = lines.Sum(line => line.digits.Count(digit => digit.Length is 2 or 3 or 4 or 7));
            Console.WriteLine(matches);
        }

        public void Part2()
        {
            var lines = input.GetLines().Select(ParseLine);
            int result = lines.Sum(line => Decode(line.patterns, line.digits));
            Console.WriteLine(result);
        }

        private static int Decode(string[] patterns, string[] digits)
        {
            var digitLookup = BuildDigitLookup(patterns);
            int code = 0;
            foreach (string digit in digits)
            {
                code = (code * 10) + digitLookup[digit];
            }

            return code;
        }

        private static Dictionary<string, int> BuildDigitLookup(string[] patterns)
        {
            var patternsToClassify = patterns.AsEnumerable();
            var patternLookup = new Dictionary<int, string>
            {
                [1] = patternsToClassify.Single(p => p.Length == 2),
                [4] = patternsToClassify.Single(p => p.Length == 4),
                [7] = patternsToClassify.Single(p => p.Length == 3),
                [8] = patternsToClassify.Single(p => p.Length == 7)
            };
            patternsToClassify = patternsToClassify.Except(patternLookup.Values);

            patternLookup[2] = patternsToClassify.Single(p => p.Intersect(patternLookup[4]).Count() == 2);
            patternLookup[3] = patternsToClassify.Single(p => p.Except(patternLookup[1]).Count() == 3);
            patternLookup[6] = patternsToClassify.Single(p => p.Except(patternLookup[1]).Count() == 5);
            patternLookup[9] = patternsToClassify.Single(p => p.Intersect(patternLookup[4]).Count() == 4);
            patternsToClassify = patternsToClassify.Except(patternLookup.Values);

            patternLookup[0] = patternsToClassify.Single(p => p.Length == 6);
            patternLookup[5] = patternsToClassify.Single(p => p.Length == 5);
            return patternLookup.ToDictionary(p => p.Value, p => p.Key);
        }

        private static (string[] patterns, string[] digits) ParseLine(string input)
        {
            string[] parts = input.Split(" | ");
            string[] patterns = parts[0].Split().Select(p => p.Sort()).ToArray();
            string[] digits = parts[1].Split().Select(p => p.Sort()).ToArray();
            return (patterns, digits);
        }
    }
}