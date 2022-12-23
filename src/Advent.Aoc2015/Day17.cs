using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day17
    {
        private readonly IInput input;

        public Day17(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            int[] containers = input.GetInts().OrderByDescending(i => i).ToArray();
            var combinations = FindContainerCombinations(containers, 150, Array.Empty<int>());
            Console.WriteLine(combinations.Count());
        }

        public void Part2()
        {
            int[] containers = input.GetInts().OrderByDescending(i => i).ToArray();
            var combinations = FindContainerCombinations(containers, 150, Array.Empty<int>()).ToList();
            int minContainerCount = combinations.Min(c => c.Length);
            Console.WriteLine(combinations.Count(c => c.Length == minContainerCount));
        }

        private static IEnumerable<int[]> FindContainerCombinations(int[] containers, int target, IEnumerable<int> used)
        {
            if (target == 0)
            {
                yield return used.ToArray();
            }
            else if (target > 0)
            {
                for (int i = 0; i < containers.Length; i++)
                {
                    var solutions = FindContainerCombinations(containers[(i + 1)..], target - containers[i], used.Append(containers[i]));
                    foreach (int[] solution in solutions)
                    {
                        yield return solution;
                    }
                }
            }
        }
    }
}
