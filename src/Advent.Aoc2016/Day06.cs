using System;
using System.Linq;
using Advent.Util;

namespace Advent.Aoc2016
{
    public class Day06
    {
        public void Part1()
        {
            string[] input = Input.GetLines(2016, 6).ToArray();
            Console.WriteLine(ErrorCorrectMessage(input, modifiedRepetition: false));
        }

        public void Part2()
        {
            string[] input = Input.GetLines(2016, 6).ToArray();
            Console.WriteLine(ErrorCorrectMessage(input, modifiedRepetition: true));
        }

        private static string ErrorCorrectMessage(string[] input, bool modifiedRepetition)
        {
            return string.Concat(Enumerable.Range(0, input[0].Length).Select(i =>
                modifiedRepetition ? LeastFrequentLetter(input, i) : MostFrequenceLetter(input, i)));
        }

        private static char MostFrequenceLetter(string[] input, int position)
        {
            return input.Select(word => word[position])
                .GroupBy(c => c).OrderByDescending(g => g.Count())
                .First().Key;
        }

        private static char LeastFrequentLetter(string[] input, int position)
        {
            return input.Select(word => word[position])
                .GroupBy(c => c).OrderBy(g => g.Count())
                .First().Key;
        }
    }
}
