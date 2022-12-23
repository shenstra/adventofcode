namespace Advent.Aoc2015
{
    public class Day16
    {
        private readonly IInput input;
        private readonly Regex sueRegex = new(@"^Sue (\d+): (.+)$");

        public Day16(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var sues = GetSues(input.GetLines());
            var isMatchingSue = GetSueMatcher(GetAnalysis(), false);
            Console.WriteLine(sues.First(isMatchingSue).num);
        }

        public void Part2()
        {
            var sues = GetSues(input.GetLines());
            var isMatchingSue = GetSueMatcher(GetAnalysis(), true);
            Console.WriteLine(sues.First(isMatchingSue).num);
        }

        private Func<(int num, Dictionary<string, int> attrs), bool> GetSueMatcher(Dictionary<string, int> analysis, bool newVersion)
        {
            return (sue) =>
            {
                foreach (var attr in sue.attrs)
                {
                    if (newVersion && attr.Key is "cats" or "trees")
                    {
                        if (attr.Value <= analysis[attr.Key])
                        {
                            return false;
                        }
                    }
                    else if (newVersion && attr.Key is "pomeranians" or "goldfish")
                    {
                        if (attr.Value >= analysis[attr.Key])
                        {
                            return false;
                        }
                    }
                    else if (attr.Value != analysis[attr.Key])
                    {
                        return false;
                    }
                }

                return true;
            };
        }

        private static Dictionary<string, int> GetAnalysis()
        {
            return new Dictionary<string, int>
            {
                { "children", 3 },
                { "cats", 7 },
                { "samoyeds", 2 },
                { "pomeranians", 3 },
                { "akitas", 0 },
                { "vizslas", 0 },
                { "goldfish", 5 },
                { "trees", 3 },
                { "cars", 2 },
                { "perfumes", 1 }
            };
        }

        private IEnumerable<(int num, Dictionary<string, int> attrs)> GetSues(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                var match = sueRegex.Match(line);
                int sue = int.Parse(match.Groups[1].Value);
                var attrs = match.Groups[2].Value.Split(", ").Select(s => s.Split(": ")).ToDictionary(a => a[0], a => int.Parse(a[1]));
                yield return (sue, attrs);
            }
        }
    }
}
