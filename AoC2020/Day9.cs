using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    class Day9
    {
        public static void Problem1()
        {
            var numbers = Input.GetLongs(2020, 9).ToList();
            var invalidNumber = FindInvalidNumber(numbers, 25);
            Console.WriteLine(invalidNumber);
        }
        public static void Problem2()
        {
            var numbers = Input.GetLongs(2020, 9).ToList();
            var invalidNumber = FindInvalidNumber(numbers, 25);
            var secretSequence = FindSequenceWithSum(numbers, invalidNumber);
            var secretNumber = secretSequence.Min() + secretSequence.Max();
            Console.WriteLine(secretNumber);
        }

        private static List<long> FindSequenceWithSum(List<long> numbers, long invalidNumber)
        {
            for (var i = 0; i < numbers.Count - 2; i++)
                for (var j = 0; j < numbers.Count - i; j++)
                    if (numbers.Skip(i).Take(j).Sum() == invalidNumber)
                        return numbers.Skip(i).Take(j).ToList();
            throw new ApplicationException("No valid solution!");
        }

        private static long FindInvalidNumber(List<long> numbers, int preambleSize)
        {
            for (var i = 0; i < numbers.Count - preambleSize; i++)
                if (!CanSumFrom(numbers.Skip(i).Take(preambleSize).ToList(), numbers[i + preambleSize]))
                    return numbers[i + preambleSize];
            return 0;
        }

        private static bool CanSumFrom(List<long> numbers, long target)
        {
            for (var i = 0; i < numbers.Count - 1; i++)
                for (var j = i + 1; j < numbers.Count; j++)
                    if (numbers[i] + numbers[j] == target)
                        return true;
            return false;
        }
    }
}
