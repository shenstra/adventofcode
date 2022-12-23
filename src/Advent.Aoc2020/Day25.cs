using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day25
    {
        private readonly IInput input;
        private const int initialSubjectNumber = 7;
        private const int divisor = 20201227;

        public Day25(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var loopSizes = input.GetLongs().Select(FindLoopsSize).ToList();
            Console.WriteLine(Transform(Transform(initialSubjectNumber, loopSizes[0]), loopSizes[1]));
        }

        private long FindLoopsSize(long publicKey)
        {
            long value = 1;
            long loopSize = 0;
            while (value != publicKey)
            {
                value = value * initialSubjectNumber % divisor;
                loopSize++;
            }

            return loopSize;
        }

        private long Transform(long subjectNumber, long loopSize)
        {
            long value = 1;
            for (long i = 0; i < loopSize; i++)
            {
                value = value * subjectNumber % divisor;
            }

            return value;
        }
    }
}