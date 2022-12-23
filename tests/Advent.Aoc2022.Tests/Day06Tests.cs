namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day06Tests
    {
        [TestMethod]
        [DataRow("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
        [DataRow("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [DataRow("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [DataRow("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [DataRow("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public void Part01(string inputValue, int expectedValue)
        {
            IInput input = new FixedInput(inputValue);
            var sut = new Day06(input);

            int result = sut.Part1();

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [DataRow("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [DataRow("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [DataRow("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [DataRow("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [DataRow("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
        public void Part02(string inputValue, int expectedValue)
        {
            IInput input = new FixedInput(inputValue);
            var sut = new Day06(input);

            int result = sut.Part2();

            Assert.AreEqual(expectedValue, result);
        }
    }
}