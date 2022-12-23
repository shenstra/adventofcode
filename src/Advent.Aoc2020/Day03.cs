namespace Advent.Aoc2020
{
    public class Day03
    {
        private readonly IInput input;

        public Day03(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var map = input.GetLines().ToList();
            int trees = CountArborealProximityEvents(map, 3, 1);
            Console.WriteLine(trees);
        }

        public void Part2()
        {
            var map = input.GetLines().ToList();
            int result = CountArborealProximityEvents(map, 1, 1)
                * CountArborealProximityEvents(map, 3, 1)
                * CountArborealProximityEvents(map, 5, 1)
                * CountArborealProximityEvents(map, 7, 1)
                * CountArborealProximityEvents(map, 1, 2);
            Console.WriteLine(result);
        }

        private static int CountArborealProximityEvents(List<string> map, int slopeX, int slopeY)
        {
            int trees = 0;
            int width = map[0].Length;
            int xPos = 0;
            for (int yPos = 0; yPos < map.Count; yPos += slopeY)
            {
                if (map[yPos][xPos] == '#')
                {
                    trees++;
                }

                xPos = (xPos + slopeX) % width;
            }

            return trees;
        }
    }
}
