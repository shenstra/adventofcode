using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day04
    {
        public void Part1()
        {
            var ranges = Input.GetLines(2022, 4).Select(MapRanges).ToList();
            Console.WriteLine(ranges.Count(FullyOverlaps));
        }

        public void Part2()
        {
            var ranges = Input.GetLines(2022, 4).Select(MapRanges).ToList();
            Console.WriteLine(ranges.Count(PartiallyOverlaps));
        }

        private (int start1, int end1, int start2, int end2) MapRanges(string input)
        {
            var numbers = input.Split(new[] { '-', ',' }).Select(int.Parse).ToList();
            return (numbers[0], numbers[1], numbers[2], numbers[3]);
        }

        private bool FullyOverlaps((int start1, int end1, int start2, int end2) ranges)
        {
            return (ranges.start1 >= ranges.start2 && ranges.end1 <= ranges.end2)
                || (ranges.start1 <= ranges.start2 && ranges.end1 >= ranges.end2);
        }

        private bool PartiallyOverlaps((int start1, int end1, int start2, int end2) ranges)
        {
            return ranges.start1 <= ranges.start2
                ? ranges.end1 >= ranges.start2
                : ranges.end2 >= ranges.start1;
        }
    }
}