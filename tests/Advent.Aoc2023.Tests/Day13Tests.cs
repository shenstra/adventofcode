namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day13Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day13 = new Day13(input);

            int result = day13.Part1();

            Assert.AreEqual(405, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day13 = new Day13(input);

            int result = day13.Part2();

            Assert.AreEqual(400, result);
        }

        private readonly string[] testInput =
        [
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#.",
            "",
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#",
        ];
    }
}
