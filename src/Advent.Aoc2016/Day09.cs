using Advent.Util;

namespace Advent.Aoc2016
{
    public class Day09
    {
        private readonly IInput input;

        public Day09(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            long decompressedSizes = input.GetLines().Sum(s => DecompressedSize(s, recurse: false));
            Console.WriteLine(decompressedSizes);
        }

        public void Part2()
        {
            long decompressedSizes = input.GetLines().Sum(s => DecompressedSize(s, recurse: true));
            Console.WriteLine(decompressedSizes);
        }

        private static long DecompressedSize(string input, bool recurse)
        {
            long size = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    int mid = input.IndexOf('x', i);
                    int end = input.IndexOf(')', i);
                    int toRepeat = int.Parse(input[++i..mid]);
                    int repeatCount = int.Parse(input[++mid..end]);
                    i = end + toRepeat;
                    size += repeatCount * (recurse ? DecompressedSize(input.Substring(end + 1, toRepeat), recurse) : toRepeat);
                }
                else
                {
                    size++;
                }
            }

            return size;
        }
    }
}
