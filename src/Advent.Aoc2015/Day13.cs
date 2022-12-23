using Advent.Util;
using System.Text.RegularExpressions;

namespace Advent.Aoc2015
{
    public class Day13
    {
        private readonly IInput input;
        private readonly Regex seatingRegex = new(@"^(.+) would (gain|lose) (\d+) happiness units by sitting next to (.+)\.$");

        public Day13(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var happinessChanges = GetHappinessChanges(input.GetLines());
            Console.WriteLine(CalculateMaximumHappiness(happinessChanges, isCircular: true));
        }

        public void Part2()
        {
            var happinessChanges = GetHappinessChanges(input.GetLines());
            Console.WriteLine(CalculateMaximumHappiness(happinessChanges, isCircular: false));
        }

        private static int CalculateMaximumHappiness(Dictionary<(string, string), int> happinessChanges, bool isCircular)
        {
            var names = happinessChanges.Select(a => a.Key.Item1).Distinct();
            var possibleSeatings = EnumeratePossibleSeatings(names, isCircular: isCircular);
            int maxHappiness = possibleSeatings.Max(seating => CalculateHappiness(happinessChanges, seating, isCircular: isCircular));
            return maxHappiness;
        }

        private static IEnumerable<string[]> EnumeratePossibleSeatings(IEnumerable<string> names, bool isCircular)
        {
            var initialSeating = isCircular ? names.Take(1) : names.Take(0);
            foreach (var partialSeating in EnumeratePossibleSeatingsRecursively(names.Except(initialSeating).ToArray()))
            {
                yield return initialSeating.Concat(partialSeating).ToArray();
            }
        }

        private static IEnumerable<IEnumerable<string>> EnumeratePossibleSeatingsRecursively(string[] names)
        {
            if (names.Length == 1)
            {
                yield return names;
            }

            foreach (string name in names)
            {
                string[] thisSeat = new string[] { name };
                foreach (var partialSeating in EnumeratePossibleSeatingsRecursively(names.Except(thisSeat).ToArray()))
                {
                    yield return thisSeat.Concat(partialSeating);
                }
            }
        }

        private static int CalculateHappiness(Dictionary<(string, string), int> happinessChange, string[] seating, bool isCircular)
        {
            int happiness = 0;
            int lastSeat = isCircular ? seating.Length : seating.Length - 1;
            for (int i = 0; i < lastSeat; i++)
            {
                happiness += happinessChange[(seating[i], seating[(i + 1) % seating.Length])];
                happiness += happinessChange[(seating[(i + 1) % seating.Length], seating[i])];
            }

            return happiness;
        }

        private Dictionary<(string, string), int> GetHappinessChanges(IEnumerable<string> lines)
        {
            var happinessChange = new Dictionary<(string, string), int>();
            foreach (string line in lines)
            {
                var match = seatingRegex.Match(line);
                int value = int.Parse(match.Groups[3].Value);
                happinessChange[(match.Groups[1].Value, match.Groups[4].Value)] = match.Groups[2].Value == "gain" ? value : -value;
            }

            return happinessChange;
        }
    }
}