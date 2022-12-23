namespace Advent.Aoc2021
{
    public class Day11
    {
        private readonly IInput input;
        private const int Size = 10;

        public Day11(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var lines = input.GetLines().ToList();
            int[,] energyLevels = ParseEnergyLevels(lines);
            int flashes = 0;
            for (int step = 0; step < 100; step++)
            {
                flashes += ApplyChargeLevel(energyLevels);
            }
            Console.WriteLine(flashes);
        }

        public void Part2()
        {
            var lines = input.GetLines().ToList();
            int[,] energyLevels = ParseEnergyLevels(lines);
            int step = 1;
            while (ApplyChargeLevel(energyLevels) != 100)
            {
                step++;
            }
            Console.WriteLine(step);
        }

        private int ApplyChargeLevel(int[,] energyLevels)
        {
            var flashing = BumpEnergyLevels(energyLevels);
            int flashes = ApplyFlashes(energyLevels, flashing);
            ResetFlashedLevels(energyLevels);
            return flashes;
        }

        private static Stack<(int, int)> BumpEnergyLevels(int[,] energyLevels)
        {
            var flashing = new Stack<(int, int)>();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (++energyLevels[i, j] == 10)
                    {
                        flashing.Push((i, j));
                    }
                }
            }

            return flashing;
        }

        private static int ApplyFlashes(int[,] energyLevels, Stack<(int, int)> flashing)
        {
            int flashes = 0;
            while (flashing.Any())
            {
                (int i, int j) = flashing.Pop();
                flashes++;
                ApplyFlash(energyLevels, i, j, flashing);
            }

            return flashes;
        }

        private static void ApplyFlash(int[,] energyLevels, int i, int j, Stack<(int, int)> flashing)
        {
            foreach ((int k, int l) in EnumerateNeighbors(i, j))
            {
                if (++energyLevels[k, l] == 10)
                {
                    flashing.Push((k, l));
                }
            }
        }

        private static void ResetFlashedLevels(int[,] energyLevels)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (energyLevels[i, j] > 9)
                    {
                        energyLevels[i, j] = 0;
                    }
                }
            }
        }

        private static IEnumerable<(int i, int j)> EnumerateNeighbors(int i, int j)
        {
            for (int k = Math.Max(i - 1, 0); k <= Math.Min(i + 1, Size - 1); k++)
            {
                for (int l = Math.Max(j - 1, 0); l <= Math.Min(j + 1, Size - 1); l++)
                {
                    if (k != i || l != j)
                    {
                        yield return (k, l);
                    }
                }
            }
        }

        private static int[,] ParseEnergyLevels(List<string> lines)
        {
            int[,] energyLevels = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    energyLevels[i, j] = lines[i][j] - '0';
                }
            }

            return energyLevels;
        }
    }
}