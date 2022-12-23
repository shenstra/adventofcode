using System.Text;

namespace Advent.Aoc2015
{
    public class Day10
    {
        private readonly IInput input;

        public Day10(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            string line = input.GetSingleLine();
            for (int i = 0; i < 40; i++)
            {
                line = LookAndSay(line);
            }

            Console.WriteLine(line.Length);
        }

        public void Part2()
        {
            string line = input.GetSingleLine();
            for (int i = 0; i < 50; i++)
            {
                line = LookAndSay(line);
            }

            Console.WriteLine(line.Length);
        }

        private string LookAndSay(string line)
        {
            var result = new StringBuilder();
            char digit = line[0];
            int count = 1;
            for (int i = 1; i < line.Length; i++)
            {
                if (line[i] == digit)
                {
                    count++;
                }
                else
                {
                    result.Append(count).Append(digit);
                    digit = line[i];
                    count = 1;
                }
            }

            result.Append(count).Append(digit);
            return result.ToString();
        }
    }
}