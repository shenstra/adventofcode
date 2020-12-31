using System;
using System.Linq;
using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day01
    {
        public void Part1()
        {
            string input = Input.GetSingleLine(2015, 1);
            Console.WriteLine(input.Count(c => c == '(') - input.Count(c => c == ')'));
        }

        public void Part2()
        {
            string input = Input.GetSingleLine(2015, 1);
            int floor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    floor++;
                }
                else if (input[i] == ')')
                {
                    floor--;
                }

                if (floor < 0)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}
