namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day01Tests
    {
        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput1);
            var sut = new Day01(input);

            int result = sut.Part1();

            Assert.AreEqual(142, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput2);
            var sut = new Day01(input);

            int result = sut.Part2();

            Assert.AreEqual(281, result);
        }

        private readonly string[] testInput1 =
        [
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet",
        ];

        private readonly string[] testInput2 =
        [
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen",
        ];
    }
}