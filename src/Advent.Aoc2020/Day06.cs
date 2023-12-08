namespace Advent.Aoc2020
{
    public class Day06(IInput input)
    {
        public void Part1()
        {
            var groups = GetGroups(input.GetLines());
            int counts = groups.Select(CountAnswersPresent).Sum();
            Console.WriteLine(counts);
        }

        public void Part2()
        {
            var groups = GetGroups(input.GetLines());
            int counts = groups.Select(CountUnanimousAnswers).Sum();
            Console.WriteLine(counts);
        }

        private int CountUnanimousAnswers(List<string> group)
        {
            return group.Select(s => s.AsEnumerable()).Aggregate(Enumerable.Intersect).Count();
        }

        private int CountAnswersPresent(List<string> group)
        {
            return group.SelectMany(s => s.ToList()).Distinct().Count();
        }

        private static IEnumerable<List<string>> GetGroups(IEnumerable<string> lines)
        {
            List<string> group = [];
            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    yield return group;
                    group = [];
                }
                else
                {
                    group.Add(line);
                }
            }

            yield return group;
        }
    }
}
