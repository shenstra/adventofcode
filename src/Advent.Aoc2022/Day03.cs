using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day03
    {
        public void Part1()
        {
            var rucksacks = Input.GetLines(2022, 3).Select(SplitCompartments);
            var priorities = rucksacks.Select(FindCommonItem).Select(GetPriority);
            Console.WriteLine(priorities.Sum());
        }

        public void Part2()
        {
            var groups = Input.GetLines(2022, 3).Chunk(3);
            var priorities = groups.Select(FindCommonItem).Select(GetPriority);
            Console.WriteLine(priorities.Sum());
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