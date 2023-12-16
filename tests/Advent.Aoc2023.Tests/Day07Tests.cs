namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day07Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day07 = new Day07(input);

            int result = day07.Part1();

            Assert.AreEqual(6440, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day07 = new Day07(input);

            int result = day07.Part2();

            Assert.AreEqual(5905, result);
        }

        private readonly string[] testInput =
        [
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483",
        ];
    }
}
