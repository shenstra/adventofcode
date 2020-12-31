using System;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day05
    {
        public void Part1()
        {
            var seatIds = Input.GetLines(2020, 5).Select(GetSeatId);
            Console.WriteLine(seatIds.Max());
        }

        public void Part2()
        {
            var seatIds = Input.GetLines(2020, 5).Select(GetSeatId);
            var orderedSeatIds = seatIds.OrderBy(id => id).ToList();
            for (int i = 1; i < orderedSeatIds.Count; i++)
            {
                if (orderedSeatIds[i - 1] == orderedSeatIds[i] - 2)
                {
                    Console.WriteLine(orderedSeatIds[i] - 1);
                }
            }
        }

        private int GetSeatId(string code)
        {
            int row = 0, col = 0;
            if (code[0] == 'B')
            {
                row += 64;
            }

            if (code[1] == 'B')
            {
                row += 32;
            }

            if (code[2] == 'B')
            {
                row += 16;
            }

            if (code[3] == 'B')
            {
                row += 8;
            }

            if (code[4] == 'B')
            {
                row += 4;
            }

            if (code[5] == 'B')
            {
                row += 2;
            }

            if (code[6] == 'B')
            {
                row += 1;
            }

            if (code[7] == 'R')
            {
                col += 4;
            }

            if (code[8] == 'R')
            {
                col += 2;
            }

            if (code[9] == 'R')
            {
                col += 1;
            }

            return (row * 8) + col;
        }
    }
}