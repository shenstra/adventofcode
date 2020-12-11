using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    class Day3
    {
        public void Problem1()
        {
            var map = Input.GetLines(2020, 3).ToList();
            int trees = CountArborealProximityEvents(map, 3, 1);
            Console.WriteLine(trees);
        }

        public void Problem2()
        {
            var map = Input.GetLines(2020, 3).ToList();
            var result = CountArborealProximityEvents(map, 1, 1)
                * CountArborealProximityEvents(map, 3, 1)
                * CountArborealProximityEvents(map, 5, 1)
                * CountArborealProximityEvents(map, 7, 1)
                * CountArborealProximityEvents(map, 1, 2);
            Console.WriteLine(result);
        }

        private static int CountArborealProximityEvents(List<string> map, int slopeX, int slopeY)
        {
            int trees = 0;
            var width = map[0].Length;
            int xPos = 0;
            for (int yPos = 0; yPos < map.Count; yPos += slopeY)
            {
                if (map[yPos][xPos] == '#')
                    trees++;
                xPos = (xPos + slopeX) % width;
            }

            return trees;
        }
    }
}
