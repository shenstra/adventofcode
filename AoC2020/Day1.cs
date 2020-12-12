using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    class Day1
    {
        public void Problem1()
        {
            List<int> numbers = Input.GetInts(2020, 1).ToList();
            Console.WriteLine(GetProductOfSum2020Pair(numbers));
        }

        public void Problem2()
        {
            List<int> numbers = Input.GetInts(2020, 1).ToList();
            Console.WriteLine(GetProductOfSum2020Triple(numbers));
        }

        private static int GetProductOfSum2020Pair(List<int> numbers)
        {
            for (var i = 0; i < numbers.Count - 1; i++)
                for (var j = i + 1; j < numbers.Count; j++)
                    if (numbers[i] + numbers[j] == 2020)
                        return numbers[i] * numbers[j];
            return 0;
        }

        private static int GetProductOfSum2020Triple(List<int> numbers)
        {
            for (var i = 0; i < numbers.Count - 1; i++)
                for (var j = i + 1; j < numbers.Count; j++)
                    for (var k = j + 1; k < numbers.Count; k++)
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                            return numbers[i] * numbers[j] * numbers[k];
            return 0;
        }
    }
}
