namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day12Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day12 = new Day12(input);

            long result = day12.Part1();

            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day12 = new Day12(input);

            long result = day12.Part2();

            Assert.AreEqual(525152, result);
        }

        private readonly string[] testInput =
        [
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1",
        ];
    }
}
