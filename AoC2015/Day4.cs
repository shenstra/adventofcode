using System;
using System.Security.Cryptography;
using System.Text;

namespace Advent.AoC2015
{
    internal class Day4
    {
        public void Problem1()
        {
            string input = Input.GetLine(2015, 4);
            for (int i = 1; true; i++)
            {
                string hashString = CalculateMd5Hash($"{input}{i}");
                if (hashString.StartsWith("00000"))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }

        public void Problem2()
        {
            string input = Input.GetLine(2015, 4);
            for (int i = 1; true; i++)
            {
                string hashString = CalculateMd5Hash($"{input}{i}");
                if (hashString.StartsWith("000000"))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }

        private static string CalculateMd5Hash(string input)
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
            string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return hashString;
        }
    }
}
