using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day10(IInput input)
    {
        public int Part1()
        {
            char[][] map = input.GetLines().Select(s => s.ToCharArray()).ToArray();
            int[][] distances = FindDistances(map);
            return distances.Select(line => line.Max()).Max();
        }

        public int Part2()
        {

            char[][] map = input.GetLines().Select(s => s.ToCharArray()).ToArray();
            int[][] distances = FindDistances(map);
            return CountInside(map, distances);
        }

        private static readonly char[] rightConnectedChars = ['-', 'L', 'F'];
        private static readonly char[] leftConnectedChars = ['-', 'J', '7'];
        private static readonly char[] upConnectedChars = ['|', 'J', 'L'];
        private static readonly char[] downConnectedChars = ['|', '7', 'F'];
        private static readonly char[] bendChars = ['L', 'F', 'J', '7'];

        private int[][] FindDistances(char[][] map)
        {
            int width = map[0].Length;
            int height = map.Length;
            int[][] distances = InitializeDistanceMap(width, height);

            var toCheck = VisitStart(map, distances);

            for (int distance = 1; toCheck.Count != 0; distance++)
            {
                var nextToCheck = new List<(int x, int y)>();

                foreach ((int x, int y) in toCheck)
                {
                    distances[y][x] = distance;
                    nextToCheck.AddRange(FindConnected(map, distances, x, y));
                }

                toCheck = nextToCheck;
            }

            return distances;
        }

        private IEnumerable<(int x, int y)> FindConnected(char[][] map, int[][] distances, int x, int y)
        {
            if (leftConnectedChars.Contains(map[y][x]) && distances[y][x - 1] == -1)
            {
                yield return (x - 1, y);
            }
            if (rightConnectedChars.Contains(map[y][x]) && distances[y][x + 1] == -1)
            {
                yield return (x + 1, y);
            }
            if (upConnectedChars.Contains(map[y][x]) && distances[y - 1][x] == -1)
            {
                yield return (x, y - 1);
            }
            if (downConnectedChars.Contains(map[y][x]) && distances[y + 1][x] == -1)
            {
                yield return (x, y + 1);
            }
        }

        private (int x, int y) FindStart(char[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    if (map[y][x] == 'S')
                    {
                        return (x, y);
                    }
                }
            }

            throw new ArgumentException("Map does not contain start point", nameof(map));
        }

        private List<(int x, int y)> VisitStart(char[][] map, int[][] distances)
        {
            (int startX, int startY) = FindStart(map);
            distances[startY][startX] = 0;
            int width = map[0].Length;
            int height = map.Length;

            var result = new List<(int, int)>();

            bool leftConnected = false;
            bool rightConnected = false;
            bool upConnected = false;
            bool downConnected = false;

            if (startX > 0 && rightConnectedChars.Contains(map[startY][startX - 1]))
            {
                result.Add((startX - 1, startY));
                leftConnected = true;
            }
            if (startX < width - 1 && leftConnectedChars.Contains(map[startY][startX + 1]))
            {
                result.Add((startX + 1, startY));
                rightConnected = true;
            }
            if (startY > 0 && downConnectedChars.Contains(map[startY - 1][startX]))
            {
                result.Add((startX, startY - 1));
                upConnected = true;
            }
            if (startY < height - 1 && upConnectedChars.Contains(map[startY + 1][startX]))
            {
                result.Add((startX, startY + 1));
                downConnected = true;
            }

            map[startY][startX] = GeneratePipe(leftConnected, rightConnected, upConnected, downConnected);

            return result;
        }

        private char GeneratePipe(bool leftConnected, bool rightConnected, bool upConnected, bool downConnected)
        {
            if (upConnected && downConnected)
            {
                return '|';
            }
            else if (leftConnected && rightConnected)
            {
                return '-';
            }
            else if (upConnected && rightConnected)
            {
                return 'L';
            }
            else if (upConnected && leftConnected)
            {
                return 'J';
            }
            else if (downConnected && leftConnected)
            {
                return '7';
            }
            else if (downConnected && rightConnected)
            {
                return 'F';
            }

            throw new ApplicationException("Invalid pipe");
        }

        private int CountInside(char[][] map, int[][] distances)
        {
            int count = 0;
            for (int y = 0; y < distances.Length; y++)
            {
                bool outside = true;
                char previousBend = '.';
                for (int x = 0; x < distances[0].Length; x++)
                {
                    if (distances[y][x] == -1)
                    {
                        if (!outside)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        if (map[y][x] == '|' || IsOppositeBend(map[y][x], previousBend))
                        {
                            outside = !outside;
                        }

                        if (bendChars.Contains(map[y][x]))
                        {
                            previousBend = map[y][x];
                        }
                    }
                }
            }

            return count;
        }

        private static bool IsOppositeBend(char current, char previous)
        {
            return (current == 'J' && previous == 'F') || (current == '7' && previous == 'L');
        }

        private static int[][] InitializeDistanceMap(int width, int height)
        {
            int[][] distances = new int[height][];
            for (int y = 0; y < height; y++)
            {
                distances[y] = new int[width];
                for (int x = 0; x < width; x++)
                {
                    distances[y][x] = -1;
                }
            }

            return distances;
        }
    }
}
