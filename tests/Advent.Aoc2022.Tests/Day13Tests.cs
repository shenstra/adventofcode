namespace Advent.Aoc2022.Tests
{
    [TestClass]
    public class Day13Tests
    {
        [TestMethod]
        [DataRow("[1]", "[2]", true)]
        [DataRow("[2]", "[1]", false)]
        [DataRow("[2]", "[11]", true)]
        [DataRow("[1,1,1]", "[1,1,2]", true)]
        [DataRow("[1,1,1]", "[1,2,1]", true)]
        [DataRow("[1,[1],1]", "[1,2,1]", true)]
        [DataRow("[1,1,1]", "[1,[[2]],1]", true)]
        [DataRow("[1,1]", "[1,1,1]", true)]
        [DataRow("[1]", "[[1,1]]", true)]
        [DataRow("[]", "[1]", true)]
        [DataRow("[1]", "[]", false)]
        public void AreInOrder(string left, string right, bool expectedResult)
        {
            var sut = new Day13(new FixedInput());

            bool result = sut.AreInOrder(left, right);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Part01()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day13(input);

            int result = sut.Part1();

            Assert.AreEqual(13, result);
        }

        [TestMethod]
        public void Part02()
        {
            IInput input = new FixedInput(testInput);
            var sut = new Day13(input);

            int result = sut.Part2();

            Assert.AreEqual(140, result);
        }

        private readonly string[] testInput = new string[]
        {
            "[1,1,3,1,1]",
            "[1,1,5,1,1]",
            "",
            "[[1],[2,3,4]]",
            "[[1],4]",
            "",
            "[9]",
            "[[8,7,6]]",
            "",
            "[[4,4],4,4]",
            "[[4,4],4,4,4]",
            "",
            "[7,7,7,7]",
            "[7,7,7]",
            "",
            "[]",
            "[3]",
            "",
            "[[[]]]",
            "[[]]",
            "",
            "[1,[2,[3,[4,[5,6,7]]]],8,9]",
            "[1,[2,[3,[4,[5,6,0]]]],8,9]",
        };
    }
}