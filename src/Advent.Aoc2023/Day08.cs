using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day08(IInput input)
    {
        public long Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var (instructions, map) = ParseNodes(lines);
            return StepsToTarget(instructions, map, "AAA", "ZZZ");
        }

        public long Part2()
        {
            string[] lines = input.GetLines().ToArray();
            var (instructions, map) = ParseNodes(lines);
            return GhostStepsToEnd(instructions, map);
        }

        private long StepsToTarget(string instructions, Dictionary<string, string> map, string from, string targetSuffix)
        {
            int count = 0;
            while (!from.EndsWith(targetSuffix))
            {
                char direction = instructions[count % instructions.Length];
                from = map[$"{from}{direction}"];
                count++;
            }
            return count;
        }

        private long GhostStepsToEnd(string instructions, Dictionary<string, string> map)
        {
            string[] starts = SelectAllEndingInA(map);
            long[] cycleLenghts = starts.Select(from => StepsToTarget(instructions, map, from, "Z")).ToArray();
            return LeastCommonMultiple(cycleLenghts);
        }

        private static string[] SelectAllEndingInA(Dictionary<string, string> map)
        {
            return map.Keys.Where(k => k[3] == 'L').Select(k => k[..3]).Where(k => k.EndsWith('A')).ToArray();
        }

        private static long LeastCommonMultiple(long[] numbers)
        {
            return numbers.Aggregate(1L, (a, b) => a * b / GreatestCommonDivisor(a, b));
        }

        private static long GreatestCommonDivisor(long a, long b)
        {
            return b == 0 ? a : GreatestCommonDivisor(b, a % b);
        }

        private (string instructions, Dictionary<string, string> map) ParseNodes(string[] input)
        {
            var map = new Dictionary<string, string>();
            foreach (string line in input.Skip(2))
            {
                map[line[..3] + "L"] = line[7..10];
                map[line[..3] + "R"] = line[12..15];
            }

            return (input[0], map);
        }
    }
}
