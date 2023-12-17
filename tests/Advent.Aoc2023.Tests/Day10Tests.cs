namespace Advent.Aoc2023.Tests
{
    [TestClass]
    public class Day10Tests
    {
        [TestMethod]
        public void Part1_1()
        {
            IInput input = new FixedInput(testInput1);
            var day10 = new Day10(input);

            int result = day10.Part1();

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Part1_2()
        {
            IInput input = new FixedInput(testInput2);
            var day10 = new Day10(input);

            int result = day10.Part1();

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Part1_3()
        {
            IInput input = new FixedInput(testInput3);
            var day10 = new Day10(input);

            int result = day10.Part1();

            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void Part2_1()
        {
            IInput input = new FixedInput(testInput4);
            var day10 = new Day10(input);

            int result = day10.Part2();

            Assert.AreEqual(4, result);
        }

        private readonly string[] testInput1 =
        [
            ".....",
            ".S-7.",
            ".|.|.",
            ".L-J.",
            ".....",
        ];

        private readonly string[] testInput2 =
        [
            "-L|F7",
            "7S-7|",
            "L|7||",
            "-L-J|",
            "L|-JF",
        ];

        private readonly string[] testInput3 =
        [
            "7-F7-",
            ".FJ|7",
            "SJLL7",
            "|F--J",
            "LJ.LJ",
        ];

        private readonly string[] testInput4 =
        [
            "...........",
            ".S-------7.",
            ".|F-----7|.",
            ".||.....||.",
            ".||.....||.",
            ".|L-7.F-J|.",
            ".|..|.|..|.",
            ".L--J.L--J.",
            "...........",
        ];
    }
}
