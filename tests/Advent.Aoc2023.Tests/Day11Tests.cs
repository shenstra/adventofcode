namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day11Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day11 = new Day11(input);

            long result = day11.Part1();

            Assert.AreEqual(374, result);
        }

        [TestMethod]
        public void Part2_1()
        {
            IInput input = new FixedInput(testInput);
            var day11 = new Day11(input);

            long result = day11.Part2(10);

            Assert.AreEqual(1030, result);
        }

        [TestMethod]
        public void Part2_2()
        {
            IInput input = new FixedInput(testInput);
            var day11 = new Day11(input);

            long result = day11.Part2(100);

            Assert.AreEqual(8410, result);
        }

        private readonly string[] testInput =
        [
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#.....",
        ];
    }
}
