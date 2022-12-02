using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day22
    {
        private readonly Dictionary<string, int> knownGameWinners = new();

        public void Part1()
        {
            var (player1, player2) = GetHands(Input.GetLines(2020, 22));
            PlayCombat(player1, player2);
            Console.WriteLine(CalculateScore(player1.Any() ? player1 : player2));
        }

        public void Part2()
        {
            var (player1, player2) = GetHands(Input.GetLines(2020, 22));
            PlayRecursiveCombat(player1, player2);
            Console.WriteLine(CalculateScore(player1.Any() ? player1 : player2));
        }

        private static void PlayCombat(Queue<int> player1, Queue<int> player2)
        {
            while (player1.Any() && player2.Any())
            {
                var (card1, card2) = (player1.Dequeue(), player2.Dequeue());
                if (card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested", Justification = "Nested ternary operator is the cleanest solution in this case")]
        private int PlayRecursiveCombat(Queue<int> player1, Queue<int> player2)
        {
            string gameId = string.Join(",", player1) + "~" + string.Join(",", player2);
            if (knownGameWinners.ContainsKey(gameId))
            {
                return knownGameWinners[gameId];
            }

            var playedRounds = new List<string>();
            while (player1.Any() && player2.Any())
            {
                string roundId = string.Join(",", player1) + "~" + string.Join(",", player2);
                if (playedRounds.Contains(roundId))
                {
                    knownGameWinners[gameId] = 1;
                    return 1;
                }

                var (card1, card2) = (player1.Dequeue(), player2.Dequeue());
                int roundWinner = card1 <= player1.Count && card2 <= player2.Count
                    ? PlayRecursiveCombat(new Queue<int>(player1.Take(card1)), new Queue<int>(player2.Take(card2)))
                    : card1 > card2 ? 1 : 2;
                if (roundWinner == 1)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }

                playedRounds.Add(roundId);
            }

            knownGameWinners[gameId] = player1.Any() ? 1 : 2;
            return knownGameWinners[gameId];
        }

        private static int CalculateScore(Queue<int> winningHand)
        {
            int score = 0;
            int multiplier = winningHand.Count;
            while (winningHand.Any())
            {
                score += winningHand.Dequeue() * multiplier--;
            }

            return score;
        }

        private static (Queue<int>, Queue<int>) GetHands(IEnumerable<string> lines)
        {
            var player1 = new Queue<int>(lines.Skip(1).TakeWhile(s => s != string.Empty).Select(s => int.Parse(s)));
            var player2 = new Queue<int>(lines.Skip(player1.Count + 3).Select(s => int.Parse(s)));
            return (player1, player2);
        }
    }
}