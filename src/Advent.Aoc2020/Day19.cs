namespace Advent.Aoc2020
{
    public class Day19(IInput input)
    {
        public void Part1()
        {
            (var rules, var lines) = ParseInput(input.GetLines().ToList());
            var matches = lines.Where(line => MatchesRules([0], line, rules));
            Console.WriteLine(matches.Count());
        }

        public void Part2()
        {
            (var rules, var lines) = ParseInput(input.GetLines().ToList());
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";
            var matches = lines.Where(line => MatchesRules([0], line, rules));
            Console.WriteLine(matches.Count());
        }

        private bool MatchesRules(List<int> toMatch, string input, Dictionary<int, string> rules)
        {
            if (toMatch.Count == 0)
            {
                return input == string.Empty;
            }
            else if (input == string.Empty)
            {
                return false;
            }

            string rule = rules[toMatch[0]];
            if (rule.StartsWith('"'))
            {
                return input.StartsWith(rule.Trim('"'))
                    && MatchesRules(toMatch.Skip(1).ToList(), input[rule.Trim('"').Length..], rules);
            }

            foreach (string option in rule.Split(" | "))
            {
                var newToMatch = option.Split().Select(int.Parse).Concat(toMatch.Skip(1)).ToList();
                if (MatchesRules(newToMatch, input, rules))
                {
                    return true;
                }
            }

            return false;
        }

        private (Dictionary<int, string>, List<string>) ParseInput(List<string> input)
        {
            int spacerIndex = input.IndexOf(string.Empty);
            var rules = input.Take(spacerIndex).Select(s => s.Split(": ")).ToDictionary(a => int.Parse(a[0]), a => a[1]);
            return (rules, input.Skip(spacerIndex + 1).ToList());
        }
    }
}