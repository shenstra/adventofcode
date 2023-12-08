namespace Advent.Aoc2021
{
    public class Day05(IInput input)
    {
        private const int mapSize = 1000;
        private readonly Regex lineRegex = new(@"^(\d+),(\d+) -> (\d+),(\d+)$");

        public void Part1()
        {
            var ventLines = input.GetLines().Select(ParseVentLine);
            int[,] ventLinesMap = PlotVents(ventLines, plotDiagonals: false);
            Console.WriteLine(CountOverlaps(ventLinesMap));
        }

        public void Part2()
        {
            var ventLines = input.GetLines().Select(ParseVentLine);
            int[,] vents = PlotVents(ventLines, plotDiagonals: true);
            Console.WriteLine(CountOverlaps(vents));
        }

        private static int[,] PlotVents(IEnumerable<(int, int, int, int)> lines, bool plotDiagonals)
        {
            int[,] ventLinesMap = new int[mapSize, mapSize];
            foreach (var (x1, y1, x2, y2) in lines)
            {
                if (x1 == x2 || y1 == y2 || plotDiagonals)
                {
                    foreach (var (x, y) in EnumerateLine(x1, y1, x2, y2))
                    {
                        ventLinesMap[x, y]++;
                    }
                }
            }

            return ventLinesMap;
        }

        private static int CountOverlaps(int[,] ventLinesMap)
        {
            int overlaps = 0;
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    if (ventLinesMap[x, y] >= 2)
                    {
                        overlaps++;
                    }
                }
            }

            return overlaps;
        }

        private static IEnumerable<(int x, int y)> EnumerateLine(int x1, int y1, int x2, int y2)
        {
            yield return (x1, y1);

            while (x1 != x2 || y1 != y2)
            {
                if (x1 < x2)
                {
                    x1++;
                }
                else if (x1 > x2)
                {
                    x1--;
                }

                if (y1 < y2)
                {
                    y1++;
                }
                else if (y1 > y2)
                {
                    y1--;
                }

                yield return (x1, y1);
            }
        }

        private (int x1, int y1, int x2, int y2) ParseVentLine(string input)
        {
            var match = lineRegex.Match(input);
            int x1 = int.Parse(match.Groups[1].Value);
            int y1 = int.Parse(match.Groups[2].Value);
            int x2 = int.Parse(match.Groups[3].Value);
            int y2 = int.Parse(match.Groups[4].Value);
            return (x1, y1, x2, y2);
        }
    }
}
