namespace Advent.Aoc2021
{
    public class Day21(IInput input)
    {
        private readonly Dictionary<(int, int, int, int), (long, long)> cachedSimulations = [];

        public void Part1()
        {
            (int position1, int position2) = ParsePositions(input.GetLines());
            Console.WriteLine(PlaySampleGame(position1, position2));
        }

        public void Part2()
        {
            (int position1, int position2) = ParsePositions(input.GetLines());
            (long wins1, long wins2) = SimulateQuantumGame(position1, position2);
            Console.WriteLine(Math.Max(wins1, wins2));
        }

        private int PlaySampleGame(int position1, int position2)
        {
            var die = new DeterministicDie();
            (int score1, int score2) = (0, 0);

            while (true)
            {
                position1 = Advance(position1, die.Rolls(3));
                score1 += position1;
                if (score1 >= 1000)
                {
                    return score2 * die.RollCount;
                }

                position2 = Advance(position2, die.Rolls(3));
                score2 += position2;
                if (score2 >= 1000)
                {
                    return score1 * die.RollCount;
                }
            }
        }

        private (long wins1, long wins2) SimulateQuantumGame(int position1, int position2, int score1 = 0, int score2 = 0)
        {
            if (!cachedSimulations.ContainsKey((position1, position2, score1, score2)))
            {
                (long wins1, long wins2) = (0, 0);
                foreach (int roll in QuantumDie.RollThreeTimes())
                {
                    int newPosition = Advance(position1, roll);
                    int newScore = score1 + newPosition;
                    if (newScore > 20)
                    {
                        wins1++;
                    }
                    else
                    {
                        (long subWins2, long subWins1) = SimulateQuantumGame(position2, newPosition, score2, newScore);
                        wins1 += subWins1;
                        wins2 += subWins2;
                    }
                }

                cachedSimulations[(position1, position2, score1, score2)] = (wins1, wins2);
            }

            return cachedSimulations[(position1, position2, score1, score2)];
        }

        private int Advance(int current, int steps)
        {
            current += steps;
            current %= 10;
            return current == 0 ? 10 : current;
        }

        private (int position1, int position2) ParsePositions(IEnumerable<string> input)
        {
            int[] positions = input.Select(s => int.Parse(s.Split().Last())).ToArray();
            return (positions[0], positions[1]);
        }

        private class DeterministicDie
        {
            private int current = 0;
            public int RollCount { get; private set; }

            public int Roll()
            {
                current = (current % 100) + 1;
                RollCount++;
                return current;
            }

            public int Rolls(int count)
            {
                return Enumerable.Range(0, count).Sum(i => Roll());
            }
        }

        private static class QuantumDie
        {
            public static IEnumerable<int> RollThreeTimes()
            {
                for (int i = 1; i <= 3; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        for (int k = 1; k <= 3; k++)
                        {
                            yield return i + j + k;
                        }
                    }
                }
            }
        }
    }
}