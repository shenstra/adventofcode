namespace Advent.Aoc2015
{
    public class Day09
    {
        private readonly IInput input;
        private readonly Regex legRegex = new(@"^(?<location1>.*) to (?<location2>.*) = (?<distance>\d+)$");

        public Day09(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var routes = EnumerateRoutes(GetDistances(input.GetLines()));
            Console.WriteLine(routes.Min(route => route.Distance));
        }

        public void Part2()
        {
            var routes = EnumerateRoutes(GetDistances(input.GetLines()));
            Console.WriteLine(routes.Max(route => route.Distance));
        }

        private static IEnumerable<Route> EnumerateRoutes(Dictionary<(string, string), int> distances)
        {
            var locations = distances.Select(kvp => kvp.Key.Item1).Distinct().ToList();
            foreach (string location in locations)
            {
                foreach (var route in EnumerateRoutesRecursively(new Route { Locations = new List<string> { location } }, distances, locations))
                {
                    yield return route;
                }
            }
        }

        private static IEnumerable<Route> EnumerateRoutesRecursively(Route route, Dictionary<(string, string), int> distances, List<string> locations)
        {
            foreach (string location in locations.Except(route.Locations))
            {
                var newRoute = new Route
                {
                    Locations = route.Locations.Append(location).ToList(),
                    Distance = route.Distance + distances[(route.Locations.Last(), location)]
                };

                if (newRoute.Locations.Count == locations.Count)
                {
                    yield return newRoute;
                }
                else
                {
                    foreach (var possibleRoute in EnumerateRoutesRecursively(newRoute, distances, locations))
                    {
                        yield return possibleRoute;
                    }
                }
            }
        }

        private Dictionary<(string, string), int> GetDistances(IEnumerable<string> lines)
        {
            var distances = new Dictionary<(string, string), int>();
            foreach (string line in lines)
            {
                var match = legRegex.Match(line);
                distances[(match.Groups["location1"].Value, match.Groups["location2"].Value)] = int.Parse(match.Groups["distance"].Value);
                distances[(match.Groups["location2"].Value, match.Groups["location1"].Value)] = int.Parse(match.Groups["distance"].Value);
            }

            return distances;
        }

        private class Route
        {
            public List<string> Locations { get; set; }
            public long Distance { get; set; }
        }
    }
}