namespace Advent.Aoc2021
{
    public class Day12(IInput input)
    {
        public void Part1()
        {
            var connections = input.GetLines().SelectMany(ParseMap).ToList();
            int pathCount = CountPossiblePaths(connections, "start", visited: []);
            Console.WriteLine(pathCount);
        }

        public void Part2()
        {
            var connections = input.GetLines().SelectMany(ParseMap).ToList();
            int pathCount = CountPossiblePaths(connections, "start", visited: [], extraTime: true);
            Console.WriteLine(pathCount);
        }

        private int CountPossiblePaths(List<(string from, string to)> connections, string current, string[] visited, bool extraTime = false)
        {
            if (current == "end")
            {
                return 1;
            }

            visited = [.. visited, current];
            var candidates = connections.Where(c => c.from == current)
                .Where(c => char.IsUpper(c.to[0]) || !visited.Contains(c.to) || (extraTime && c.to is not "start" or "end"));

            int paths = 0;
            foreach ((string from, string to) in candidates)
            {
                bool extraTimeAfter = extraTime && !(char.IsLower(to[0]) && visited.Contains(to));
                paths += CountPossiblePaths(connections, to, visited, extraTimeAfter);
            }

            return paths;
        }

        private IEnumerable<(string from, string to)> ParseMap(string arg)
        {
            string[] parts = arg.Split('-');
            yield return (parts[0], parts[1]);
            yield return (parts[1], parts[0]);
        }
    }
}