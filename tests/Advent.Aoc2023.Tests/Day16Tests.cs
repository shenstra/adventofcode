namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day16Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day16 = new Day16(input);

            int result = day16.Part1();

            Assert.AreEqual(46, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day16 = new Day16(input);

            int result = day16.Part2();

            Assert.AreEqual(51, result);
        }

        private readonly string[] testInput =
        [
            @".|...\....",
            @"|.-.\.....",
            @".....|-...",
            @"........|.",
            @"..........",
            @".........\",
            @"..../.\\..",
            @".-.-/..|..",
            @".|....-|.\",
            @"..//.|...."
        ];
    }
}
