using System;
using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day25
    {
        public void Part1()
        {
            var (row, col) = ParseInput(Input.GetSingleLine(2015, 25));
            int position = SumOfRange(1, col) + SumOfRange(col, row - 1);
            Console.WriteLine(CodeAtPosition(position));
        }

        private (int row, int col) ParseInput(string input)
        {
            var match = Regex.Match(input, @"^To continue, please consult the code grid in the manual.  Enter the code at row (\d+), column (\d+).$");
            return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }

        private int SumOfRange(int start, int count)
        {
            return ((2 * start) + count - 1) * count / 2;
        }

        private long CodeAtPosition(int position)
        {
            long code = 20151125;
            for (int p = 1; p < position; p++)
            {
                code = code * 252533 % 33554393;
            }
            return code;
        }
    }
}
