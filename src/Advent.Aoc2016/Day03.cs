using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2016
{
    public class Day03
    {
        private readonly Regex triangleRegex = new(@"(\d+)\s+(\d+)\s+(\d+)");

        public void Part1()
        {
            var triples = GetTriples(Input.GetLines(2016, 3));
            Console.WriteLine(triples.Count(IsValidTriangle));
        }

        public void Part2()
        {
            var triples = GetTriplesVertically(Input.GetLines(2016, 3).ToArray());
            Console.WriteLine(triples.Count(IsValidTriangle));
        }

        private IEnumerable<(int a, int b, int c)> GetTriples(IEnumerable<string> input)
        {
            foreach (string line in input)
            {
                var groups = triangleRegex.Match(line).Groups;
                yield return ParseTriple(groups[1], groups[2], groups[3]);
            }
        }

        private IEnumerable<(int a, int b, int c)> GetTriplesVertically(string[] input)
        {
            for (int i = 0; i < input.Length; i += 3)
            {
                var groups1 = triangleRegex.Match(input[i]).Groups;
                var groups2 = triangleRegex.Match(input[i + 1]).Groups;
                var groups3 = triangleRegex.Match(input[i + 2]).Groups;
                yield return ParseTriple(groups1[1], groups2[1], groups3[1]);
                yield return ParseTriple(groups1[2], groups2[2], groups3[2]);
                yield return ParseTriple(groups1[3], groups2[3], groups3[3]);
            }
        }

        private static (int a, int b, int c) ParseTriple(Group groupA, Group groupB, Group groupC)
        {
            return (int.Parse(groupA.Value), int.Parse(groupB.Value), int.Parse(groupC.Value));
        }

        private static bool IsValidTriangle((int a, int b, int c) triple)
        {
            return triple.a + triple.b > triple.c
                && triple.a + triple.c > triple.b
                && triple.b + triple.c > triple.a;
        }
    }
}
