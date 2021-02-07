using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2016
{
    public class Day10
    {
        public void Part1And2()
        {
            var input = Input.GetLines(2016, 10);
            var actors = CreateActors(input);
            ActivateInputs(input, actors);
            int out0 = actors.Single(a => a.Type == "output" && a.Id == 0).Values.Single();
            int out1 = actors.Single(a => a.Type == "output" && a.Id == 1).Values.Single();
            int out2 = actors.Single(a => a.Type == "output" && a.Id == 2).Values.Single();
            Console.WriteLine(out0 * out1 * out2);
        }

        private static List<Actor> CreateActors(IEnumerable<string> lines)
        {
            var actorRegex = new Regex(@"bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)");
            var actors = new List<Actor>();
            foreach (string line in lines.Where(s => s.StartsWith("bot")))
            {
                var match = actorRegex.Match(line);
                int botId = int.Parse(match.Groups[1].Value);
                var bot = GetOrCreateActor(actors, "bot", botId);
                bot.LowOutput = GetOrCreateActor(actors, match.Groups[2].Value, int.Parse(match.Groups[3].Value));
                bot.HighOutput = GetOrCreateActor(actors, match.Groups[4].Value, int.Parse(match.Groups[5].Value));
            }

            return actors;
        }

        private static Actor GetOrCreateActor(List<Actor> actors, string type, int id)
        {
            if (!actors.Any(a => a.Type == type && a.Id == id))
            {
                actors.Add(new Actor(type, id));
            }

            return actors.Single(a => a.Type == type && a.Id == id);
        }

        private static void ActivateInputs(IEnumerable<string> lines, List<Actor> actors)
        {
            var inputRegex = new Regex(@"value (\d+) goes to bot (\d+)");
            foreach (string line in lines.Where(s => s.StartsWith("value")))
            {
                var match = inputRegex.Match(line);
                int value = int.Parse(match.Groups[1].Value);
                int botId = int.Parse(match.Groups[2].Value);
                var bot = actors.Single(a => a.Type == "bot" && a.Id == botId);
                bot.Send(value);
            }
        }

        private class Actor
        {
            public string Type { get; private init; }
            public int Id { get; private init; }
            public Actor LowOutput { get; set; }
            public Actor HighOutput { get; set; }
            public List<int> Values { get; private init; } = new List<int>();

            public Actor(string type, int id)
            {
                Type = type;
                Id = id;
            }

            public override string ToString()
            {
                return $"{Type} {Id}";
            }

            public void Send(int value)
            {
                Values.Add(value);
                if (Type == "bot" && Values.Count == 2)
                {
                    if (Values.Contains(61) && Values.Contains(17))
                    {
                        Console.WriteLine($"Bot {Id} compares 61 and 17");
                    }

                    LowOutput.Send(Values.Min());
                    HighOutput.Send(Values.Max());
                    Values.Clear();
                }
            }
        }
    }
}
