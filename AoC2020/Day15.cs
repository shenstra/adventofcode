using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day15
    {
        public void Problem1()
        {
            var numbers = GetNumbers(Input.GetLine(2020, 15));
            Console.WriteLine(GetNthNumber(numbers, 2020));
        }

        public void Problem2()
        {
            var numbers = GetNumbers(Input.GetLine(2020, 15));
            Console.WriteLine(GetNthNumber(numbers, 30000000));
        }

        private static int GetNthNumber(List<int> numbers, int limit)
        {
            int[] lastIndex = Enumerable.Repeat(-1, limit).ToArray();
            for (int round = 0; round < numbers.Count - 1; round++)
                lastIndex[numbers[round]] = round;
            int prevNumber = numbers.Last();
            int newNumber;
            for (int round = numbers.Count; round < limit; round++)
            {
                newNumber = lastIndex[prevNumber] == -1 ? 0 : round - 1 - lastIndex[prevNumber];
                lastIndex[prevNumber] = round - 1;
                prevNumber = newNumber;
            }
            return prevNumber;
        }

        private static List<int> GetNumbers(string line)
        {
            return line.Split(',').Select(s => int.Parse(s)).ToList();
        }
    }
}
