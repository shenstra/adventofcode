namespace Advent.Aoc2022
{
    public class Day04
    {
        private readonly IInput input;

        public Day04(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            var ranges = input.GetLines().Select(MapRanges).ToList();
            return ranges.Count(FullyOverlaps);
        }

        public int Part2()
        {
            var ranges = input.GetLines().Select(MapRanges).ToList();
            return ranges.Count(PartiallyOverlaps);
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