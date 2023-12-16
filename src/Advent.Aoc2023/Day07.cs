using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day07(IInput input)
    {
        public int Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var hands = lines.Select(line => ParseHand(line, useJokers: false)).ToArray();
            return CalculateTotalWinnings(hands);
        }

        public int Part2()
        {
            string[] lines = input.GetLines().ToArray();
            var hands = lines.Select(line => ParseHand(line, useJokers: true)).ToArray();
            return CalculateTotalWinnings(hands);
        }

        private int CalculateTotalWinnings(Hand[] hands)
        {
            Array.Sort(hands);
            return hands.Select((hand, index) => hand.Bid * (index + 1)).Sum();
        }

        private Hand ParseHand(string input, bool useJokers)
        {
            string[] parts = input.Split(' ');
            var cards = parts[0].Select(character => GetCard(character, useJokers)).ToArray();
            return new Hand(cards, int.Parse(parts[1]));
        }

        private Card GetCard(char character, bool useJokers)
        {
            return character switch
            {
                'A' => Card.Ace,
                'K' => Card.King,
                'Q' => Card.Queen,
                'J' => useJokers ? Card.Joker : Card.Jack,
                'T' => Card.Ten,
                _ => (Card)(character - '0')
            };
        }

        private enum HandType
        {
            FiveOfAKind = 6,
            FourOfAKind = 5,
            FullHouse = 4,
            ThreeOfAKind = 3,
            TwoPair = 2,
            OnePair = 1,
            HighCard = 0
        }

        private enum Card
        {
            Ace = 14,
            King = 13,
            Queen = 12,
            Jack = 11,
            Ten = 10,
            Nine = 9,
            Eight = 8,
            Seven = 7,
            Six = 6,
            Five = 5,
            Four = 4,
            Three = 3,
            Two = 2,
            Joker = 1,
        }

        private class Hand(Card[] cards, int bid) : IComparable<Hand>
        {

            public Card[] Cards { get; set; } = cards;
            public int Bid { get; set; } = bid;
            public HandType HandType { get; } = DetermineHandType(cards);

            private static HandType DetermineHandType(Card[] cards)
            {
                var cardCounts = new Dictionary<Card, int>();
                foreach (var card in cards)
                {
                    if (card != Card.Joker)
                    {
                        cardCounts[card] = cardCounts.TryGetValue(card, out int value) ? value + 1 : 1;
                    }
                }

                if (cardCounts.Keys.Count == 0)
                {
                    cardCounts[Card.Joker] = 5;
                }
                else
                {
                    var mostCommonCard = cardCounts.OrderByDescending(kvp => kvp.Value).First().Key;
                    cardCounts[mostCommonCard] += cards.Count(c => c == Card.Joker);
                }

                if (cardCounts.Values.Any(count => count == 5))
                {
                    return HandType.FiveOfAKind;
                }
                else if (cardCounts.Values.Any(count => count == 4))
                {
                    return HandType.FourOfAKind;
                }
                else if (cardCounts.Values.Any(count => count == 3) && cardCounts.Values.Any(count => count == 2))
                {
                    return HandType.FullHouse;
                }
                else if (cardCounts.Values.Any(count => count == 3))
                {
                    return HandType.ThreeOfAKind;
                }
                else if (cardCounts.Values.Count(count => count == 2) == 2)
                {
                    return HandType.TwoPair;
                }
                else if (cardCounts.Values.Any(count => count == 2))
                {
                    return HandType.OnePair;
                }
                return HandType.HighCard;
            }

            public int CompareTo(Hand? other)
            {
                if (other is null)
                {
                    return 1;
                }

                if (HandType != other.HandType)
                {
                    return HandType.CompareTo(other.HandType);
                }

                for (int i = 0; i < Cards.Length; i++)
                {
                    if (Cards[i] != other.Cards[i])
                    {
                        return Cards[i].CompareTo(other.Cards[i]);
                    }
                }

                return 0;
            }

            public override string ToString()
            {
                return $"{HandType}: {string.Join(", ", Cards)}";
            }
        }
    }
}
