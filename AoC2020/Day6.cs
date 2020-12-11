using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    class Day6
    {
        public void Problem1()
        {
            var groups = GetGroups(Input.GetLines(2020, 6));
            var counts = groups.Select(CountAnswersPresent).Sum();
            Console.WriteLine(counts);
        }

        public void Problem2()
        {
            var groups = GetGroups(Input.GetLines(2020, 6));
            var counts = groups.Select(CountUnanimousAnswers).Sum();
            Console.WriteLine(counts);
        }

        private int CountUnanimousAnswers(List<string> group)
        {
            return group.Select(s => s.ToList()).Aggregate((a, b) => a.Intersect(b).ToList()).Count();
        }

        private int CountAnswersPresent(List<string> group)
        {
            return group.SelectMany(s => s.ToList()).Distinct().Count();
        }

        private IEnumerable<List<string>> GetGroups(IEnumerable<string> lines)
        {
            List<string> group = new();
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    yield return group;
                    group = new List<string>();
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
