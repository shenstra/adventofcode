namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day09Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day09 = new Day09(input);

            long result = day09.Part1();

            Assert.AreEqual(114, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day09 = new Day09(input);

            long result = day09.Part2();

            Assert.AreEqual(2, result);
        }

        private readonly string[] testInput =
        [
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45",
        ];
    }
}
