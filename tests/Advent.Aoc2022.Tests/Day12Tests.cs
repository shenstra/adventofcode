namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day12Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day12(input);

            int result = sut.Part1();

            Assert.AreEqual(31, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day12(input);

            int result = sut.Part2();

            Assert.AreEqual(29, result);
        }

        private readonly string[] testInput =
        [
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        ];
    }
}