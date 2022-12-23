namespace Advent.Aoc2021
{
    public class Day07
    {
        private readonly IInput input;

        public Day07(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            int[] crabPositions = input.GetSingleLine().SplitToInts();
            Console.WriteLine(FindLowestFuelCost(crabPositions, LinearFuelCost));
        }

        public void Part2()
        {
            int[] crabPositions = input.GetSingleLine().SplitToInts();
            Console.WriteLine(FindLowestFuelCost(crabPositions, TriangularFuelCost));

        }

        private static int FindLowestFuelCost(int[] crabPositions, Func<int, int, int> fuelCostFunction)
        {
            return Enumerable.Range(crabPositions.Min(), crabPositions.Max())
                .Min(target => crabPositions.Sum(position => fuelCostFunction(position, target)));
        }

        private static int LinearFuelCost(int a, int b) => Math.Abs(a - b);

        private static int TriangularFuelCost(int a, int b) => TriangularNumber(Math.Abs(a - b));

        private static int TriangularNumber(int n) => n * (n + 1) / 2;
    }
}