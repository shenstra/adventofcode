using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day09(IInput input)
    {
        public int Part1()
        {
            string[] lines = input.GetLines().ToArray();
            int[][] histories = lines.Select(ParseHistory).ToArray();
            return histories.Select(PredictNextValue).Sum();
        }

        public int Part2()
        {
            string[] lines = input.GetLines().ToArray();
            int[][] histories = lines.Select(ParseHistory).ToArray();
            return histories.Select(PredictPreviousValue).Sum();
        }

        private int PredictNextValue(int[] history)
        {
            if (history.All(v => v == 0))
            {
                return 0;
            }

            int[] differences = new int[history.Length - 1];
            for (int i = 0; i < differences.Length; i++)
            {
                differences[i] = history[i + 1] - history[i];
            }

            return history[^1] + PredictNextValue(differences);
        }

        private int PredictPreviousValue(int[] history)
        {
            if (history.All(v => v == 0))
            {
                return 0;
            }

            int[] differences = new int[history.Length - 1];
            for (int i = 0; i < differences.Length; i++)
            {
                differences[i] = history[i + 1] - history[i];
            }

            return history[0] - PredictPreviousValue(differences);
        }

        private int[] ParseHistory(string line)
        {
            return line.Split(' ').Select(int.Parse).ToArray();
        }
    }
}
