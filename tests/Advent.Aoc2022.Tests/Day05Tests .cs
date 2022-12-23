namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day05Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day05(input);

            string result = sut.Part1();

            Assert.AreEqual("CMZ", result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day05(input);

            string result = sut.Part2();

            Assert.AreEqual("MCD", result);
        }

        private readonly string[] testInput = new string[]
        {
            "    [D]    ",
            "[N] [C]    ",
            "[Z] [M] [P]",
            " 1   2   3 ",
            "",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2",
        };
    }
}