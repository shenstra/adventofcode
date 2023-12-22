namespace Advent.Aoc2023
{
    public class Day14(IInput input)
    {
        public int Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var platform = new Platform(lines);
            platform.TiltNorth();
            return platform.GetNorthernLoad();
        }

        public int Part2()
        {
            string[] lines = input.GetLines().ToArray();
            var platform = new Platform(lines);
            platform.SpinCycles(1000000000);
            return platform.GetNorthernLoad();
        }

        private class Platform(string[] lines)
        {
            private readonly int height = lines.Length;
            private readonly int width = lines[0].Length;
            private readonly char[][] contents = lines.Select(lines => lines.ToCharArray()).ToArray();
            private readonly Dictionary<string, int> spinCycleLookup = [];

            public void SpinCycles(int cycles)
            {
                int remainingSpinCycles = SpinUntilCycleFound(cycles);
                for (int cycle = 0; cycle < remainingSpinCycles; cycle++)
                {
                    SpinCycle();
                }
            }

            private int SpinUntilCycleFound(int cycles)
            {
                for (int cycle = 0; cycle < cycles; cycle++)
                {
                    string before = string.Join("", contents.Select(c => new string(c)));
                    if (spinCycleLookup.TryGetValue(before, out int previousCycle))
                    {
                        return (cycles - cycle) % (cycle - previousCycle);
                    }
                    spinCycleLookup[before] = cycle;
                    SpinCycle();
                }

                return 0;
            }

            private void SpinCycle()
            {
                TiltNorth();
                TiltWest();
                TiltSouth();
                TiltEast();
            }

            public void TiltNorth()
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (contents[y][x] == '.')
                        {
                            for (int y2 = y + 1; y2 < height; y2++)
                            {
                                if (contents[y2][x] == 'O')
                                {
                                    contents[y][x] = 'O';
                                    contents[y2][x] = '.';
                                    break;
                                }
                                if (contents[y2][x] == '#')
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            public void TiltWest()
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (contents[y][x] == '.')
                        {
                            for (int x2 = x + 1; x2 < width; x2++)
                            {
                                if (contents[y][x2] == 'O')
                                {
                                    contents[y][x] = 'O';
                                    contents[y][x2] = '.';
                                    break;
                                }
                                if (contents[y][x2] == '#')
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            public void TiltSouth()
            {
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (contents[y][x] == '.')
                        {
                            for (int y2 = y - 1; y2 >= 0; y2--)
                            {
                                if (contents[y2][x] == 'O')
                                {
                                    contents[y][x] = 'O';
                                    contents[y2][x] = '.';
                                    break;
                                }
                                if (contents[y2][x] == '#')
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            public void TiltEast()
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (contents[y][x] == '.')
                        {
                            for (int x2 = x - 1; x2 >= 0; x2--)
                            {
                                if (contents[y][x2] == 'O')
                                {
                                    contents[y][x] = 'O';
                                    contents[y][x2] = '.';
                                    break;
                                }
                                if (contents[y][x2] == '#')
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            public int GetNorthernLoad()
            {
                int load = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (contents[y][x] == 'O')
                        {
                            load += height - y;
                        }
                    }
                }

                return load;
            }
        }
    }
}
