using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day02
    {
        private readonly IInput input;

        public Day02(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            var rounds = input.GetLines().Select(MapRound).ToList();
            return rounds.Sum(r => ScoreRound1(r));
        }

        public int Part2()
        {
            var rounds = input.GetLines().Select(MapRound).ToList();
            return rounds.Sum(r => ScoreRound2(r));
        }

        private int ScoreRound1((char opponent, char player) round)
        {
            return round switch
            {
                ('A', 'X') => 4,
                ('A', 'Y') => 8,
                ('A', 'Z') => 3,
                ('B', 'X') => 1,
                ('B', 'Y') => 5,
                ('B', 'Z') => 9,
                ('C', 'X') => 7,
                ('C', 'Y') => 2,
                ('C', 'Z') => 6,
                _ => throw new ApplicationException($"Impossible hand {round}"),
            };
        }

        private int ScoreRound2((char opponent, char player) round)
        {
            return round switch
            {
                ('A', 'X') => 3,
                ('A', 'Y') => 4,
                ('A', 'Z') => 8,
                ('B', 'X') => 1,
                ('B', 'Y') => 5,
                ('B', 'Z') => 9,
                ('C', 'X') => 2,
                ('C', 'Y') => 6,
                ('C', 'Z') => 7,
                _ => throw new ApplicationException($"Impossible hand {round}"),
            };
        }

        private (char opponent, char player) MapRound(string input)
        {
            return (input[0], input[2]);
        }
    }
}