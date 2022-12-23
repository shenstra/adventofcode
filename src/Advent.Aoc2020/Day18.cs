namespace Advent.Aoc2020
{
    public class Day18
    {
        private readonly IInput input;

        public Day18(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var lines = input.GetLines();
            var solutions = lines.Select(l => Solve(l, addFirst: false));
            Console.WriteLine(solutions.Sum());
        }

        public void Part2()
        {
            var lines = input.GetLines();
            var solutions = lines.Select(l => Solve(l, addFirst: true));
            Console.WriteLine(solutions.Sum());
        }

        private long Solve(string input, bool addFirst)
        {
            while (input.Contains('('))
            {
                (int openParen, int closeParen) = FindMatchingParentheses(input);
                long subResult = Solve(input[(openParen + 1)..(closeParen - 1)], addFirst);
                input = $"{input[..openParen]}{subResult}{input[closeParen..]}";
            }

            return addFirst ? SolveReducedAddingFirst(input) : SolveReducedLeftToRight(input);
        }

        private static (int, int) FindMatchingParentheses(string input)
        {
            int openIndex = input.IndexOf('(');
            int openParens = 1;
            for (int index = openIndex + 1; index < input.Length; index++)
            {
                if (input[index] == '(')
                {
                    openParens++;
                }
                else if (input[index] == ')')
                {
                    if (--openParens == 0)
                    {
                        return (openIndex, index + 1);
                    }
                }
            }

            throw new ApplicationException("Couldn't match parentheses");
        }

        private static long SolveReducedLeftToRight(string input)
        {
            string[] parts = input.Split(" ");
            long result = long.Parse(parts[0]);
            for (int index = 1; index < parts.Length; index += 2)
            {
                if (parts[index] == "+")
                {
                    result += long.Parse(parts[index + 1]);
                }
                else if (parts[index] == "*")
                {
                    result *= long.Parse(parts[index + 1]);
                }
            }

            return result;
        }

        private static long SolveReducedAddingFirst(string input)
        {
            return input.Split(" * ").Select(s => s.Split(" + ").Select(s => long.Parse(s)).Sum()).Aggregate((a, b) => a * b);
        }
    }
}