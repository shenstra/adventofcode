using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day19
    {
        public void Part1()
        {
            (var rules, var input) = ParseInput(Input.GetLines(2020, 19).ToList());
            var matches = input.Where(line => MatchesRules(new List<int> { 0 }, line, rules));
            Console.WriteLine(matches.Count());
        }

        public void Part2()
        {
            (var rules, var input) = ParseInput(Input.GetLines(2020, 19).ToList());
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";
            var matches = input.Where(line => MatchesRules(new List<int> { 0 }, line, rules));
            Console.WriteLine(matches.Count());
        }

        private bool MatchesRules(List<int> toMatch, string input, Dictionary<int, string> rules)
        {
            if (!toMatch.Any())
                return input == string.Empty;
            else if (input == string.Empty)
                return false;
            string rule = rules[toMatch[0]];
            if (rule.StartsWith('"'))
                return input.StartsWith(rule.Trim('"'))
                    && MatchesRules(toMatch.Skip(1).ToList(), input[rule.Trim('"').Length..], rules);
            foreach (string option in rule.Split(" | "))
            {
                var newToMatch = option.Split().Select(s => int.Parse(s)).Concat(toMatch.Skip(1)).ToList();
                if (MatchesRules(newToMatch, input, rules))
                    return true;
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