namespace Advent.Aoc2021
{
    public class Day06(IInput input)
    {
        private const int initialTimer = 8;

        public void Part1()
        {
            string line = input.GetSingleLine();
            long[] fishWithTimer = InitializeTimerCounts(line);
            RunSimulation(fishWithTimer, 80);
            Console.WriteLine(fishWithTimer.Sum());
        }

        public void Part2()
        {
            string line = input.GetSingleLine();
            long[] fishWithTimer = InitializeTimerCounts(line);
            RunSimulation(fishWithTimer, 256);
            Console.WriteLine(fishWithTimer.Sum());
        }

        private static long[] InitializeTimerCounts(string input)
        {
            int[] fishTimers = input.SplitToInts();
            long[] fishWithTimer = new long[initialTimer + 1];
            foreach (int fishTimer in fishTimers)
            {
                fishWithTimer[fishTimer]++;
            }

            return fishWithTimer;
        }

        private static void RunSimulation(long[] fishWithTimer, int days)
        {
            for (int day = 0; day < days; day++)
            {
                long mommies = fishWithTimer[0];
                for (int timer = 0; timer < initialTimer; timer++)
                {
                    fishWithTimer[timer] = fishWithTimer[timer + 1];
                }
                fishWithTimer[8] = mommies;
                fishWithTimer[6] += mommies;
            }
        }
    }
}