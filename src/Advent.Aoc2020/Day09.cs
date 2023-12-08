namespace Advent.Aoc2020
{
    public class Day09(IInput input)
    {
        public void Part1()
        {
            var numbers = input.GetLongs().ToList();
            long invalidNumber = FindInvalidNumber(numbers, 25);
            Console.WriteLine(invalidNumber);
        }

        public void Part2()
        {
            var numbers = input.GetLongs().ToList();
            long invalidNumber = FindInvalidNumber(numbers, 25);
            var secretSequence = FindSequenceWithSum(numbers, invalidNumber);
            long secretNumber = secretSequence.Min() + secretSequence.Max();
            Console.WriteLine(secretNumber);
        }

        private static List<long> FindSequenceWithSum(List<long> numbers, long invalidNumber)
        {
            for (int i = 0; i < numbers.Count - 2; i++)
            {
                for (int j = 0; j < numbers.Count - i; j++)
                {
                    if (numbers.Skip(i).Take(j).Sum() == invalidNumber)
                    {
                        return numbers.Skip(i).Take(j).ToList();
                    }
                }
            }

            throw new ApplicationException("No valid solution!");
        }

        private static long FindInvalidNumber(List<long> numbers, int preambleSize)
        {
            for (int i = 0; i < numbers.Count - preambleSize; i++)
            {
                if (!CanSumFrom(numbers.Skip(i).Take(preambleSize).ToList(), numbers[i + preambleSize]))
                {
                    return numbers[i + preambleSize];
                }
            }

            return 0;
        }

        private static bool CanSumFrom(List<long> numbers, long target)
        {
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == target)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
