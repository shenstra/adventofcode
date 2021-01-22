using System;
using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day04
    {
        public void Part1()
        {
            string input = Input.GetSingleLine(2015, 4);
            var md5 = new MD5Hasher();
            for (int i = 1; true; i++)
            {
                string hashString = md5.Hash($"{input}{i}");
                if (hashString.StartsWith("00000"))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }

        public void Part2()
        {
            string input = Input.GetSingleLine(2015, 4);
            var md5 = new MD5Hasher();
            for (int i = 1; true; i++)
            {
                string hashString = md5.Hash($"{input}{i}");
                if (hashString.StartsWith("000000"))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }
    }
}
