namespace Advent.Aoc2021
{
    public class Day13(IInput input)
    {
        public void Part1()
        {
            (var dots, var instructions) = ParseManual(input.GetLines().ToList());
            (char axis, int degree) = instructions[0];
            dots = ApplyFold(dots, axis, degree);
            Console.WriteLine(dots.Count);
        }

        public void Part2()
        {
            (var dots, var instructions) = ParseManual(input.GetLines().ToList());
            foreach (var (axis, degree) in instructions)
            {
                dots = ApplyFold(dots, axis, degree);
            }
            PrintDots(dots);
        }

        private static List<(int x, int y)> ApplyFold(List<(int x, int y)> dots, char axis, int degree)
        {
            if (axis == 'x')
            {
                var dotsInPlace = dots.Where(d => d.x < degree);
                var dotsMirrored = dots.Where(d => d.x > degree).Select(d => ((2 * degree) - d.x, d.y));
                dots = dotsInPlace.Union(dotsMirrored).Distinct().ToList();
            }
            else
            {
                var dotsInPlace = dots.Where(d => d.y < degree);
                var dotsMirrored = dots.Where(d => d.y > degree).Select(d => (d.x, (2 * degree) - d.y));
                dots = dotsInPlace.Union(dotsMirrored).Distinct().ToList();
            }

            return dots;
        }

        private static void PrintDots(List<(int x, int y)> dots)
        {
            for (int y = dots.Min(d => d.y); y <= dots.Max(d => d.y); y++)
            {
                for (int x = dots.Min(d => d.x); x <= dots.Max(d => d.x); x++)
                {
                    Console.Write(dots.Contains((x, y)) ? "O " : "  ");
                }
                Console.WriteLine();
            }
        }

        private static (List<(int x, int y)> dots, List<(char axis, int degree)> instructions) ParseManual(List<string> input)
        {
            int whiteLineIndex = input.IndexOf(string.Empty);
            var dots = input.Take(whiteLineIndex).Select(ParseDot).ToList();
            var instructions = input.Skip(whiteLineIndex + 1).Select(ParseInstruction).ToList();
            return (dots, instructions);
        }

        private static (int x, int y) ParseDot(string input)
        {
            int[] numbers = input.SplitToInts();
            return (numbers[0], numbers[1]);
        }

        private static (char axis, int degree) ParseInstruction(string input)
        {
            string[] parts = input.Split('=');
            return (parts[0].Last(), int.Parse(parts[1]));
        }
    }
}