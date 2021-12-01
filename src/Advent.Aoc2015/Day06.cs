using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day06
    {
        private const int size = 1000;
        private readonly Regex instructionRegex = new Regex(@"^(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)$");

        public void Part1()
        {
            var lines = Input.GetLines(2015, 6);
            bool[] lights = new bool[size * size];
            ApplyToggleInstructions(lines, lights);
            Console.WriteLine(lights.Count(l => l));
        }

        public void Part2()
        {
            var lines = Input.GetLines(2015, 6);
            int[] lights = new int[size * size];
            ApplyDimmerInstructions(lines, lights);
            Console.WriteLine(lights.Sum());
        }

        private void ApplyToggleInstructions(IEnumerable<string> lines, bool[] lights)
        {
            foreach (string line in lines)
            {
                var match = instructionRegex.Match(line);
                int x1 = int.Parse(match.Groups[2].Value);
                int y1 = int.Parse(match.Groups[3].Value);
                int x2 = int.Parse(match.Groups[4].Value);
                int y2 = int.Parse(match.Groups[5].Value);
                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        lights[x + (y * size)] = match.Groups[1].Value switch
                        {
                            "turn on" => true,
                            "turn off" => false,
                            "toggle" => !lights[x + (y * size)],
                            _ => throw new ApplicationException($"Unknown instruction {match.Groups[1].Value}"),
                        };
                    }
                }
            }
        }

        private void ApplyDimmerInstructions(IEnumerable<string> lines, int[] lights)
        {
            foreach (string line in lines)
            {
                var match = instructionRegex.Match(line);
                int x1 = int.Parse(match.Groups[2].Value);
                int y1 = int.Parse(match.Groups[3].Value);
                int x2 = int.Parse(match.Groups[4].Value);
                int y2 = int.Parse(match.Groups[5].Value);
                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "turn on":
                                lights[x + (y * size)] += 1;
                                break;
                            case "turn off":
                                if (lights[x + (y * size)] > 0)
                                {
                                    lights[x + (y * size)] -= 1;
                                }

                                break;
                            case "toggle":
                                lights[x + (y * size)] += 2;
                                break;
                            default:
                                throw new ApplicationException($"Unknown instruction {match.Groups[1].Value}");
                        }
                    }
                }
            }
        }
    }
}
