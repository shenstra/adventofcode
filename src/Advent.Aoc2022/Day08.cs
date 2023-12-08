namespace Advent.Aoc2022
{
    public class Day08(IInput input)
    {
        public int Part1()
        {
            var lines = input.GetLines().ToList();
            char[,] map = GetMap(lines);
            bool[,] visibility = BuildVisibilityMap(map);
            return visibility.AsEnumerable().Count(visible => visible);
        }

        public int Part2()
        {
            var lines = input.GetLines().ToList();
            char[,] map = GetMap(lines);
            return ScenicScores(map).Max();
        }

        private static bool[,] BuildVisibilityMap(char[,] map)
        {
            bool[,] visibility = new bool[(map.GetLength(0)), (map.GetLength(1))];
            ScanUp(map, visibility);
            ScanDown(map, visibility);
            ScanLeft(map, visibility);
            ScanRight(map, visibility);
            return visibility;
        }

        private static void ScanUp(char[,] map, bool[,] visible)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                int highest = -1;
                for (int y = map.GetLength(1) - 1; y > 0; y--)
                {
                    if (map[x, y] > highest)
                    {
                        visible[x, y] = true;
                        highest = map[x, y];
                    }
                }
            }
        }

        private static void ScanDown(char[,] map, bool[,] visible)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                int highest = -1;
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] > highest)
                    {
                        visible[x, y] = true;
                        highest = map[x, y];
                    }
                }
            }
        }

        private static void ScanLeft(char[,] map, bool[,] visible)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int highest = -1;
                for (int x = map.GetLength(0) - 1; x > 0; x--)
                {
                    if (map[x, y] > highest)
                    {
                        visible[x, y] = true;
                        highest = map[x, y];
                    }
                }
            }
        }

        private static void ScanRight(char[,] map, bool[,] visible)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int highest = -1;
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] > highest)
                    {
                        visible[x, y] = true;
                        highest = map[x, y];
                    }
                }
            }
        }

        private IEnumerable<int> ScenicScores(char[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    yield return VisibleLeft(map, x, y) * VisibleRight(map, x, y) * VisibleUp(map, x, y) * VisibleDown(map, x, y);
                }
            }
        }

        private static int VisibleLeft(char[,] map, int x, int y)
        {
            int visible = 0;
            if (x > 0)
            {
                for (int targetX = x - 1; targetX >= 0; targetX--)
                {
                    visible++;
                    if (map[targetX, y] >= map[x, y])
                    {
                        break;
                    }
                }
            }

            return visible;
        }

        private static int VisibleRight(char[,] map, int x, int y)
        {
            int visible = 0;
            if (x < map.GetLength(0))
            {
                for (int targetX = x + 1; targetX < map.GetLength(0); targetX++)
                {
                    visible++;
                    if (map[targetX, y] >= map[x, y])
                    {
                        break;
                    }
                }
            }

            return visible;
        }

        private static int VisibleUp(char[,] map, int x, int y)
        {
            int visible = 0;
            if (y > 0)
            {
                for (int targetY = y - 1; targetY >= 0; targetY--)
                {
                    visible++;
                    if (map[x, targetY] >= map[x, y])
                    {
                        break;
                    }
                }
            }

            return visible;
        }

        private static int VisibleDown(char[,] map, int x, int y)
        {
            int visible = 0;
            if (y < map.GetLength(1))
            {
                for (int targetY = y + 1; targetY < map.GetLength(1); targetY++)
                {
                    visible++;
                    if (map[x, targetY] >= map[x, y])
                    {
                        break;
                    }
                }
            }

            return visible;
        }

        private char[,] GetMap(List<string> lines)
        {
            int width = lines[0].Length;
            int height = lines.Count;
            char[,] map = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = lines[y][x];
                }
            }

            return map;
        }
    }
}