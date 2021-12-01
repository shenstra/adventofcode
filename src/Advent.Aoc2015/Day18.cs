using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day18
    {
        public void Part1()
        {
            bool[,] lights = GetLights(Input.GetLines(2015, 18).ToList());
            for (int round = 0; round < 100; round++)
            {
                lights = ApplyRound(lights);
            }

            Console.WriteLine(CountActiveLights(lights));
        }

        public void Part2()
        {
            bool[,] lights = GetLights(Input.GetLines(2015, 18).ToList());
            lights[0, 0] = true;
            lights[0, lights.GetLength(1) - 1] = true;
            lights[lights.GetLength(0) - 1, 0] = true;
            lights[lights.GetLength(0) - 1, lights.GetLength(1) - 1] = true;
            for (int round = 0; round < 100; round++)
            {
                lights = ApplyRound(lights, cornerLightsBroken: true);
            }

            Console.WriteLine(CountActiveLights(lights));
        }

        private static bool[,] GetLights(List<string> lines)
        {
            bool[,] lights = new bool[lines.Count, lines.Count];
            for (int row = 0; row < lines.Count; row++)
            {
                for (int col = 0; col < lines.Count; col++)
                {
                    lights[row, col] = lines[row][col] == '#';
                }
            }

            return lights;
        }

        private static bool[,] ApplyRound(bool[,] lights, bool cornerLightsBroken = false)
        {
            int rows = lights.GetLength(0);
            int cols = lights.GetLength(1);
            bool[,] next = new bool[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    bool isBrokenCornerLight = cornerLightsBroken && (row == 0 || row == rows - 1) && (col == 0 || col == cols - 1);
                    next[row, col] = isBrokenCornerLight || ShouldBeActive(lights, row, col);
                }
            }

            return next;
        }

        private static bool ShouldBeActive(bool[,] lights, int row, int col)
        {
            int neighbours = 0;
            for (int dRow = -1; dRow <= 1; dRow++)
            {
                for (int dCol = -1; dCol <= 1; dCol++)
                {
                    if ((dRow != 0 || dCol != 0)
                        && row + dRow >= 0 && row + dRow < lights.GetLength(0)
                        && col + dCol >= 0 && col + dCol < lights.GetLength(1)
                        && lights[row + dRow, col + dCol])
                    {
                        neighbours++;
                    }
                }
            }

            return lights[row, col] ? neighbours is 2 or 3 : neighbours is 3;
        }

        private static int CountActiveLights(bool[,] lights)
        {
            int count = 0;
            for (int row = 0; row < lights.GetLength(0); row++)
            {
                for (int col = 0; col < lights.GetLength(1); col++)
                {
                    if (lights[row, col])
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
