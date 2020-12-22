using System;
using System.Text;

namespace Advent.AoC2015
{
    internal class Day10
    {
        public void Part1()
        {
            string line = Input.GetSingleLine(2015, 10);
            for (int i = 0; i < 40; i++)
            {
                line = LookAndSay(line);
            }

            Console.WriteLine(line.Length);
        }

        public void Part2()
        {
            string line = Input.GetSingleLine(2015, 10);
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