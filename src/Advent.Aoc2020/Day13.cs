using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day13
    {
        public void Part1()
        {
            var lines = Input.GetLines(2020, 13).ToList();
            ulong timeNow = ulong.Parse(lines[0]);
            (_, var busIds) = GetBusEntriesAndIds(lines[1]);
            var waitingTimes = busIds.ToDictionary(id => id, id => id - (timeNow % id));
            ulong firstBusId = waitingTimes.OrderBy(wt => wt.Value).First().Key;
            Console.WriteLine(firstBusId * waitingTimes[firstBusId]);
        }

        public void Part2()
        {
            var lines = Input.GetLines(2020, 13);
            (var entries, var busIds) = GetBusEntriesAndIds(lines.Skip(1).Single());
            var expectedDepartures = busIds.ToDictionary(id => id, id => (ulong)entries.IndexOf(id.ToString()));
            Console.WriteLine(GetFirstTime(expectedDepartures));
        }

        private static (List<string>, List<ulong>) GetBusEntriesAndIds(string line)
        {
            var entries = line.Split(",").ToList();
            var busIds = entries.Where(s => s != "x").Select(s => ulong.Parse(s)).ToList();
            return (entries, busIds);
        }

        private static ulong GetFirstTime(Dictionary<ulong, ulong> expectedDepartures)
        {
            ulong firstTime = 1;
            ulong interval = 1;
            foreach (ulong busId in expectedDepartures.Keys)
            {
                for (ulong time = firstTime; time < ulong.MaxValue; time += interval)
                {
                    if ((time + expectedDepartures[busId]) % busId == 0)
                    {
                        firstTime = time;
                        interval = interval * busId / GreatestCommonDivisor(interval, busId);
                        break;
                    }
                }
            }

            return firstTime;
        }

        private static ulong GreatestCommonDivisor(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return a | b;
        }
    }
}
