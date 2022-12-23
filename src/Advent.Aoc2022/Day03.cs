using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day03
    {
        private readonly IInput input;

        public Day03(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            var rucksacks = input.GetLines().Select(SplitCompartments);
            var priorities = rucksacks.Select(FindCommonItem).Select(GetPriority);
            return priorities.Sum();
        }

        public int Part2()
        {
            var groups = input.GetLines().Chunk(3);
            var priorities = groups.Select(FindCommonItem).Select(GetPriority);
            return priorities.Sum();
        }

        private static char FindCommonItem(IEnumerable<string> itemLists)
        {
            return itemLists.Aggregate(StringIntersect).Single();
        }

        private static string StringIntersect(string result, string current)
        {
            return string.Concat(result.Intersect(current));
        }

        private static int GetPriority(char common)
        {
            return char.IsLower(common) ? common - 'a' + 1 : common - 'A' + 27;
        }

        private static IEnumerable<string> SplitCompartments(string rucksack)
        {
            int halfway = rucksack.Length / 2;
            yield return rucksack[..halfway];
            yield return rucksack[halfway..];
        }
    }
}