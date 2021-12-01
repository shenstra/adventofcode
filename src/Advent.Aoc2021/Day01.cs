using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day01
    {
        public void Part1()
        {
            var numbers = Input.GetInts(2021, 1).ToList();
            int increases = CountSlidingWindowDecreases(numbers, windowSize: 1);
            Console.WriteLine(increases);
        }

        public void Part2()
        {
            var numbers = Input.GetInts(2021, 1).ToList();
            Console.WriteLine(CountSlidingWindowDecreases(numbers, windowSize: 3));
        }

        private static int CountSlidingWindowDecreases(List<int> numbers, int windowSize)
        {
            int increases = 0;
            for (int i = windowSize; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - windowSize])
                {
                    increases++;
                }
            }

            return increases;
        }
    }
}