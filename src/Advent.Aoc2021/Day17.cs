using Advent.Util;
using System.Text.RegularExpressions;

namespace Advent.Aoc2021
{
    public class Day17
    {
        private readonly IInput input;
        private readonly Regex targetRegex = new(@"target area: x=(-?\d+)..(-?\d+), y=(-?\d+)..(-?\d+)");
        private const int MaxDeltaY = 1000;

        public Day17(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var target = ParseTarget(input.GetSingleLine());
            Console.WriteLine(FindHighPoints(target).Max());
        }

        public void Part2()
        {
            var target = ParseTarget(input.GetSingleLine());
            Console.WriteLine(FindHighPoints(target).Count());
        }

        private static IEnumerable<int> FindHighPoints(Target target)
        {
            uint minDeltaX = (uint)Math.Sqrt(target.MinX * 2);
            for (uint deltaX = minDeltaX; deltaX <= target.MaxX; deltaX++)
            {
                for (int deltaY = target.MinY; deltaY < MaxDeltaY; deltaY++)
                {
                    if (HitsTarget(new(deltaX, deltaY), target, out int maxY))
                    {
                        yield return maxY;
                    }
                }
            }
        }

        private static bool HitsTarget(Velocity velocity, Target target, out int maxY)
        {
            var position = new Position(0, 0);
            maxY = 0;

            while (true)
            {
                if (IsInTarget(position, target))
                {
                    return true;
                }

                if (!StillOnTarget(position, target, velocity))
                {
                    return false;
                }

                (position, velocity) = AdvanceProjectile(position, velocity);
                maxY = Math.Max(maxY, position.Y);
            }
        }

        private static (Position, Velocity) AdvanceProjectile(Position position, Velocity velocity)
        {
            return (new Position(position.X + velocity.X, position.Y + velocity.Y),
                new Velocity(velocity.X > 0 ? velocity.X - 1 : 0, velocity.Y - 1));
        }

        private static bool IsInTarget(Position position, Target target)
        {
            return position.X >= target.MinX &&
                position.X <= target.MaxX &&
                position.Y >= target.MinY &&
                position.Y <= target.MaxY;
        }

        private static bool StillOnTarget(Position current, Target target, Velocity velocity)
        {
            if (velocity.X == 0)
            {
                return current.X >= target.MinX && current.X <= target.MaxX && current.Y >= target.MinY;
            }
            else if (current.X > target.MaxX)
            {
                return false;
            }

            return velocity.Y > 0 || current.Y > target.MinY;
        }

        private Target ParseTarget(string input)
        {
            var groups = targetRegex.Match(input).Groups;
            return new Target(
                int.Parse(groups[1].Value),
                int.Parse(groups[2].Value),
                int.Parse(groups[3].Value),
                int.Parse(groups[4].Value));
        }

        internal record struct Target(int MinX, int MaxX, int MinY, int MaxY);
        internal record struct Position(uint X, int Y);
        internal record struct Velocity(uint X, int Y);
    }
}