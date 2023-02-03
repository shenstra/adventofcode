namespace Advent.Aoc2022
{
    public class Day11
    {
        private readonly IInput input;

        public Day11(IInput input)
        {
            this.input = input;
        }

        public ulong Part1()
        {
            var monkeys = input.GetLines().Chunk(7).Select(lines => new Monkey(lines)).ToArray();
            PlayKeepAway(monkeys, rounds: 20, worryDivisor: 3);

            var topMonkeys = monkeys.OrderByDescending(m => m.ItemsInspected).Take(2).ToArray();
            return topMonkeys[0].ItemsInspected * topMonkeys[1].ItemsInspected;
        }

        public ulong Part2()
        {
            var monkeys = input.GetLines().Chunk(7).Select(lines => new Monkey(lines)).ToArray();
            PlayKeepAway(monkeys, rounds: 10000, worryDivisor: 1);

            var topMonkeys = monkeys.OrderByDescending(m => m.ItemsInspected).Take(2).ToArray();
            return topMonkeys[0].ItemsInspected * topMonkeys[1].ItemsInspected;
        }

        private static void PlayKeepAway(Monkey[] monkeys, ulong rounds, ulong worryDivisor)
        {
            ulong worryModulus = monkeys.Select(monkey => monkey.TestDivisor).Aggregate((a, b) => a * b);

            for (ulong round = 0; round < rounds; round++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.TakeTurn(monkeys, worryDivisor, worryModulus);
                }
            }
        }

        private class Monkey
        {
            private readonly List<ulong> heldItemValues;
            private readonly Func<ulong, ulong, ulong> operation;
            public ulong TestDivisor { get; private set; }
            private readonly ulong trueTarget;
            private readonly ulong falseTarget;
            public ulong ItemsInspected { get; private set; } = 0;

            public Monkey(string[] monkeyLines)
            {
                heldItemValues = monkeyLines[1].Split(": ").Last().Split(", ").Select(ulong.Parse).ToList();
                operation = ParseOperation(monkeyLines[2]);
                TestDivisor = ulong.Parse(monkeyLines[3].Split(' ').Last());
                trueTarget = ulong.Parse(monkeyLines[4].Split(' ').Last());
                falseTarget = ulong.Parse(monkeyLines[5].Split(' ').Last());
            }

            private Func<ulong, ulong, ulong> ParseOperation(string input)
            {
                string[] tokens = input.Split(" = ").Last().Split(" ").ToArray();
                ulong? left = tokens[0] == "old" ? null : ulong.Parse(tokens[0]);
                char operand = tokens[1][0];
                ulong? right = tokens[2] == "old" ? null : ulong.Parse(tokens[2]);
                return (value, modulus) => operand switch
                {
                    '+' => (left ?? value) + (right ?? value),
                    '*' => (left ?? value) * (right ?? value),
                    _ => throw new ApplicationException($"Unsupported operand {operand}")
                } % modulus;
            }

            public void TakeTurn(Monkey[] monkeys, ulong worryDivisor, ulong worryModulus)
            {
                foreach (ulong itemValue in heldItemValues)
                {
                    ulong newValue = operation(itemValue, worryModulus) / worryDivisor;

                    if (newValue % TestDivisor == 0)
                    {
                        monkeys[trueTarget].Catch(newValue);
                    }
                    else
                    {
                        monkeys[falseTarget].Catch(newValue);
                    }
                }

                ItemsInspected += (ulong)heldItemValues.Count;
                heldItemValues.Clear();
            }

            public void Catch(ulong itemValue)
            {
                heldItemValues.Add(itemValue);
            }
        }
    }
}