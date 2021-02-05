using System;
using Advent.Util;

namespace Advent.Aoc2016
{
    public class Day08
    {
        public void Part1()
        {
            var input = Input.GetLines(2016, 8);
            bool[,] pixels = new bool[50, 6];
            foreach (string line in input)
            {
                FollowInstruction(pixels, line);
            }

            Console.WriteLine(CountLit(pixels));
        }

        public void Part2()
        {
            var input = Input.GetLines(2016, 8);
            bool[,] pixels = new bool[50, 6];
            foreach (string line in input)
            {
                FollowInstruction(pixels, line);
            }

            PrintDisplay(pixels);
        }

        private static void FollowInstruction(bool[,] pixels, string instruction)
        {
            string[] parts = instruction.Split();
            if (parts[0] == "rect")
            {
                int a = int.Parse(parts[1].Split('x')[0]);
                int b = int.Parse(parts[1].Split('x')[1]);
                LightRect(pixels, a, b);
            }
            else
            {
                int a = int.Parse(parts[2][2..]);
                int b = int.Parse(parts[4]);
                if (parts[1] == "row")
                {
                    RotateRow(pixels, a, b);
                }
                else
                {
                    RotateCol(pixels, a, b);
                }
            }
        }

        private static void LightRect(bool[,] pixels, int a, int b)
        {
            for (int x = 0; x < a; x++)
            {
                for (int y = 0; y < b; y++)
                {
                    pixels[x, y] = true;
                }
            }
        }

        private static void RotateRow(bool[,] pixels, int a, int b)
        {
            bool[] newRow = new bool[pixels.GetLength(0)];
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                newRow[(x + b) % pixels.GetLength(0)] = pixels[x, a];
            }

            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                pixels[x, a] = newRow[x];
            }
        }

        private static void RotateCol(bool[,] pixels, int a, int b)
        {
            bool[] newCol = new bool[pixels.GetLength(1)];
            for (int y = 0; y < pixels.GetLength(1); y++)
            {
                newCol[(y + b) % pixels.GetLength(1)] = pixels[a, y];
            }

            for (int y = 0; y < pixels.GetLength(1); y++)
            {
                pixels[a, y] = newCol[y];
            }
        }

        private static int CountLit(bool[,] pixels)
        {
            int lit = 0;
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    if (pixels[x, y])
                    {
                        lit++;
                    }
                }
            }

            return lit;
        }

        private static void PrintDisplay(bool[,] pixels)
        {
            for (int y = 0; y < pixels.GetLength(1); y++)
            {
                for (int x = 0; x < pixels.GetLength(0); x++)
                {
                    Console.Write(pixels[x, y] ? "[]" : "  ");
                }

                Console.WriteLine();
            }
        }
    }
}
