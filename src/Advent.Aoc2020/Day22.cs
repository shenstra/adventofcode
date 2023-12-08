namespace Advent.Aoc2020
{
    public class Day22(IInput input)
    {
        private readonly Dictionary<string, int> knownGameWinners = [];

        public void Part1()
        {
            var (player1, player2) = GetHands(input.GetLines());
            PlayCombat(player1, player2);
            Console.WriteLine(CalculateScore(player1.Count != 0 ? player1 : player2));
        }

        public void Part2()
        {
            var (player1, player2) = GetHands(input.GetLines());
            PlayRecursiveCombat(player1, player2);
            Console.WriteLine(CalculateScore(player1.Count != 0 ? player1 : player2));
        }

        private static void PlayCombat(Queue<int> player1, Queue<int> player2)
        {
            while (player1.Count != 0 && player2.Count != 0)
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

        private int PlayRecursiveCombat(Queue<int> player1, Queue<int> player2)
        {
            string gameId = string.Join(",", player1) + "~" + string.Join(",", player2);
            if (knownGameWinners.TryGetValue(gameId, out int value))
            {
                return value;
            }

            var playedRounds = new List<string>();
            while (player1.Count != 0 && player2.Count != 0)
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

            knownGameWinners[gameId] = player1.Count != 0 ? 1 : 2;
            return knownGameWinners[gameId];
        }

        private static int CalculateScore(Queue<int> winningHand)
        {
            int score = 0;
            int multiplier = winningHand.Count;
            while (winningHand.Count != 0)
            {
                score += winningHand.Dequeue() * multiplier--;
            }

            return score;
        }

        private static (Queue<int>, Queue<int>) GetHands(IEnumerable<string> lines)
        {
            var player1 = new Queue<int>(lines.Skip(1).TakeWhile(s => s != string.Empty).Select(int.Parse));
            var player2 = new Queue<int>(lines.Skip(player1.Count + 3).Select(int.Parse));
            return (player1, player2);
        }
    }
}