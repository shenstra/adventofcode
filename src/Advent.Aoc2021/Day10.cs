namespace Advent.Aoc2021
{
    public class Day10(IInput input)
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
            var lines = input.GetLines();
            Console.WriteLine(lines.Sum(DetermineCorruptionScore));
        }

        public void Part2()
        {
            var lines = input.GetLines();
            long[] scores =
            [
                .. lines.Select(DetermineCompletionScore)
                                .Where(s => s >= 0)
                                .OrderBy(s => s)
,
            ];
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
            while (stack.Count != 0)
            {
                score = (score * 5) + completionValue[stack.Pop()];
            }

            return score;
        }
    }
}
