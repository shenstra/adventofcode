namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day14Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day14(input);

            int result = sut.Part1();

            Assert.AreEqual(24, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day14(input);

            int result = sut.Part2();

            Assert.AreEqual(93, result);
        }

        private readonly string[] testInput =
        [
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9",
        ];
    }
}