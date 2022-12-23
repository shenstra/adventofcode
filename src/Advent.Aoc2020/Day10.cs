using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day10
    {
        private readonly IInput input;
        private static readonly Dictionary<string, long> memoizedCombinations = new();

        public Day10(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var sortedAdapters = GetSortedAdapters(input.GetInts());
            int jump1 = 0, jump3 = 0;
            for (int i = 0; i < sortedAdapters.Count - 1; i++)
            {
                if (sortedAdapters[i] == sortedAdapters[i + 1] - 1)
                {
                    jump1++;
                }

                if (sortedAdapters[i] == sortedAdapters[i + 1] - 3)
                {
                    jump3++;
                }
            }

            Console.WriteLine(jump1 * jump3);
        }

        public void Part2()
        {
            var sortedAdapters = GetSortedAdapters(input.GetInts());
            long combinations = FindCombinations(sortedAdapters);
            Console.WriteLine(combinations);
        }

        private static List<int> GetSortedAdapters(IEnumerable<int> input)
        {
            var adapters = input.ToList();
            adapters.Add(0); // outlet
            adapters.Add(adapters.Max() + 3); // device
            return adapters.OrderBy(a => a).ToList();
        }

        private long FindCombinations(List<int> sortedAdapters)
        {
            string key = string.Join("-", sortedAdapters);
            if (!memoizedCombinations.ContainsKey(key))
            {
                if (sortedAdapters.Count == 1)
                {
                    memoizedCombinations[key] = 1;
                }
                else
                {
                    long combinations = 0;
                    for (int i = 1; i < sortedAdapters.Count; i++)
                    {
                        if (sortedAdapters[i] - sortedAdapters[0] <= 3)
                        {
                            combinations += FindCombinations(sortedAdapters.Skip(i).ToList());
                        }
                    }

                    memoizedCombinations[key] = combinations;
                }
            }

            return memoizedCombinations[key];
        }
    }
}
