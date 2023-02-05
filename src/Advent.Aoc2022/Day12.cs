namespace Advent.Aoc2022
{
    public class Day12
    {
        private readonly IInput input;

        public Day12(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            string[] map = input.GetLines().ToArray();

            return FindShortestRoute(map, fixedStartingLocation: true);
        }

        public int Part2()
        {
            string[] map = input.GetLines().ToArray();

            return FindShortestRoute(map, fixedStartingLocation: false);
        }

        private int FindShortestRoute(string[] map, bool fixedStartingLocation)
        {
            (int width, int height) = (map[0].Length, map.Length);
            int[,] distances = new int[width, height];

            (int startX, int startY) = FindLocation(map, 'S');
            (int targetX, int targetY) = FindLocation(map, 'E');

            map[startY] = map[startY].Replace('S', 'a');
            map[targetY] = map[targetY].Replace('E', 'z');
            if (fixedStartingLocation)
            {
                TryVisitNeighbors(map, distances, startX, startY);
            }
            else
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (map[y][x] == 'a')
                        {
                            TryVisitNeighbors(map, distances, x, y, onlyUp: true);
                        }
                    }
                }
            }

            for (int traveled = 1; traveled < width * height; traveled++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (distances[x, y] == traveled)
                        {
                            TryVisitNeighbors(map, distances, x, y);
                        }
                    }
                }

                if (distances[targetX, targetY] > 0)
                {
                    return distances[targetX, targetY];
                }
            }

            throw new ApplicationException("No route found");
        }

        private (int x, int y) FindLocation(string[] map, char value)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                for (int y = 0; y < map.Length; y++)
                {
                    if (map[y][x] == value)
                    {
                        return (x, y);
                    }
                }
            }

            throw new ApplicationException($"Location {value} not found");
        }

        private void TryVisitNeighbors(string[] map, int[,] distances, int x, int y, bool onlyUp = false)
        {
            char current = map[y][x];
            if (IsValidStep(map, distances, x - 1, y, current, onlyUp))
            {
                distances[x - 1, y] = distances[x, y] + 1;
            }

            if (IsValidStep(map, distances, x + 1, y, current, onlyUp))
            {
                distances[x + 1, y] = distances[x, y] + 1;
            }

            if (IsValidStep(map, distances, x, y - 1, current, onlyUp))
            {
                distances[x, y - 1] = distances[x, y] + 1;
            }

            if (IsValidStep(map, distances, x, y + 1, current, onlyUp))
            {
                distances[x, y + 1] = distances[x, y] + 1;
            }
        }

        private bool IsValidStep(string[] map, int[,] distances, int x, int y, char current, bool onlyUp)
        {
            if (x < 0 || x >= distances.GetLength(0) ||
                y < 0 || y >= distances.GetLength(1) ||
                distances[x, y] != 0)
            {
                return false;
            }

            int delta = map[y][x] - current;
            return onlyUp ? delta == 1 : delta <= 1;
        }
    }
}