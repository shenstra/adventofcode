namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day08Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day08(input);

            int result = sut.Part1();

            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day08(input);

            int result = sut.Part2();

            Assert.AreEqual(8, result);
        }

        private readonly string[] testInput = new string[]
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390",
        };
    }
}