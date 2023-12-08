namespace Advent.Aoc2022
{
    public class Day09(IInput input)
    {
        public int Part1()
        {
            var instructions = input.GetLines().Select(MapInstruction).ToList();
            var trail = FindTrail(instructions, 2);
            return trail.Distinct().Count();
        }

        public int Part2()
        {
            var instructions = input.GetLines().Select(MapInstruction).ToList();
            var trail = FindTrail(instructions, 10);
            return trail.Distinct().Count();
        }

        private IEnumerable<(int x, int y)> FindTrail(List<(char direction, int distance)> instructions, int length)
        {
            var positions = new (int x, int y)[length];
            for (int i = 0; i < length; i++)
            {
                positions[i] = (0, 0);
            }

            foreach (var (direction, distance) in instructions)
            {
                for (int i = 0; i < distance; i++)
                {
                    switch (direction)
                    {
                        case 'R':
                            positions[0].x++;
                            AdjustRope(positions);
                            yield return positions.Last();
                            break;
                        case 'L':
                            positions[0].x--;
                            AdjustRope(positions);
                            yield return positions.Last();
                            break;
                        case 'U':
                            positions[0].y++;
                            AdjustRope(positions);
                            yield return positions.Last();
                            break;
                        case 'D':
                            positions[0].y--;
                            AdjustRope(positions);
                            yield return positions.Last();
                            break;
                        default:
                            throw new ApplicationException($"Invalid direction {direction}");
                    }
                }
            }
        }

        private void AdjustRope((int x, int y)[] positions)
        {
            for (int i = 1; i < positions.Length; i++)
            {
                positions[i] = NewPosition(positions[i], positions[i - 1]);
            }
        }

        private (int x, int y) NewPosition((int x, int y) follower, (int x, int y) target)
        {
            if (follower.x < target.x - 1)
            {
                follower.x++;
                if (follower.y < target.y)
                {
                    follower.y++;
                }
                else if (follower.y > target.y)
                {
                    follower.y--;
                }
            }
            else if (follower.x > target.x + 1)
            {
                follower.x--;
                if (follower.y < target.y)
                {
                    follower.y++;
                }
                else if (follower.y > target.y)
                {
                    follower.y--;
                }
            }
            else if (follower.y < target.y - 1)
            {
                follower.y++;
                if (follower.x < target.x)
                {
                    follower.x++;
                }
                else if (follower.x > target.x)
                {
                    follower.x--;
                }
            }
            else if (follower.y > target.y + 1)
            {
                follower.y--;
                if (follower.x < target.x)
                {
                    follower.x++;
                }
                else if (follower.x > target.x)
                {
                    follower.x--;
                }
            }

            return follower;
        }

        private (char direction, int distance) MapInstruction(string line)
        {
            return (line[0], int.Parse(line[2..]));
        }
    }
}