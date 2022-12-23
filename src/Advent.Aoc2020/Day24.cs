namespace Advent.Aoc2020
{
    public class Day24
    {
        private readonly IInput input;
        private readonly Regex instructionRegex = new(@"(ne|nw|se|sw|e|w)");

        public Day24(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var tilesToFlip = input.GetLines().Select(FindTile);
            var blackTiles = GetBlackTiles(tilesToFlip);
            Console.WriteLine(blackTiles.Count);
        }

        public void Part2()
        {
            var tilesToFlip = input.GetLines().Select(FindTile);
            var blackTiles = GetBlackTiles(tilesToFlip);
            for (int i = 0; i < 100; i++)
            {
                blackTiles = ApplyRound(blackTiles);
            }
            Console.WriteLine(blackTiles.Count);
        }

        private (int x, int y) FindTile(string input)
        {
            (int x, int y) = (0, 0);
            foreach (object match in instructionRegex.Matches(input))
            {
                bool evenRow = y % 2 == 0;
                (x, y) = match.ToString() switch
                {
                    "ne" => (evenRow ? x + 1 : x, y - 1),
                    "e" => (x + 1, y),
                    "se" => (evenRow ? x + 1 : x, y + 1),
                    "sw" => (evenRow ? x : x - 1, y + 1),
                    "w" => (x - 1, y),
                    "nw" => (evenRow ? x : x - 1, y - 1),
                    _ => throw new ApplicationException($"Invalid instruction {match}"),
                };
            }

            return (x, y);
        }

        private static List<(int x, int y)> GetBlackTiles(IEnumerable<(int x, int y)> tileFlips)
        {
            return tileFlips.GroupBy(tile => tile).Where(group => group.Count() % 2 == 1).Select(group => group.Key).ToList();
        }

        private List<(int x, int y)> ApplyRound(List<(int x, int y)> blackTiles)
        {
            var stayBlack = blackTiles.Where(tile => blackTiles.Intersect(GetAdjacentTiles(tile)).Count() is 1 or 2);
            var becomeBlack = blackTiles.SelectMany(GetAdjacentTiles).GroupBy(tile => tile)
                .Where(group => group.Count() == 2).Select(group => group.Key).Except(blackTiles);
            return stayBlack.Concat(becomeBlack).ToList();
        }

        private IEnumerable<(int x, int y)> GetAdjacentTiles((int, int) tile)
        {
            (int x, int y) = tile;
            bool evenRow = y % 2 == 0;
            yield return (evenRow ? x + 1 : x, y - 1); // ne
            yield return (x + 1, y); // e
            yield return (evenRow ? x + 1 : x, y + 1); // se
            yield return (evenRow ? x : x - 1, y + 1); // sw
            yield return (x - 1, y); // w
            yield return (evenRow ? x : x - 1, y - 1); // nw
        }
    }
}