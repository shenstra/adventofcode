﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day14
    {

        public void Part1()
        {
            var reindeer = Input.GetLines(2015, 14).Select(line => new Reindeer(line));
            Console.WriteLine(reindeer.Max(r => r.GetDistanceAfter(2503)));
        }

        public void Part2()
        {
            var reindeer = Input.GetLines(2015, 14).Select(line => new Reindeer(line)).ToList();
            for (int time = 1; time <= 2503; time++)
            {
                long winningDistance = reindeer.Max(r => r.GetDistanceAfter(time));
                reindeer.ForEach(r => r.ScoreIfWinning(time, winningDistance));
            }

            Console.WriteLine(reindeer.Max(r => r.Score));
        }

        private class Reindeer
        {
            private readonly Regex reindeerRegex = new Regex(@"^(.+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.");

            public Reindeer(string input)
            {
                var match = reindeerRegex.Match(input);
                Name = match.Groups[1].Value;
                Speed = int.Parse(match.Groups[2].Value);
                Stamina = int.Parse(match.Groups[3].Value);
                RestTime = int.Parse(match.Groups[4].Value);
            }

            public string Name { get; set; }
            public int Speed { get; set; }
            public int Stamina { get; set; }
            public int RestTime { get; set; }
            public int Score { get; set; }

            public long GetDistanceAfter(int time)
            {
                int fullCycles = time / (Stamina + RestTime);
                int currentCycleFlyTime = Math.Min(time % (Stamina + RestTime), Stamina);
                int totalFlyTime = (fullCycles * Stamina) + currentCycleFlyTime;
                return Math.BigMul(Speed, totalFlyTime);
            }

            public void ScoreIfWinning(int time, long maxDistance)
            {
                if (GetDistanceAfter(time) == maxDistance)
                {
                    Score++;
                }
            }
        }
    }
}