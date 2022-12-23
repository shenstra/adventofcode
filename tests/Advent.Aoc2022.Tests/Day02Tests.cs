namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day02Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day02(input);

            int result = sut.Part1();

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day02(input);

            int result = sut.Part2();

            Assert.AreEqual(12, result);
        }

        private readonly string[] testInput = new string[]
        {
            "A Y",
            "B X",
            "C Z",
        };
    }
}