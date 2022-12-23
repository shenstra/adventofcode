namespace Advent.Aoc2016
{
    public class Day01
    {
        private readonly IInput input;

        public Day01(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            string[] instructions = input.GetSingleLine().Split(", ");
            var (x, y) = FollowInstructions(instructions, stopOnSecondVisit: false);
            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }

        public void Part2()
        {
            string[] instructions = input.GetSingleLine().Split(", ");
            var (x, y) = FollowInstructions(instructions, stopOnSecondVisit: true);
            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }

        private static (int x, int y) FollowInstructions(string[] instructions, bool stopOnSecondVisit)
        {
            (int x, int y) = (0, 0);
            char direction = 'N';
            var visited = new List<(int x, int y)>();
            foreach (string instruction in instructions)
            {
                direction = AdjustDirection(direction, instruction);
                var (dx, dy) = GetDeltas(direction);
                int steps = int.Parse(instruction[1..]);
                for (int i = 0; i < steps; i++)
                {
                    x += dx;
                    y += dy;
                    if (stopOnSecondVisit && visited.Contains((x, y)))
                    {
                        return (x, y);
                    }

                    visited.Add((x, y));
                }
            }

            return (x, y);
        }

        private static char AdjustDirection(char direction, string instruction)
        {
            return (direction, instruction[0]) switch
            {
                ('N', 'R') or ('S', 'L') => 'E',
                ('E', 'R') or ('W', 'L') => 'S',
                ('S', 'R') or ('N', 'L') => 'W',
                ('W', 'R') or ('E', 'L') => 'N',
                _ => throw new InvalidOperationException($"Invalid direction {direction} or instruction {instruction[0]}"),
            };
        }

        private static (int dx, int dy) GetDeltas(char direction)
        {
            return direction switch
            {
                'N' => (0, 1),
                'E' => (1, 0),
                'S' => (0, -1),
                'W' => (-1, 0),
                _ => throw new InvalidOperationException($"Invalid direction {direction}"),
            };
        }
    }
}
