using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day07
    {
        private readonly Dictionary<int, int> triangularNumbers = new() { { 0, 1 }, { 1, 1 } };

        public void Part1()
        {
            int[] crabPositions = Input.GetSingleLine(2021, 7).SplitToInts();
            Console.WriteLine(FindLowestFuelCost(crabPositions, LinearFuelCost));
        }

        public void Part2()
        {
            int[] crabPositions = Input.GetSingleLine(2021, 7).SplitToInts();
            Console.WriteLine(FindLowestFuelCost(crabPositions, TriangularFuelCost));

        }

        private static int FindLowestFuelCost(int[] crabPositions, Func<int, int, int> fuelCostFunction)
        {
            return Enumerable.Range(crabPositions.Min(), crabPositions.Max())
                .Min(target => crabPositions.Sum(position => fuelCostFunction(position, target)));
        }

        private int LinearFuelCost(int a, int b) => Math.Abs(a - b);

        private int TriangularFuelCost(int a, int b) => TriangularNumber(Math.Abs(a - b));

        private int TriangularNumber(int n)
        {
            if (!triangularNumbers.ContainsKey(n))
            {
                triangularNumbers[n] = TriangularNumber(n - 1) + n;
            }

            return triangularNumbers[n];
        }
    }
}