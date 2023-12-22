namespace Advent.Aoc2023
{
    public class Day02(IInput input)
    {
        public int Part1()
        {
            Dictionary<string, int> availableCubes = new()
            {
                ["red"] = 12,
                ["green"] = 13,
                ["blue"] = 14,
            };

            var games = input.GetLines().Select(ParseGame).ToArray();
            var possibleGames = games.Where(game => game.IsPossible(availableCubes));
            return possibleGames.Sum(game => game.Id);
        }

        public int Part2()
        {
            var games = input.GetLines().Select(ParseGame).ToArray();
            return games.Sum(game => game.GetPower());
        }

        private Game ParseGame(string input)
        {
            int colonIndex = input.IndexOf(':');
            int id = int.Parse(input[5..colonIndex]);
            string[] roundStrings = input[(colonIndex + 2)..].Split("; ");
            var rounds = roundStrings.Select(ParseRound).ToArray();
            return new Game(id, rounds);

        }

        private Round ParseRound(string input)
        {
            var cubes = input.Split(", ").Select(ParseCube);
            var cubeDictionary = cubes.ToDictionary(cube => cube.color, cube => cube.count);
            return new Round(cubeDictionary);
        }

        private (string color, int count) ParseCube(string input)
        {
            string[] parts = input.Split(" ");
            return (parts[1], int.Parse(parts[0]));
        }

        private record Game(int Id, Round[] Rounds)
        {
            public bool IsPossible(Dictionary<string, int> availableCubes)
            {
                return Rounds.All(round => round.IsPossible(availableCubes));
            }

            public int GetPower()
            {
                Dictionary<string, int> minimumRequired = [];

                foreach (var round in Rounds)
                {
                    foreach (var cube in round.Cubes)
                    {
                        minimumRequired[cube.Key] = minimumRequired.TryGetValue(cube.Key, out int value)
                            ? Math.Max(value, cube.Value) : cube.Value;
                    }
                }

                return minimumRequired.Values.Aggregate((a, b) => a * b);
            }
        }

        private record Round(Dictionary<string, int> Cubes)
        {
            public bool IsPossible(Dictionary<string, int> availableCubes)
            {
                return Cubes.All(cube => availableCubes[cube.Key] >= cube.Value);
            }
        }
    }
}
