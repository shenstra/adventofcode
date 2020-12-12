using System;
using System.Linq;

namespace Advent.AoC2015
{
    class Day1
    {
        public static void Problem1()
        {
            var input = Input.GetLine(2015, 1);
            Console.WriteLine(input.Count(c => c == '(') - input.Count(c => c == ')'));
        }

        public static void Problem2()
        {
            var input = Input.GetLine(2015, 1);
            int floor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') floor++;
                else if (input[i] == ')') floor--;
                if (floor < 0)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}
