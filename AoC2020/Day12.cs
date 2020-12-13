using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day12
    {
        private readonly List<char> headings = new List<char> { 'N', 'E', 'S', 'W' };

        public void Problem1()
        {
            var instructions = Input.GetLines(2020, 12).Select(GetInstruction);
            (int x, int y) = FollowSimpleInstructions(instructions);
            int manhattanDistance = Math.Abs(x) + Math.Abs(y);
            Console.WriteLine(manhattanDistance);
        }

        public void Problem2()
        {
            var instructions = Input.GetLines(2020, 12).Select(GetInstruction);
            (int x, int y) = FollowWaypointInstructions(instructions);
            int manhattanDistance = Math.Abs(x) + Math.Abs(y);
            Console.WriteLine(manhattanDistance);
        }

        private (int, int) FollowSimpleInstructions(IEnumerable<(char, int)> instructions)
        {
            (int x, int y) position = (0, 0);
            char heading = 'E';
            foreach ((char instruction, int value) in instructions)
            {
                if (instruction is 'L' or 'R')
                    heading = MakeTurn(instruction, value, heading);
                else
                    position = Move(instruction == 'F' ? heading : instruction, value, position);
            }
            return position;
        }

        private static (int, int) FollowWaypointInstructions(IEnumerable<(char, int)> instructions)
        {
            (int x, int y) position = (0, 0);
            (int x, int y) waypoint = (10, 1);
            foreach ((char instruction, int value) in instructions)
            {
                if (instruction is 'L' or 'R')
                    waypoint = TurnWaypoint(instruction, value, waypoint);
                else if (instruction == 'F')
                    position = MoveTowardWaypoint(value, waypoint, position);
                else
                    waypoint = Move(instruction, value, waypoint);
            }
            return position;
        }

        private static (int, int) Move(char heading, int value, (int x, int y) position)
        {
            return heading switch
            {
                'N' => (position.x, position.y + value),
                'S' => (position.x, position.y - value),
                'E' => (position.x + value, position.y),
                'W' => (position.x - value, position.y),
                _ => throw new ApplicationException($"Invalid heading {heading}")
            };
        }

        private char MakeTurn(char instruction, int value, char heading)
        {
            int index = headings.IndexOf(heading);
            if (instruction == 'R')
                index += value / 90;
            else
                index -= value / 90;
            return headings[(index + 4) % 4];
        }

        private static (int, int) MoveTowardWaypoint(int value, (int x, int y) waypoint, (int x, int y) position)
        {
            return (position.x + (value * waypoint.x), position.y + (value * waypoint.y));
        }

        private static (int, int) TurnWaypoint(char instruction, int value, (int x, int y) waypoint)
        {
            return instruction switch
            {
                'R' when value == 90 => (waypoint.y, -waypoint.x),
                'L' when value == 270 => (waypoint.y, -waypoint.x),
                'R' or 'L' when value == 180 => (-waypoint.x, -waypoint.y),
                'R' when value == 270 => (-waypoint.y, waypoint.x),
                'L' when value == 90 => (-waypoint.y, waypoint.x),
                _ => throw new ApplicationException($"Invalid instruction {instruction} {value}")
            };
        }

        private (char, int) GetInstruction(string input)
        {
            return (input[0], int.Parse(input[1..]));
        }
    }
}
