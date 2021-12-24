using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day20
    {
        public void Part1()
        {
            var (enhancement, image) = ParseInput(Input.GetLines(2021, 20).ToList());
            for (int i = 0; i < 2; i++)
            {
                image = EnhanceImage(image, enhancement, i);
            }
            Console.WriteLine(CountLitPixels(image));
        }

        public void Part2()
        {
            var (enhancement, image) = ParseInput(Input.GetLines(2021, 20).ToList());
            for (int i = 0; i < 50; i++)
            {
                image = EnhanceImage(image, enhancement, i);
            }
            Console.WriteLine(CountLitPixels(image));
        }

        private char[,] EnhanceImage(char[,] image, char[] enhancement, int iteration)
        {
            int width = image.GetLength(0) + 2;
            int height = image.GetLength(1) + 2;
            char[,] enhanced = new char[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    enhanced[x, y] = EnhancePixel(x - 1, y - 1, image, enhancement, iteration);
                }
            }

            return enhanced;
        }

        private char EnhancePixel(int x, int y, char[,] image, char[] enhancement, int iteration)
        {
            bool spaceIsLit = iteration % 2 == 1 && enhancement[0] == '#';
            int index = 0;
            for (int py = y - 1; py <= y + 1; py++)
            {
                for (int px = x - 1; px <= x + 1; px++)
                {
                    index *= 2;
                    if (px >= 0 && px < image.GetLength(0) && py >= 0 && py < image.GetLength(1))
                    {
                        if (image[px, py] == '#')
                        {
                            index++;
                        }
                    }
                    else if (spaceIsLit)
                    {
                        index++;
                    }
                }
            }

            return enhancement[index];
        }

        private int CountLitPixels(char[,] image)
        {
            int count = 0;
            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    if (image[x, y] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private (char[] enhancement, char[,] image) ParseInput(List<string> input)
        {
            char[] enhancement = input[0].ToCharArray();
            int height = input.Count - 2;
            int width = input[2].Length;
            char[,] image = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    image[x, y] = input[y + 2][x];
                }
            }

            return (enhancement, image);
        }
    }
}