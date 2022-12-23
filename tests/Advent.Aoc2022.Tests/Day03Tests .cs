namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day03Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day03(input);

            int result = sut.Part1();

            Assert.AreEqual(157, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day03(input);

            int result = sut.Part2();

            Assert.AreEqual(70, result);
        }

        private readonly string[] testInput = new string[]
        {
            "vJrwpWtwJgWrhcsFMMfFFhFp",
            "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg",
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
            "ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw",
        };
    }
}