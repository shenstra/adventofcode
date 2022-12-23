namespace Advent.Aoc2021
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
            int increases = CountSlidingWindowDecreases(numbers, windowSize: 1);
            Console.WriteLine(increases);
        }

        public void Part2()
        {
            var numbers = input.GetInts().ToList();
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