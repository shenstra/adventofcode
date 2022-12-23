using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day01
    {
        private readonly IInput input;

        public Day01(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var numbers = input.GetInts().ToList();
            Console.WriteLine(GetProductOfSum2020Pair(numbers));
        }

        public void Part2()
        {
            var numbers = input.GetInts().ToList();
            Console.WriteLine(GetProductOfSum2020Triple(numbers));
        }

        private static int GetProductOfSum2020Pair(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == 2020)
                    {
                        return numbers[i] * numbers[j];
                    }
                }
            }

            return 0;
        }

        private static int GetProductOfSum2020Triple(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    for (int k = j + 1; k < numbers.Count; k++)
                    {
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                        {
                            return numbers[i] * numbers[j] * numbers[k];
                        }
                    }
                }
            }

            return 0;
        }
    }
}
