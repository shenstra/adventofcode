namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day09Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day09(input);

            int result = sut.Part1();

            Assert.AreEqual(13, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day09(input);

            int result = sut.Part2();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Part02_Big()
        {
            IInput input = new FixedInput(bigTestInput);
            var sut = new Day09(input);

            int result = sut.Part2();

            Assert.AreEqual(36, result);
        }

        private readonly string[] testInput =
        [
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2",
        ];

        private readonly string[] bigTestInput =
        [
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20",
        ];
    }
}