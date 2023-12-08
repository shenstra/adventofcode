namespace Advent.Aoc2020
{
    public class Day15(IInput input)
    {
        public void Part1()
        {
            int[] numbers = input.GetSingleLine().SplitToInts();
            Console.WriteLine(GetNthNumber(numbers, 2020));
        }

        public void Part2()
        {
            int[] numbers = input.GetSingleLine().SplitToInts();
            Console.WriteLine(GetNthNumber(numbers, 30000000));
        }

        private static int GetNthNumber(int[] numbers, int limit)
        {
            int[] lastIndex = Enumerable.Repeat(-1, limit).ToArray();
            for (int round = 0; round < numbers.Length - 1; round++)
            {
                lastIndex[numbers[round]] = round;
            }

            int prevNumber = numbers.Last();
            int newNumber;
            for (int round = numbers.Length; round < limit; round++)
            {
                newNumber = lastIndex[prevNumber] == -1 ? 0 : round - 1 - lastIndex[prevNumber];
                lastIndex[prevNumber] = round - 1;
                prevNumber = newNumber;
            }

            return prevNumber;
        }
    }
}
