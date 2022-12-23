using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day19
    {
        private readonly IInput input;

        public Day19(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var lines = input.GetLines();
            var (rules, molecule) = ParseInput(lines);
            var mutations = GenerateMutations(molecule, rules);
            Console.WriteLine(mutations.Distinct().Count());
        }

        public void Part2()
        {
            var lines = input.GetLines();
            var (rules, molecule) = ParseInput(lines);
            Console.WriteLine(FindShortestMutationPath("e", molecule, rules));
        }

        private static ((string from, string to)[] rules, string molecule) ParseInput(IEnumerable<string> lines)
        {
            var rules = lines.TakeWhile(s => s != string.Empty).Select(s => s.Split(" => ")).Select(a => (a[0], a[1])).ToArray();
            return (rules, lines.Last());
        }

        private static IEnumerable<string> GenerateMutations(string input, (string from, string to)[] rules)
        {
            for (int i = 0; i < input.Length; i++)
            {
                foreach (var (from, to) in rules)
                {
                    if (input[i..].StartsWith(from))
                    {
                        yield return input[..i] + to + input[(i + from.Length)..];
                    }
                }
            }
        }

        private static int FindShortestMutationPath(string start, string target, (string from, string to)[] rules)
        {
            var distances = new Dictionary<string, int> { { target, 0 } };
            var toTry = new List<string> { target };
            while (true)
            {
                string molecule = toTry.OrderBy(m => distances[m] + (m.Length * 10)).First();
                toTry.Remove(molecule);
                foreach (var (from, to) in rules)
                {
                    for (int i = 0; i < molecule.Length; i++)
                    {
                        if (molecule[i..].StartsWith(to))
                        {
                            string originalMolecule = molecule[..i] + from + molecule[(i + to.Length)..];
                            if (originalMolecule == start)
                            {
                                return distances[molecule] + 1;
                            }
                            else if (!distances.ContainsKey(originalMolecule))
                            {
                                distances[originalMolecule] = distances[molecule] + 1;
                                toTry.Add(originalMolecule);
                            }
                        }
                    }
                }
            }
        }
    }
}
