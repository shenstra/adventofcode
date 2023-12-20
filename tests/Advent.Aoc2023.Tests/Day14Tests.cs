namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day14Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day14 = new Day14(input);

            int result = day14.Part1();

            Assert.AreEqual(136, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day14 = new Day14(input);

            int result = day14.Part2();

            Assert.AreEqual(64, result);
        }

        private readonly string[] testInput =
        [
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#....",
        ];
    }
}
