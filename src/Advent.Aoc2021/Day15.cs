namespace Advent.Aoc2021
{
    public class Day15(IInput input)
    {
        public void Part1()
        {
            int[,] riskLevels = ParseRiskLevels(input.GetLines().ToList());
            Console.WriteLine(CalculateMinimumRisk(riskLevels));
        }

        public void Part2()
        {
            int[,] riskLevels = ParseRiskLevels(input.GetLines().ToList());
            Console.WriteLine(CalculateMinimumRisk(ScaleUp(riskLevels)));
        }

        private int CalculateMinimumRisk(int[,] riskLevels)
        {
            int size = riskLevels.GetLength(0);
            int[,] cumulativeRisk = new int[size, size];
            var toTry = new PriorityQueue<(int x, int y), int>();
            toTry.Enqueue((0, 0), 0);
            while (toTry.Count > 0)
            {
                var current = toTry.Dequeue();
                foreach ((int x, int y) in EnumerateNeighbors(current.x, current.y, size))
                {
                    if (cumulativeRisk[x, y] == 0 && (x, y) != (0, 0))
                    {
                        cumulativeRisk[x, y] = cumulativeRisk[current.x, current.y] + riskLevels[x, y];
                        toTry.Enqueue((x, y), cumulativeRisk[x, y]);
                    }
                }
            }

            return cumulativeRisk[size - 1, size - 1];
        }

        private IEnumerable<(int x, int y)> EnumerateNeighbors(int x, int y, int size)
        {
            if (x > 0)
            {
                yield return (x - 1, y);
            }

            if (x < size - 1)
            {
                yield return (x + 1, y);
            }

            if (y > 0)
            {
                yield return (x, y - 1);
            }

            if (y < size - 1)
            {
                yield return (x, y + 1);
            }
        }

        private int[,] ScaleUp(int[,] original)
        {
            int size = original.GetLength(0);
            int[,] scaledUp = new int[size * 5, size * 5];
            for (int tileX = 0; tileX < 5; tileX++)
            {
                for (int tileY = 0; tileY < 5; tileY++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            int targetX = (tileX * size) + x;
                            int targetY = (tileY * size) + y;
                            scaledUp[targetX, targetY] = original[x, y] + tileX + tileY;
                            if (scaledUp[targetX, targetY] > 9)
                            {
                                scaledUp[targetX, targetY] -= 9;
                            }
                        }
                    }
                }
            }

            return scaledUp;
        }

        private int[,] ParseRiskLevels(List<string> lines)
        {
            int[,] riskLevels = new int[lines.Count, lines[0].Length];
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    riskLevels[x, y] = lines[y][x] - '0';
                }
            }

            return riskLevels;
        }
    }
}