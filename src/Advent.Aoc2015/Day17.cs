using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day17
    {

        public void Part1()
        {
            int[] containers = Input.GetInts(2015, 17).OrderByDescending(i => i).ToArray();
            var combinations = FindContainerCombinations(containers, 150, Array.Empty<int>());
            Console.WriteLine(combinations.Count());
        }

        public void Part2()
        {
            int[] containers = Input.GetInts(2015, 17).OrderByDescending(i => i).ToArray();
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
