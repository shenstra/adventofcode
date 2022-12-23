using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day06
    {
        private readonly IInput input;

        public Day06(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            string line = input.GetSingleLine();
            return GetMarkerEnd(line, 4);
        }

        public int Part2()
        {
            string line = input.GetSingleLine();
            return GetMarkerEnd(line, 14);
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