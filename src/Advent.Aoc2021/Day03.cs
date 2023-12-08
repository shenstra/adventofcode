namespace Advent.Aoc2021
{
    public class Day03(IInput input)
    {
        public void Part1()
        {
            var numbers = input.GetLines().Select(ParseBinaryNumber).ToList();
            Console.WriteLine(CalculatePowerConsumption(numbers));
        }

        public void Part2()
        {
            var lines = input.GetLines().ToList();
            Console.WriteLine(CalculateRating(lines, OxygenFilter) * CalculateRating(lines, CO2Filter));
        }

        private static int CalculatePowerConsumption(List<int> numbers)
        {
            (int gamma, int epsilon) = (0, 0);
            int bitmask = 1;
            while (numbers.Any(n => n >= bitmask))
            {
                int numberOfOnes = numbers.Count(n => (n & bitmask) != 0);
                if (numberOfOnes * 2 > numbers.Count)
                {
                    gamma += bitmask;
                }
                else
                {
                    epsilon += bitmask;
                }
                bitmask <<= 1;
            }
            return gamma * epsilon;
        }

        private static int CalculateRating(List<string> lines, Func<List<string>, int, List<string>> filter, int index = 0)
        {
            lines = filter(lines, index);
            return lines.Count == 1 ? ParseBinaryNumber(lines.Single()) : CalculateRating(lines, filter, index + 1);
        }

        private static List<string> OxygenFilter(List<string> lines, int index)
        {
            return lines.Count(s => s[index] == '1') >= lines.Count(s => s[index] == '0')
                ? lines.Where(s => s[index] == '1').ToList()
                : lines.Where(s => s[index] == '0').ToList();
        }

        private static List<string> CO2Filter(List<string> lines, int index)
        {
            return lines.Count(s => s[index] == '1') >= lines.Count(s => s[index] == '0')
                ? lines.Where(s => s[index] == '0').ToList()
                : lines.Where(s => s[index] == '1').ToList();
        }

        private static int ParseBinaryNumber(string input)
        {
            return Convert.ToInt32(input, 2);
        }
    }
}