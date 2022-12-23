using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day06
    {
        public void Part1()
        {
            string input = Input.GetSingleLine(2022, 6);
            Console.WriteLine(GetMarkerEnd(input, 4));
        }

        public void Part2()
        {
            string input = Input.GetSingleLine(2022, 6);
            Console.WriteLine(GetMarkerEnd(input, 14));
        }

        private int GetMarkerEnd(string input, int markerLength)
        {
            for (int i = markerLength; i <= input.Length; i++)
            {
                if (input[(i - markerLength)..i].Distinct().Count() == markerLength)
                {
                    return i;
                }
            }

            throw new ApplicationException("No marker found in message");
        }
    }
}