using Advent.Util;
using System.Text.RegularExpressions;

namespace Advent.Aoc2020
{
    public class Day07
    {
        private readonly IInput input;

        public Day07(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var rules = BuildBagRules(input.GetLines());
            var bagOptions = FindBagsThatCanHoldCertainBags(rules, new List<string> { "shiny gold" }).ToList();

            while (true)
            {
                var extraBagOptions = FindBagsThatCanHoldCertainBags(rules, bagOptions).Except(bagOptions);
                if (!extraBagOptions.Any())
                {
                    break;
                }

                bagOptions.AddRange(extraBagOptions);
            }

            Console.WriteLine(bagOptions.Count);
        }

        public void Part2()
        {
            var rules = BuildBagRules(input.GetLines());
            var bagsToCheck = new Queue<string>();
            var requiredBags = new List<string>();
            bagsToCheck.Enqueue("shiny gold");

            while (bagsToCheck.Any())
            {
                string bag = bagsToCheck.Dequeue();
                requiredBags.Add(bag);
                foreach (var neededBag in rules[bag])
                {
                    for (int i = 0; i < neededBag.Value; i++)
                    {
                        bagsToCheck.Enqueue(neededBag.Key);
                    }
                }
            }

            Console.WriteLine(requiredBags.Count - 1);
        }

        private static List<string> FindBagsThatCanHoldCertainBags(Dictionary<string, Dictionary<string, int>> rules, List<string> certainBags)
        {
            return rules.Where(rule => rule.Value.Keys.Intersect(certainBags).Any())
                .Select(rule => rule.Key)
                .ToList();
        }

        private static Dictionary<string, Dictionary<string, int>> BuildBagRules(IEnumerable<string> input)
        {
            var rules = new Dictionary<string, Dictionary<string, int>>();
            var containerRegex = new Regex(@"^(?<container>[a-z ]*) bags contain (?<contents>.*).$");
            var contentsRegex = new Regex(@"^(?<count>\d+) (?<color>.+) bags?$");

            foreach (string line in input)
            {
                var containerResult = containerRegex.Match(line);
                string container = containerResult.Groups["container"].Value;
                string contents = containerResult.Groups["contents"].Value;

                rules.Add(container, new Dictionary<string, int>());
                if (contents != "no other bags")
                {
                    foreach (string content in contents.Split(", "))
                    {
                        var contentsMatch = contentsRegex.Match(content);
                        int count = int.Parse(contentsMatch.Groups["count"].Value);
                        string color = contentsMatch.Groups["color"].Value;
                        rules[container].Add(color, count);
                    }
                }
            }

            return rules;
        }
    }
}
