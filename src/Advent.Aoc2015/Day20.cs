namespace Advent.Aoc2015
{
    public class Day20
    {
        private readonly IInput input;

        public Day20(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            int target = input.GetSingleInt();
            int[] presents = VisitHouses(target / 10, lazy: false);
            Console.WriteLine(FindHouseWithPresents(target, presents));
        }

        public void Part2()
        {
            int target = input.GetSingleInt();
            int[] presents = VisitHouses(target / 11, lazy: true);
            Console.WriteLine(FindHouseWithPresents(target, presents));
        }

        private static int[] VisitHouses(int houses, bool lazy)
        {
            int[] presents = new int[houses + 1];
            for (int elf = 1; elf < houses; elf++)
            {
                int maxVisits = lazy ? 50 : houses;
                for (int visit = 1; visit <= houses / elf && visit <= maxVisits; visit++)
                {
                    presents[visit * elf] += elf * (lazy ? 11 : 10);
                }
            }

            return presents;
        }

        private static int FindHouseWithPresents(int target, int[] presents)
        {
            for (int house = 1; house < presents.Length; house++)
            {
                if (presents[house] > target)
                {
                    return house;
                }
            }

            return -1;
        }
    }
}
