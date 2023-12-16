using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day06(IInput input)
    {
        public int Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var races = ParseRaces(lines);

            var waysToWinRaces = races.Select(race => WaysToWin(race.time, race.distance));
            return waysToWinRaces.Aggregate((a, b) => a * b);
        }

        public int Part2()
        {
            string[] lines = input.GetLines().ToArray();
            (long time, long distance) = ParseBadlyKernedRace(lines);

            return WaysToWin(time, distance);
        }

        private int WaysToWin(long time, long recordDistance)
        {
            int waysToWin = 0;
            for (long holdTime = 0; holdTime < time; holdTime++)
            {
                if (holdTime * (time - holdTime) > recordDistance)
                {
                    waysToWin++;
                }
            }
            return waysToWin;
        }

        private IEnumerable<(long time, long distance)> ParseRaces(string[] lines)
        {
            long[] times = ParseLabeledNumbers(lines[0]);
            long[] distances = ParseLabeledNumbers(lines[1]);
            for (int i = 0; i < times.Length; i++)
            {
                yield return (times[i], distances[i]);
            }
        }

        private static long[] ParseLabeledNumbers(string line)
        {
            string numberString = line.Split(':')[1].Trim();
            return CommonRegex.Whitespace().Split(numberString).Select(long.Parse).ToArray();
        }

        private (long time, long distance) ParseBadlyKernedRace(string[] lines)
        {
            return (ParseBadlyKernedNumber(lines[0]), ParseBadlyKernedNumber(lines[1]));
        }

        private static long ParseBadlyKernedNumber(string line)
        {
            string numberString = line.Split(':')[1].Replace(" ", "");
            return long.Parse(numberString);
        }
    }
}
