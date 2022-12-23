namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day04Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day04(input);

            int result = sut.Part1();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day04(input);

            int result = sut.Part2();

            Assert.AreEqual(4, result);
        }

        private readonly string[] testInput = new string[]
        {
            "2-4,6-8",
            "2-3,4-5",
            "5-7,7-9",
            "2-8,3-7",
            "6-6,4-6",
            "2-6,4-8",
        };
    }
}