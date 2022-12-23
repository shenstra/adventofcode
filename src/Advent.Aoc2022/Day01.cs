using Advent.Util;

namespace Advent.Aoc2022
{
    public class Day01
    {
        private readonly IInput input;

        public Day01(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            var lines = input.GetLines().ToList();
            var inventories = ParseInventories(lines);
            return inventories.Max();
        }

        public int Part2()
        {
            var lines = input.GetLines().ToList();
            var inventories = ParseInventories(lines);
            var topThree = inventories.OrderByDescending(i => i).Take(3);
            return topThree.Sum();
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