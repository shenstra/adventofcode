using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day11(IInput input)
    {
        public long Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var galaxyMap = new GalaxyMap(lines);
            return galaxyMap.GetGalaxyDistanceSum();
        }

        public long Part2(int emptySpaceMultiplier = 1_000_000)
        {
            string[] lines = input.GetLines().ToArray();
            var galaxyMap = new GalaxyMap(lines);
            return galaxyMap.GetGalaxyDistanceSum(emptySpaceMultiplier);
        }

        private class GalaxyMap
        {
            private readonly List<(int x, int y)> galaxies = [];
            private readonly List<int> emptyX = [];
            private readonly List<int> emptyY = [];

            public GalaxyMap(string[] input)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[y].Length; x++)
                    {
                        if (input[y][x] == '#')
                        {
                            galaxies.Add((x, y));
                        }
                    }
                }

                emptyX.AddRange(Enumerable.Range(0, input[0].Length).Where(x => !galaxies.Any(g => g.x == x)));
                emptyY.AddRange(Enumerable.Range(0, input.Length).Where(y => !galaxies.Any(g => g.y == y)));
            }

            public long GetGalaxyDistanceSum(int emptySpaceMultiplier = 2)
            {
                long sum = 0;

                for (int i = 0; i < galaxies.Count; i++)
                {
                    for (int j = i + 1; j < galaxies.Count; j++)
                    {
                        int lowX = Math.Min(galaxies[i].x, galaxies[j].x);
                        int highX = Math.Max(galaxies[i].x, galaxies[j].x);
                        int lowY = Math.Min(galaxies[i].y, galaxies[j].y);
                        int highY = Math.Max(galaxies[i].y, galaxies[j].y);
                        int emptyXCount = emptyX.Count(x => x > lowX && x < highX);
                        int emptyYCount = emptyY.Count(y => y > lowY && y < highY);

                        long baseDistance = highX - lowX + highY - lowY;
                        long ageCorrection = (emptySpaceMultiplier - 1) * (emptyXCount + emptyYCount);
                        sum += baseDistance + ageCorrection;
                    }
                }

                return sum;
            }
        }
    }
}
