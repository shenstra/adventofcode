using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day14
    {
        private readonly Dictionary<(string, int), Dictionary<char, long>> cache = new();

        public void Part1()
        {
            (string polymer, var insertionRules) = ParseInput(Input.GetLines(2021, 14).ToList());
            var characterCounts = CharacterCountsAfterSteps(polymer, insertionRules, steps: 10);
            Console.WriteLine(characterCounts.Values.Max() - characterCounts.Values.Min());
        }

        public void Part2()
        {
            (string polymer, var insertionRules) = ParseInput(Input.GetLines(2021, 14).ToList());
            var characterCounts = CharacterCountsAfterSteps(polymer, insertionRules, steps: 40);
            Console.WriteLine(characterCounts.Values.Max() - characterCounts.Values.Min());
        }

        private Dictionary<char, long> CharacterCountsAfterSteps(string polymer, Dictionary<string, char> insertionRules, int steps)
        {
            var counts = polymer.GroupBy(c => c).ToDictionary(g => g.Key, g => (long)g.Count());

            for (int i = 0; i < polymer.Length - 1; i++)
            {
                var extra = CharactersCountsBetween(polymer.Substring(i, 2), insertionRules, steps);
                foreach ((char character, long count) in extra)
                {
                    if (counts.ContainsKey(character))
                    {
                        counts[character] += count;
                    }
                    else
                    {
                        counts[character] = count;
                    }
                }
            }

            return counts;
        }

        private Dictionary<char, long> CharactersCountsBetween(string pair, Dictionary<string, char> insertionRules, int folds)
        {
            if (folds == 0 || !insertionRules.ContainsKey(pair))
            {
                return new();
            }

            if (!cache.ContainsKey((pair, folds)))
            {
                char newCharacter = insertionRules[pair];
                var counts = new Dictionary<char, long> { [newCharacter] = 1 };
                AddCounts(counts, CharactersCountsBetween($"{pair[0]}{newCharacter}", insertionRules, folds - 1));
                AddCounts(counts, CharactersCountsBetween($"{newCharacter}{pair[1]}", insertionRules, folds - 1));
                cache[(pair, folds)] = counts;
            }

            return cache[(pair, folds)];
        }

        private static void AddCounts(Dictionary<char, long> counts, Dictionary<char, long> countsToAdd)
        {
            foreach ((char character, long count) in countsToAdd)
            {
                if (counts.ContainsKey(character))
                {
                    counts[character] += count;
                }
                else
                {
                    counts[character] = count;
                }
            }
        }

        private static (string polymer, Dictionary<string, char> insertionRules) ParseInput(List<string> input)
        {
            string polymer = input.First();
            var insertionRules = new Dictionary<string, char>();
            foreach (string line in input.Skip(2))
            {
                insertionRules[line[0..2]] = line[^1];
            }

            return (polymer, insertionRules);
        }
    }
}