using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day03(IInput input)
    {
        public int Part1()
        {
            string[] lines = input.GetLines().ToArray();
            return FindPartNumbers(lines).Sum();
        }

        public int Part2()
        {
            string[] lines = input.GetLines().ToArray();
            return FindGearRatios(lines).Sum();
        }

        private IEnumerable<int> FindPartNumbers(string[] lines)
        {
            int partNumber = 0;
            bool adjacentSymbol = false;
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (char.IsDigit(line[x]))
                    {
                        partNumber = (partNumber * 10) + line[x] - '0';
                        if (HasAdjacentSymbol(lines, x, y))
                        {
                            adjacentSymbol = true;
                        }
                    }

                    else if (partNumber > 0)
                    {
                        if (adjacentSymbol)
                        {
                            yield return partNumber;
                        }
                        partNumber = 0;
                        adjacentSymbol = false;
                    }
                }

                if (partNumber > 0)
                {
                    if (adjacentSymbol)
                    {
                        yield return partNumber;
                    }
                    partNumber = 0;
                    adjacentSymbol = false;
                }
            }
        }

        private bool HasAdjacentSymbol(string[] lines, int x, int y)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (x + dx < 0 || x + dx >= lines[y].Length)
                {
                    continue;
                }

                for (int dy = -1; dy <= 1; dy++)
                {
                    if (y + dy < 0 || y + dy >= lines.Length)
                    {
                        continue;
                    }

                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    char character = lines[y + dy][x + dx];
                    if (!char.IsDigit(character) && character != '.')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private IEnumerable<int> FindGearRatios(string[] lines)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '*')
                    {
                        int[] partNumbers = FindAdjacentPartNumbers(lines, x, y).ToArray();
                        if (partNumbers.Length == 2)
                        {
                            yield return partNumbers[0] * partNumbers[1];
                        }
                    }
                }
            }
        }

        private IEnumerable<int> FindAdjacentPartNumbers(string[] lines, int gearX, int gearY)
        {
            int partNumber = 0;
            bool adjacentSymbol = false;
            for (int y = gearY - 1; y <= gearY + 1; y++)
            {
                if (y < 0 || y >= lines.Length)
                {
                    continue;
                }

                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (char.IsDigit(line[x]))
                    {
                        partNumber = (partNumber * 10) + line[x] - '0';
                        if (x >= gearX - 1 && x <= gearX + 1)
                        {
                            adjacentSymbol = true;
                        }
                    }
                    else if (partNumber > 0)
                    {
                        if (adjacentSymbol)
                        {
                            yield return partNumber;
                        }
                        partNumber = 0;
                        adjacentSymbol = false;
                    }
                }

                if (partNumber > 0)
                {
                    if (adjacentSymbol)
                    {
                        yield return partNumber;
                    }
                    partNumber = 0;
                    adjacentSymbol = false;
                }
            }
        }
    }
}
