using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day10
    {
        private readonly Dictionary<char, char> openingMatch =
            new()
            {
                [')'] = '(',
                [']'] = '[',
                ['}'] = '{',
                ['>'] = '<'
            };

        private readonly Dictionary<char, int> corruptionValue =
            new()
            {
                [')'] = 3,
                [']'] = 57,
                ['}'] = 1197,
                ['>'] = 25137
            };

        private readonly Dictionary<char, int> completionValue =
            new()
            {
                ['('] = 1,
                ['['] = 2,
                ['{'] = 3,
                ['<'] = 4
            };

        public void Part1()
        {
            var lines = Input.GetLines(2021, 10);
            Console.WriteLine(lines.Sum(DetermineCorruptionScore));
        }

        public void Part2()
        {
            var lines = Input.GetLines(2021, 10);
            long[] scores = lines.Select(DetermineCompletionScore)
                .Where(s => s >= 0)
                .OrderBy(s => s)
                .ToArray();
            Console.WriteLine(scores[scores.Length / 2]);
        }

        private int DetermineCorruptionScore(string input)
        {
            var stack = new Stack<char>();
            foreach (char c in input)
            {
                if (c is '(' or '[' or '{' or '<')
                {
                    stack.Push(c);
                }
                else if (c is ')' or ']' or '}' or '>')
                {
                    if (stack.Pop() != openingMatch[c])
                    {
                        return corruptionValue[c];
                    }
                }
            }

            return 0;
        }

        private long DetermineCompletionScore(string input)
        {
            var stack = new Stack<char>();
            foreach (char c in input)
            {
                if (c is '(' or '[' or '{' or '<')
                {
                    stack.Push(c);
                }
                else if (c is ')' or ']' or '}' or '>')
                {
                    if (stack.Pop() != openingMatch[c])
                    {
                        return -1;
                    }
                }
            }

            long score = 0;
            while (stack.Any())
            {
                score = (score * 5) + completionValue[stack.Pop()];
            }

            return score;
        }
    }
}
