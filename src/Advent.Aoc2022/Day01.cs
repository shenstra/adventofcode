using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day01
    {
        public void Part1()
        {
            var lines = Input.GetLines(2022, 1).ToList();
            var inventories = ParseInventories(lines);
            Console.WriteLine(inventories.Max());
        }

        public void Part2()
        {
            var lines = Input.GetLines(2022, 1).ToList();
            var inventories = ParseInventories(lines);
            var topThree = inventories.OrderByDescending(i => i).Take(3);
            Console.WriteLine(topThree.Sum());
        }

        private IEnumerable<int> ParseInventories(List<string> lines)
        {
            int inventory = 0;
            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    yield return inventory;
                    inventory = 0;
                }
                else
                {
                    inventory += int.Parse(line);
                }
            }

            yield return inventory;
        }
    }
}