namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day06Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day06 = new Day06(input);

            int result = day06.Part1();

            Assert.AreEqual(288, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day06 = new Day06(input);

            int result = day06.Part2();

            Assert.AreEqual(71503, result);
        }

        private readonly string[] testInput =
        [
            "Time:      7  15   30",
            "Distance:  9  40  200"
        ];
    }
}
