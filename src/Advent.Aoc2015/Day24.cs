using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day24
    {
        public void Part1()
        {
            long[] packages = Input.GetLongs(2015, 24).ToArray();
            var groups = EnumerateThirdGroups(packages, packages.Sum() / 3).ToList();
            int minSize = groups.Min(g => g.Length);
            long solution = groups.Where(g => g.Length == minSize).Min(g => QuantumEntanglement(g));
            Console.WriteLine(solution);
        }

        public void Part2()
        {
            long[] packages = Input.GetLongs(2015, 24).ToArray();
            var groups = EnumerateQuarterGroups(packages, packages.Sum() / 4).ToList();
            int minSize = groups.Min(g => g.Length);
            long solution = groups.Where(g => g.Length == minSize).Min(g => QuantumEntanglement(g));
            Console.WriteLine(solution);
        }

        private IEnumerable<long[]> EnumerateThirdGroups(long[] packages, long target)
        {
            return EnumerateGroups(packages, target)
                .Where(group => EnumerateGroups(packages.Except(group).ToArray(), target).Any());
        }

        private IEnumerable<long[]> EnumerateQuarterGroups(long[] packages, long target)
        {
            return EnumerateGroups(packages, target)
                .Where(group => EnumerateThirdGroups(packages.Except(group).ToArray(), target).Any());
        }

        private IEnumerable<long[]> EnumerateGroups(long[] packages, long target)
        {
            for (int i = 0; i < packages.Length; i++)
            {
                if (packages[i] == target)
                {
                    yield return new long[] { packages[i] };
                }
                else if (packages[i] < target)
                {
                    foreach (var group in EnumerateGroups(packages[(i + 1)..], target - packages[i]))
                    {
                        yield return group.Append(packages[i]).ToArray();
                    }
                }
            }
        }

        private long QuantumEntanglement(long[] packages)
        {
            return packages.Aggregate((a, b) => a * b);
        }
    }
}
