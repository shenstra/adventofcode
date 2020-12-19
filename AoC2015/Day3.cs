using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2015
{
    internal class Day3
    {
        public void Part1()
        {
            string input = Input.GetSingleLine(2015, 3);
            var history = new List<(int, int)> { (0, 0) };
            int x = 0, y = 0;
            foreach (char c in input)
            {
                switch (c)
                {
                    case '^': y--; break;
                    case 'v': y++; break;
                    case '>': x++; break;
                    case '<': x--; break;
                    default: throw new ApplicationException($"Invalid input: {c}");
                }

                history.Add((x, y));
            }

            Console.WriteLine(history.Distinct().Count());
        }

        public void Part2()
        {
            string input = Input.GetSingleLine(2015, 3);
            var history = new List<(int, int)> { (0, 0) };
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            for (int i = 0; i < input.Length; i += 2)
            {
                switch (input[i])
                {
                    case '^': y1--; break;
                    case 'v': y1++; break;
                    case '>': x1++; break;
                    case '<': x1--; break;
                    default: throw new ApplicationException($"Invalid input: {input[i]}");
                }

                history.Add((x1, y1));

                switch (input[i + 1])
                {
                    case '^': y2--; break;
                    case 'v': y2++; break;
                    case '>': x2++; break;
                    case '<': x2--; break;
                    default: throw new ApplicationException($"Invalid input: {input[i + 1]}");
                }

                history.Add((x2, y2));
            }

            Console.WriteLine(history.Distinct().Count());
        }
    }
}
