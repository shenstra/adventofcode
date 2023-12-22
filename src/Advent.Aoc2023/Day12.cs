namespace Advent.Aoc2023
{
    public class Day12(IInput input)
    {
        public long Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var nonogramLines = lines.Select(l => new NonogramLine(l)).ToArray();
            return nonogramLines.Sum(n => n.GetPossibleConfigurations());
        }

        public long Part2()
        {
            string[] lines = input.GetLines().ToArray();
            var nonogramLines = lines.Select(l => new NonogramLine(l).Unfold(5)).ToArray();
            return nonogramLines.Sum(n => n.GetPossibleConfigurations());
        }

        private class NonogramLine(string input)
        {
            private string state = input.Split(' ')[0];
            private int[] clues = input.Split(' ')[1].Split(',').Select(int.Parse).ToArray();

            public NonogramLine Unfold(int count)
            {
                state = string.Concat(Enumerable.Repeat(state + '?', count))[..^1];
                clues = Enumerable.Repeat(clues, count).SelectMany(x => x).ToArray();
                return this;
            }

            private readonly Dictionary<(int, int, int), long> memoizedAnswers = [];

            public long GetPossibleConfigurations(int startIndex = 0, int startCount = 0, int startClue = 0)
            {
                if (memoizedAnswers.TryGetValue((startIndex, startCount, startClue), out long value))
                {
                    return value;
                }

                (int count, int clue) = (startCount, startClue);
                for (int index = startIndex; index < state.Length; index++)
                {
                    if (state[index] == '#')
                    {
                        count++;
                    }
                    else if (state[index] == '.' && count > 0)
                    {
                        if (clue >= clues.Length)
                        {
                            return memoizedAnswers[(startIndex, startCount, startClue)] = 0;
                        }
                        if (count != clues[clue])
                        {
                            return memoizedAnswers[(startIndex, startCount, startClue)] = 0;
                        }
                        count = 0;
                        clue++;
                    }
                    else if (state[index] == '?')
                    {
                        // # case
                        long result = GetPossibleConfigurations(index + 1, count + 1, clue);

                        // . case
                        if (count == 0)
                        {
                            result += GetPossibleConfigurations(index + 1, 0, clue);
                        }
                        else if (clue < clues.Length && count == clues[clue])
                        {
                            result += GetPossibleConfigurations(index + 1, 0, clue + 1);
                        }

                        return memoizedAnswers[(startIndex, startCount, startClue)] = result;
                    }
                }

                if (count > 0)
                {
                    if (clue >= clues.Length)
                    {
                        return memoizedAnswers[(startIndex, startCount, startClue)] = 0;
                    }
                    if (count != clues[clue])
                    {
                        return memoizedAnswers[(startIndex, startCount, startClue)] = 0;
                    }
                    clue++;
                }

                long answer = clue == clues.Length ? 1 : 0;
                return memoizedAnswers[(startIndex, startCount, startClue)] = answer;
            }

            public override string ToString()
            {
                return $"{state} {string.Join(",", clues)}";
            }
        }
    }
}