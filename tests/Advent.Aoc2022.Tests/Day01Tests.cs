namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day01Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day01(input);

            int result = sut.Part1();

            Assert.AreEqual(24000, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day01(input);

            int result = sut.Part2();

            Assert.AreEqual(45000, result);
        }

        private readonly string[] testInput =
        [
            "1000",
            "2000",
            "3000",
            "",
            "4000",
            "",
            "5000",
            "6000",
            "",
            "7000",
            "8000",
            "9000",
            "",
            "10000",
        ];
    }
}