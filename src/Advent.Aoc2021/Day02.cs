using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day02
    {
        private readonly Regex instructionRegex = new(@"^(forward|down|up) (\d+)$");

        public void Part1()
        {
            var instructions = Input.GetLines(2021, 2).Select(MapToInstruction).ToList();
            (int position, int depth) = (0, 0);
            foreach ((string command, int value) in instructions)
            {
                switch (command)
                {
                    case "forward":
                        position += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    default: throw new ApplicationException($"Invalid instruction: {command}");
                }
            }
            Console.WriteLine(position * depth);
        }

        public void Part2()
        {
            var instructions = Input.GetLines(2021, 2).Select(MapToInstruction).ToList();
            (int position, int depth, int aim) = (0, 0, 0);
            foreach ((string command, int value) in instructions)
            {
                switch (command)
                {
                    case "forward":
                        position += value;
                        depth += value * aim;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    default: throw new ApplicationException($"Invalid instruction: {command}");
                }
            }
            Console.WriteLine(position * depth);
        }

        private (string command, int value) MapToInstruction(string input)
        {
            var match = instructionRegex.Match(input);
            return (match.Groups[1].Value, int.Parse(match.Groups[2].Value));
        }
    }
}