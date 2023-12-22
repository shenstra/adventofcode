namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day15Tests
    {
        [TestMethod]
        public void Part1()
        {
            IInput input = new FixedInput(testInput);
            var day15 = new Day15(input);

            int result = day15.Part1();

            Assert.AreEqual(1320, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput);
            var day15 = new Day15(input);

            int result = day15.Part2();

            Assert.AreEqual(145, result);
        }

        private readonly string testInput = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
    }
}
