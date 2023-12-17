namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day08Tests
    {
        [TestMethod]
        public void Part1_1()
        {
            IInput input = new FixedInput(testInput1);
            var day08 = new Day08(input);

            long result = day08.Part1();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Part1_2()
        {
            IInput input = new FixedInput(testInput2);
            var day08 = new Day08(input);

            long result = day08.Part1();

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Part2()
        {
            IInput input = new FixedInput(testInput3);
            var day08 = new Day08(input);

            long result = day08.Part2();

            Assert.AreEqual(6, result);
        }

        private readonly string[] testInput1 =
        [
            "RL",
            "",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)",
        ];

        private readonly string[] testInput2 =
        [
            "LLR",
            "",
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)",
        ];

        private readonly string[] testInput3 =
        [
            "LR",
            "",
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)",
        ];
    }
}
