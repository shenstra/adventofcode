﻿namespace Advent.Aoc2015
{
    public class Day02(IInput input)
    {
        public void Part1()
        {
            var dimensions = input.GetLines().Select(ConvertToDimensions);
            int neededPaper = dimensions.Select(NeededPaper).Sum();
            Console.WriteLine(neededPaper);
        }
        public void Part2()
        {
            var dimensions = input.GetLines().Select(ConvertToDimensions);
            int neededRibbon = dimensions.Select(NeededRibbon).Sum();
            Console.WriteLine(neededRibbon);
        }

        private List<int> ConvertToDimensions(string l)
        {
            return l.Split('x').Select(int.Parse).ToList();
        }

        private static int NeededRibbon(List<int> dimensions)
        {
            var orderedDimensions = dimensions.OrderBy(d => d).ToList();
            return (2 * orderedDimensions[0])
                + (2 * orderedDimensions[1])
                + (orderedDimensions[0] * orderedDimensions[1] * orderedDimensions[2]);
        }

        private static int NeededPaper(List<int> dimensions)
        {
            var sides = new List<int> {
                dimensions[0] * dimensions[1],
                dimensions[0] * dimensions[2],
                dimensions[1] * dimensions[2]
            };
            return (2 * sides[0])
                + (2 * sides[1])
                + (2 * sides[2])
                + sides.Min();
        }
    }
}
