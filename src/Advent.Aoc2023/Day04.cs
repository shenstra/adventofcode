using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day04(IInput input)
    {
        public int Part1()
        {
            var cards = input.GetLines().Select(s => new Card(s)).ToArray();
            return cards.Sum(c => c.Value);
        }

        public int Part2()
        {
            var cards = input.GetLines().Select(s => new Card(s)).ToArray();
            WinCopies(cards);
            return cards.Sum(c => c.Copies);
        }

        private void WinCopies(Card[] cards)
        {
            foreach (var card in cards)
            {
                for (int i = 0; i < card.MatchingNumbers; i++)
                {
                    if (card.Id + i >= cards.Length)
                    {
                        break;
                    }
                    cards[card.Id + i].Copies += card.Copies;
                }
            }
        }

        private class Card
        {
            public Card(string input)
            {
                string[] parts = input.Split(": ");
                Id = int.Parse(parts[0][5..]);
                string[] numberParts = parts[1].Split(" | ");
                WinningNumbers = ParseNumbers(numberParts[0]);
                Numbers = ParseNumbers(numberParts[1]);
            }

            private int[] ParseNumbers(string input)
            {
                return CommonRegex.Whitespace().Split(input.Trim()).Select(int.Parse).ToArray();
            }

            public int Id { get; set; }
            public int[] WinningNumbers { get; set; }
            public int[] Numbers { get; set; }
            public int Copies { get; set; } = 1;
            public int MatchingNumbers => Numbers.Count(n => WinningNumbers.Contains(n));
            public int Value => (1 << MatchingNumbers) / 2;
        }
    }
}
