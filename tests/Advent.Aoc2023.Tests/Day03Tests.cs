namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day03Tests
    {
        [TestMethod]
        public void Part1_Test()
        {
            IInput input = new FixedInput(testInput);
            var day03 = new Day03(input);

            int result = day03.Part1();

            Assert.AreEqual(4361, result);
        }

        [TestMethod]
        public void Part2_Test()
        {
            IInput input = new FixedInput(testInput);
            var day03 = new Day03(input);

            int result = day03.Part2();

            Assert.AreEqual(467835, result);
        }

        private readonly string[] testInput =
        [
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
        ];
    }
}
