namespace Advent.Aoc2015
{
    public class Day03(IInput input)
    {
        public void Part1()
        {
            string line = input.GetSingleLine();
            var history = new List<(int, int)> { (0, 0) };
            int x = 0, y = 0;
            foreach (char c in line)
            {
                switch (c)
                {
                    case '^': y--; break;
                    case 'v': y++; break;
                    case '>': x++; break;
                    case '<': x--; break;
                    default: throw new ApplicationException($"Invalid input: {c}");
                }

                history.Add((x, y));
            }

            Console.WriteLine(history.Distinct().Count());
        }

        public void Part2()
        {
            string line = input.GetSingleLine();
            var history = new List<(int, int)> { (0, 0) };
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            for (int i = 0; i < line.Length; i += 2)
            {
                switch (line[i])
                {
                    case '^': y1--; break;
                    case 'v': y1++; break;
                    case '>': x1++; break;
                    case '<': x1--; break;
                    default: throw new ApplicationException($"Invalid input: {line[i]}");
                }

                history.Add((x1, y1));

                switch (line[i + 1])
                {
                    case '^': y2--; break;
                    case 'v': y2++; break;
                    case '>': x2++; break;
                    case '<': x2--; break;
                    default: throw new ApplicationException($"Invalid input: {line[i + 1]}");
                }

                history.Add((x2, y2));
            }

            Console.WriteLine(history.Distinct().Count());
        }
    }
}
