namespace Advent.Aoc2021
{
    public class Day04
    {
        private readonly IInput input;

        public Day04(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var lines = input.GetLines().ToList();
            var winner = new BingoGame(lines).Play();
            Console.WriteLine(winner.Score);
        }

        public void Part2()
        {
            var lines = input.GetLines().ToList();
            var loser = new BingoGame(lines).PlayToTheEnd();
            Console.WriteLine(loser.Score);
        }

        private class BingoGame
        {
            private readonly int[] randomNumbers;
            private readonly List<BingoBoard> boards;

            public BingoGame(List<string> input)
            {
                randomNumbers = input.First().SplitToInts();
                boards = input.Skip(2).Chunk(6).Select(l => new BingoBoard(l.ToList())).ToList();
            }

            public BingoBoard Play()
            {
                foreach (int number in randomNumbers)
                {
                    boards.ForEach(b => b.MarkNumber(number));
                    var winners = boards.Where(b => b.HasWon);
                    if (winners.Count() == 1)
                    {
                        return winners.Single();
                    }
                }
                throw new ApplicationException("No single winner");
            }

            public BingoBoard PlayToTheEnd()
            {
                BingoBoard? loser = null;
                foreach (int number in randomNumbers)
                {
                    boards.ForEach(b => b.MarkNumber(number));
                    var losers = boards.Where(b => !b.HasWon);
                    if (losers.Count() == 1)
                    {
                        loser = losers.Single();
                    }
                    else if (!losers.Any())
                    {
                        return loser!;
                    }
                }
                throw new ApplicationException("No single loser");
            }
        }

        private class BingoBoard
        {
            private readonly Regex whitespaceRegex = new(@"\s+");
            private readonly int[,] numbers = new int[5, 5];
            private readonly int[] rowMarks = new int[5];
            private readonly int[] colMarks = new int[5];
            private int runningScore = 0;
            private int lastCalled = 0;

            public bool HasWon => rowMarks.Max() == 5 || colMarks.Max() == 5;
            public int Score => runningScore * lastCalled;

            public BingoBoard(List<string> input)
            {
                for (int row = 0; row < 5; row++)
                {
                    string[] rowParts = whitespaceRegex.Split(input[row].Trim()).ToArray();
                    for (int col = 0; col < 5; col++)
                    {
                        numbers[row, col] = int.Parse(rowParts[col]);
                        runningScore += numbers[row, col];
                    }
                }
            }

            public void MarkNumber(int number)
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (numbers[row, col] == number)
                        {
                            rowMarks[row]++;
                            colMarks[col]++;
                            runningScore -= number;
                        }
                    }
                }
                lastCalled = number;
            }
        }
    }
}