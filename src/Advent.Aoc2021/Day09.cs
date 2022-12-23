using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day09
    {
        private readonly IInput input;

        public Day09(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var lines = input.GetLines().ToList();
            int[,] heightMap = ParseHeightMap(lines);
            var lowPoints = EnumerateLowPoints(heightMap);
            int sumRisk = lowPoints.Sum(pos => heightMap[pos.x, pos.y] + 1);
            Console.WriteLine(sumRisk);
        }

        public void Part2()
        {
            var lines = input.GetLines().ToList();
            int[,] heightMap = ParseHeightMap(lines);
            var lowPoints = EnumerateLowPoints(heightMap);
            var basinSizes = lowPoints.Select(pos => FindBasinSize(heightMap, pos.x, pos.y));
            var largeBasinSizes = basinSizes.OrderByDescending(b => b).Take(3);
            Console.WriteLine(largeBasinSizes.Aggregate((a, b) => a * b));
        }

        private static IEnumerable<(int x, int y)> EnumerateLowPoints(int[,] heightMap)
        {
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    var neighbors = EnumerateNeighbors(heightMap, x, y);
                    if (neighbors.All(pos => heightMap[pos.x, pos.y] > heightMap[x, y]))
                    {
                        yield return (x, y);
                    }
                }
            }
        }

        private int FindBasinSize(int[,] heightMap, int x, int y)
        {
            var basinPositions = new List<(int x, int y)>();
            var positionsToCheck = new Stack<(int x, int y)>();
            positionsToCheck.Push((x, y));
            while (positionsToCheck.Any())
            {
                var pos = positionsToCheck.Pop();
                if (!basinPositions.Contains(pos))
                {
                    basinPositions.Add(pos);
                    foreach (var neighbor in EnumerateNeighbors(heightMap, pos.x, pos.y))
                    {
                        if (heightMap[neighbor.x, neighbor.y] < 9)
                        {
                            positionsToCheck.Push((neighbor.x, neighbor.y));
                        }
                    }
                }
            }
            return basinPositions.Count;
        }

        private static IEnumerable<(int x, int y)> EnumerateNeighbors(int[,] heightMap, int x, int y)
        {
            if (x > 0)
            {
                yield return (x - 1, y);
            }

            if (x < heightMap.GetLength(0) - 1)
            {
                yield return (x + 1, y);
            }

            if (y > 0)
            {
                yield return (x, y - 1);
            }

            if (y < heightMap.GetLength(1) - 1)
            {
                yield return (x, y + 1);
            }
        }

        private static int[,] ParseHeightMap(List<string> lines)
        {
            int width = lines.First().Length;
            int height = lines.Count;
            int[,] heightMap = new int[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    heightMap[x, y] = int.Parse(lines[y][x].ToString());
                }
            }

            return heightMap;
        }
    }
}