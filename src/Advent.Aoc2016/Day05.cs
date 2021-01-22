using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Util;

namespace Advent.Aoc2016
{
    public partial class Day05
    {
        public void Part1()
        {
            string doorCode = Input.GetSingleLine(2016, 5);
            Console.WriteLine(ComputePassword(doorCode));
        }

        public void Part2()
        {
            string doorCode = Input.GetSingleLine(2016, 5);
            Console.WriteLine(ComputeBetterPassword(doorCode));
        }

        private static string ComputePassword(string doorCode)
        {
            var hashes = EnumerateValidHashes(doorCode).Take(8);
            return string.Concat(hashes.Select(h => h[5]));
        }

        private static string ComputeBetterPassword(string doorCode)
        {
            char[] password = Enumerable.Repeat(' ', 8).ToArray();
            foreach (string hash in EnumerateValidHashes(doorCode))
            {
                int position = hash[5] - '0';
                if (position is >= 0 and <= 7 && password[position] == ' ')
                {
                    password[position] = hash[6];
                    if (!password.Contains(' '))
                    {
                        break;
                    }
                }
            }

            return new string(password);
        }

        private static IEnumerable<string> EnumerateValidHashes(string doorCode)
        {
            var md5 = new MD5Hasher();
            for (int index = 0; index < int.MaxValue; index++)
            {
                string hash = md5.Hash($"{doorCode}{index}");
                if (hash.StartsWith("00000"))
                {
                    yield return hash;
                }
            }
        }
    }
}
