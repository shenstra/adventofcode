namespace Advent.Aoc2023
{
    public class Day01(IInput input)
    {
        public int Part1()
        {
            Dictionary<string, int> valueMap = new()
            {
                ["0"] = 0,
                ["1"] = 1,
                ["2"] = 2,
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5,
                ["6"] = 6,
                ["7"] = 7,
                ["8"] = 8,
                ["9"] = 9,
            };

            return input.GetLines().Sum(line => GetCalibrationValue(line, valueMap));
        }

        public int Part2()
        {
            Dictionary<string, int> valueMap = new()
            {
                ["one"] = 1,
                ["two"] = 2,
                ["three"] = 3,
                ["four"] = 4,
                ["five"] = 5,
                ["six"] = 6,
                ["seven"] = 7,
                ["eight"] = 8,
                ["nine"] = 9,
                ["0"] = 0,
                ["1"] = 1,
                ["2"] = 2,
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5,
                ["6"] = 6,
                ["7"] = 7,
                ["8"] = 8,
                ["9"] = 9,
            };

            return input.GetLines().Sum(line => GetCalibrationValue(line, valueMap));
        }

        private int GetCalibrationValue(string input, Dictionary<string, int> valueMap)
        {
            return (10 * FindFirstValue(input, valueMap)) + FindLastValue(input, valueMap);
        }

        private static int FindFirstValue(string input, Dictionary<string, int> valueMap)
        {
            for (int i = 0; i < input.Length; i++)
            {
                foreach (var value in valueMap)
                {
                    if (input[i..].StartsWith(value.Key))
                    {
                        return value.Value;
                    }
                }
            }

            return 0;
        }

        private static int FindLastValue(string input, Dictionary<string, int> valueMap)
        {
            for (int i = input.Length; i > 0; i--)
            {
                foreach (var value in valueMap)
                {
                    if (input[..i].EndsWith(value.Key))
                    {
                        return value.Value;
                    }
                }
            }

            return 0;
        }
    }
}
