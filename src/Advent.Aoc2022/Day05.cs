namespace Advent.Aoc2022
{
    public class Day05
    {
        private readonly IInput input;

        public Day05(IInput input)
        {
            this.input = input;
        }
        public string Part1()
        {
            var lines = input.GetLines().ToList();
            var crateGame = new CrateGame(lines);
            crateGame.ExecuteInstructions1();
            return crateGame.GetTopCrates();
        }

        public string Part2()
        {
            var lines = input.GetLines().ToList();
            var crateGame = new CrateGame(lines);
            crateGame.ExecuteInstructions2();
            return crateGame.GetTopCrates();
        }

        private class CrateGame
        {
            private readonly Regex instructionRegex = new(@"move (\d+) from (\d+) to (\d+)");

            private readonly Dictionary<int, Stack<char>> stacks;
            private readonly List<(int count, int from, int to)> instructions;


            public CrateGame(List<string> input)
            {
                int blankIndex = input.IndexOf(string.Empty);
                string axis = input[blankIndex - 1];
                int size = (axis.Length + 1) / 4;

                stacks = new Dictionary<int, Stack<char>>();
                for (int i = 1; i <= size; i++)
                {
                    stacks[i] = new Stack<char>();
                }

                foreach (string line in input.Take(blankIndex - 1).Reverse())
                {
                    for (int i = 0; i < size; i++)
                    {
                        char crate = line[(4 * i) + 1];
                        if (crate != ' ')
                        {
                            stacks[i + 1].Push(crate);
                        }
                    }
                }

                instructions = input.Skip(blankIndex + 1).Select(ParseInstruction).ToList();
            }

            private (int count, int from, int to) ParseInstruction(string input)
            {
                var groups = instructionRegex.Match(input).Groups;
                return (int.Parse(groups[1].Value), int.Parse(groups[2].Value), int.Parse(groups[3].Value));
            }

            internal void ExecuteInstructions1()
            {
                foreach (var (count, from, to) in instructions)
                {
                    for (int i = 0; i < count; i++)
                    {
                        stacks[to].Push(stacks[from].Pop());
                    }
                }
            }

            internal void ExecuteInstructions2()
            {
                var buffer = new Stack<char>();
                foreach (var (count, from, to) in instructions)
                {
                    for (int i = 0; i < count; i++)
                    {
                        buffer.Push(stacks[from].Pop());
                    }

                    for (int i = 0; i < count; i++)
                    {
                        stacks[to].Push(buffer.Pop());
                    }
                }
            }

            internal string GetTopCrates()
            {
                return string.Concat(stacks.Select(s => s.Value.Peek()));
            }
        }
    }
}