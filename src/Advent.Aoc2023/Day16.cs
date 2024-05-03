namespace Advent.Aoc2023
{
    public class Day16(IInput input)
    {
        public int Part1()
        {
            var lines = input.GetLines();
            var grid = new BeamGrid(lines);
            return grid.FindEnergyValue(0, 0, Direction.East);
        }

        public int Part2()
        {
            var lines = input.GetLines();
            var grid = new BeamGrid(lines);
            return grid.FindEnergyValues().Max();
        }

        [Flags]
        private enum Direction
        {
            North = 1,
            East = 2,
            South = 4,
            West = 8
        }

        private class BeamGrid
        {
            private readonly string[] grid;
            private readonly int width;
            private readonly int height;
            private readonly Direction[][] directionsVisited;

            public BeamGrid(IEnumerable<string> input)
            {
                grid = input.ToArray();
                width = input.First().Length;
                height = input.Count();
                directionsVisited = new Direction[width][];
                for (int i = 0; i < width; i++)
                {
                    directionsVisited[i] = new Direction[height];
                }
            }

            public int FindEnergyValue(int x, int y, Direction direction)
            {
                ShootBeam(x, y, direction);
                return directionsVisited.Sum(row => row.Count(v => v != 0));
            }

            public IEnumerable<int> FindEnergyValues()
            {
                for (int x = 0; x < width; x++)
                {
                    yield return FindEnergyValue(x, 0, Direction.South);
                    ResetDirectionsVisited();
                    yield return FindEnergyValue(x, height - 1, Direction.North);
                    ResetDirectionsVisited();
                }

                for (int y = 0; y < height; y++)
                {
                    yield return FindEnergyValue(0, y, Direction.East);
                    ResetDirectionsVisited();
                    yield return FindEnergyValue(width - 1, y, Direction.West);
                    ResetDirectionsVisited();
                }
            }

            private void ShootBeam(int x, int y, Direction direction)
            {
                while (true)
                {
                    if (IsOutOfBounds(x, y) || directionsVisited[x][y].HasFlag(direction))
                    {
                        return;
                    }

                    directionsVisited[x][y] |= direction;
                    var newDirection = FindNewDirection(x, y, direction);

                    if (HitsSplitter(x, y, direction))
                    {
                        ShootBeam(x, y, ReverseDirection(newDirection));
                    }

                    direction = newDirection;
                    (x, y) = direction switch
                    {
                        Direction.North => (x, y - 1),
                        Direction.East => (x + 1, y),
                        Direction.South => (x, y + 1),
                        Direction.West => (x - 1, y),
                        _ => throw new InvalidOperationException()
                    };
                }
            }

            private bool HitsSplitter(int x, int y, Direction direction)
            {
                return grid[y][x] switch
                {
                    '-' => direction is Direction.North or Direction.South,
                    '|' => direction is Direction.East or Direction.West,
                    _ => false,
                };
            }

            private bool IsOutOfBounds(int x, int y)
            {
                return x < 0 || x >= width || y < 0 || y >= height;
            }

            private Direction FindNewDirection(int x, int y, Direction currentDirection)
            {
                char tile = grid[y][x];
                return tile switch
                {
                    '.' => currentDirection,
                    '-' => currentDirection switch
                    {
                        Direction.North => Direction.East,
                        Direction.East => Direction.East,
                        Direction.South => Direction.West,
                        Direction.West => Direction.West,
                        _ => throw new InvalidOperationException()
                    },
                    '|' => currentDirection switch
                    {
                        Direction.North => Direction.North,
                        Direction.East => Direction.South,
                        Direction.South => Direction.South,
                        Direction.West => Direction.North,
                        _ => throw new InvalidOperationException()
                    },
                    '/' => currentDirection switch
                    {
                        Direction.North => Direction.East,
                        Direction.East => Direction.North,
                        Direction.South => Direction.West,
                        Direction.West => Direction.South,
                        _ => throw new InvalidOperationException()
                    },
                    '\\' => currentDirection switch
                    {
                        Direction.North => Direction.West,
                        Direction.East => Direction.South,
                        Direction.South => Direction.East,
                        Direction.West => Direction.North,
                        _ => throw new InvalidOperationException()
                    },
                    _ => throw new InvalidOperationException()
                };
            }

            private static Direction ReverseDirection(Direction newDirection)
            {
                return newDirection switch
                {
                    Direction.North => Direction.South,
                    Direction.East => Direction.West,
                    Direction.South => Direction.North,
                    Direction.West => Direction.East,
                    _ => throw new InvalidOperationException()
                };
            }

            public void ResetDirectionsVisited()
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        directionsVisited[x][y] = 0;
                    }
                }
            }
        }
    }
}
