using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day17
    {
        public void Part1()
        {
            var activeCubes = GetActiveCubes(Input.GetLines(2020, 17).ToList());
            for (int cycle = 0; cycle < 6; cycle++)
            {
                activeCubes = RunCycle(activeCubes);
            }

            Console.WriteLine(activeCubes.Count);
        }

        public void Part2()
        {
            var activeHypercubes = GetActiveHyperCubes(Input.GetLines(2020, 17).ToList());
            for (int cycle = 0; cycle < 6; cycle++)
            {
                activeHypercubes = RunHyperCycle(activeHypercubes);
            }

            Console.WriteLine(activeHypercubes.Count);
        }

        private static List<(int, int, int)> GetActiveCubes(List<string> lines)
        {
            var activeCubes = new List<(int, int, int)>();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        activeCubes.Add((x, y, 0));
                    }
                }
            }

            return activeCubes;
        }

        private static List<(int, int, int, int)> GetActiveHyperCubes(List<string> lines)
        {
            var activeCubes = new List<(int, int, int, int)>();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        activeCubes.Add((x, y, 0, 0));
                    }
                }
            }

            return activeCubes;
        }

        private static List<(int, int, int)> RunCycle(IEnumerable<(int x, int y, int z)> activeCubes)
        {
            var newActiveCubes = new List<(int, int, int)>();
            for (int z = activeCubes.Min(cube => cube.z) - 1; z <= activeCubes.Max(cube => cube.z) + 1; z++)
            {
                for (int y = activeCubes.Min(cube => cube.y) - 1; y <= activeCubes.Max(cube => cube.y) + 1; y++)
                {
                    for (int x = activeCubes.Min(cube => cube.x) - 1; x <= activeCubes.Max(cube => cube.x) + 1; x++)
                    {
                        if (ShouldBeActive(activeCubes, x, y, z))
                        {
                            newActiveCubes.Add((x, y, z));
                        }
                    }
                }
            }

            return newActiveCubes;
        }

        private static List<(int, int, int, int)> RunHyperCycle(IEnumerable<(int x, int y, int z, int w)> activeCubes)
        {
            var newActiveCubes = new List<(int, int, int, int)>();
            for (int z = activeCubes.Min(cube => cube.z) - 1; z <= activeCubes.Max(cube => cube.z) + 1; z++)
            {
                for (int y = activeCubes.Min(cube => cube.y) - 1; y <= activeCubes.Max(cube => cube.y) + 1; y++)
                {
                    for (int x = activeCubes.Min(cube => cube.x) - 1; x <= activeCubes.Max(cube => cube.x) + 1; x++)
                    {
                        for (int w = activeCubes.Min(cube => cube.w) - 1; w <= activeCubes.Max(cube => cube.w) + 1; w++)
                        {
                            if (ShouldBeHyperActive(activeCubes, x, y, z, w))
                            {
                                newActiveCubes.Add((x, y, z, w));
                            }
                        }
                    }
                }
            }

            return newActiveCubes;
        }

        private static bool ShouldBeActive(IEnumerable<(int, int, int)> activeCubes, int x, int y, int z)
        {
            int activeNeighbourhood = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        if (activeCubes.Contains((x + dx, y + dy, z + dz)))
                        {
                            activeNeighbourhood++;
                        }
                    }
                }
            }

            return activeCubes.Contains((x, y, z)) ? activeNeighbourhood is 3 or 4 : activeNeighbourhood == 3;
        }

        private static bool ShouldBeHyperActive(IEnumerable<(int, int, int, int)> activeCubes, int x, int y, int z, int w)
        {
            int activeNeighbourhood = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        for (int dw = -1; dw <= 1; dw++)
                        {
                            if (activeCubes.Contains((x + dx, y + dy, z + dz, w + dw)))
                            {
                                activeNeighbourhood++;
                            }
                        }
                    }
                }
            }

            return activeCubes.Contains((x, y, z, w)) ? activeNeighbourhood is 3 or 4 : activeNeighbourhood == 3;
        }
    }
}
