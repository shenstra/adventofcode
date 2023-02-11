namespace Advent.Aoc2022
{
    public class Day14
    {
        private readonly IInput input;

        public Day14(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            var map = ParseMap(input.GetLines());

            int grainsDropped = 0;
            while (DropGrain(map))
            {
                grainsDropped++;
            }

            return grainsDropped;
        }

        public int Part2()
        {
            var map = ParseMap(input.GetLines());
            AddFloor(map);

            int grainsDropped = 0;
            while (!map.ContainsKey((500, 0)))
            {
                DropGrain(map);
                grainsDropped++;
            }

            return grainsDropped;
        }

        private bool DropGrain(Dictionary<(int x, int y), char> map)
        {
            int x = 500;
            int maxY = map.Keys.Max(coordinate => coordinate.y);

            for (int y = 0; y < maxY; y++)
            {
                if (map.ContainsKey((x, y + 1)))
                {
                    if (!map.ContainsKey((x - 1, y + 1)))
                    {
                        x--;
                    }
                    else if (!map.ContainsKey((x + 1, y + 1)))
                    {
                        x++;
                    }
                    else
                    {
                        map[(x, y)] = 'O';
                        return true;
                    }
                }
            }

            return false;
        }

        private void AddFloor(Dictionary<(int x, int y), char> map)
        {
            int y = map.Keys.Max(coordinate => coordinate.y) + 2;

            for (int x = 500 - y; x <= 500 + y; x++)
            {
                map[(x, y)] = '#';
            }
        }

        private Dictionary<(int x, int y), char> ParseMap(IEnumerable<string> lines)
        {
            var map = new Dictionary<(int x, int y), char>();

            foreach (var coordinate in lines.SelectMany(ParseCoordinates))
            {
                map[coordinate] = '#';
            }

            return map;
        }

        private IEnumerable<(int x, int y)> ParseCoordinates(string line)
        {
            var nodes = line.Split(" -> ").Select(ParseCoordinate).ToList();

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                var from = nodes[i];
                var to = nodes[i + 1];

                for (int x = Math.Min(from.x, to.x); x <= Math.Max(from.x, to.x); x++)
                {
                    for (int y = Math.Min(from.y, to.y); y <= Math.Max(from.y, to.y); y++)
                    {
                        yield return (x, y);
                    }
                }
            }
        }

        private (int x, int y) ParseCoordinate(string input)
        {
            var parts = input.Split(",");
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}