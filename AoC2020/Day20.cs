using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day20
    {
        public void Part1()
        {
            var tiles = GetTiles(Input.GetLines(2020, 20).ToList());
            int size = (int)Math.Round(Math.Sqrt(tiles.Count));
            var arrangement = new Tile[size, size];
            ArrangeTiles(tiles, arrangement, size, 0, 0);
            Console.WriteLine(arrangement[0, 0].Id * arrangement[0, size - 1].Id * arrangement[size - 1, 0].Id * arrangement[size - 1, size - 1].Id);
        }

        private bool ArrangeTiles(List<Tile> tiles, Tile[,] arrangement, int size, int row, int col)
        {
            if (tiles.Count == 0)
            {
                return true;
            }

            foreach (var tile in tiles)
            {
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.FlipX();
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.FlipY();
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.FlipX();
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.Transpose();
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.FlipX();
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.FlipY();
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.FlipX();
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }
            }

            arrangement[col, row] = null;
            return false;
        }

        private bool TryPlaceTile(List<Tile> tiles, Tile[,] arrangement, int size, int row, int col, Tile tile)
        {
            if ((col == 0 || tile.Left == arrangement[col - 1, row].Right)
                && (row == 0 || tile.Top == arrangement[col, row - 1].Bottom))
            {
                arrangement[col, row] = tile;
                if (ArrangeTiles(tiles.Where(t => t.Id != tile.Id).ToList(), arrangement, size, row + ((col + 1) / size), (col + 1) % size))
                {
                    return true;
                }

                arrangement[col, row] = null;
            }

            return false;
        }

        private List<Tile> GetTiles(List<string> lines)
        {
            var tiles = new List<Tile>();
            for (int i = 0; i < lines.Count - 10; i += 12)
            {
                tiles.Add(new Tile(lines.GetRange(i, 11)));
            }

            return tiles;
        }

        private class Tile
        {
            public Tile(List<string> input)
            {
                Id = long.Parse(input[0][5..^1]);
                Size = input.Count - 1;
                Pixels = new char[Size, Size];
                for (int row = 0; row < Size; row++)
                {
                    for (int col = 0; col < Size; col++)
                    {
                        Pixels[col, row] = input[row + 1][col];
                    }
                }
            }

            public long Id { get; set; }
            public int Size { get; set; }
            public char[,] Pixels { get; set; }

            public string Top => string.Join("", Enumerable.Range(0, Size).Select(i => Pixels[i, 0]));
            public string Bottom => string.Join("", Enumerable.Range(0, Size).Select(i => Pixels[i, Size - 1]));
            public string Left => string.Join("", Enumerable.Range(0, Size).Select(i => Pixels[0, i]));
            public string Right => string.Join("", Enumerable.Range(0, Size).Select(i => Pixels[Size - 1, i]));

            public void FlipX()
            {
                for (int row = 0; row < Size; row++)
                {
                    for (int col = 0; col < Size / 2; col++)
                    {
                        char temp = Pixels[col, row];
                        Pixels[col, row] = Pixels[Size - col - 1, row];
                        Pixels[Size - col - 1, row] = temp;
                    }
                }
            }

            public void FlipY()
            {
                for (int row = 0; row < Size / 2; row++)
                {
                    for (int col = 0; col < Size; col++)
                    {
                        char temp = Pixels[col, row];
                        Pixels[col, row] = Pixels[col, Size - row - 1];
                        Pixels[col, Size - row - 1] = temp;
                    }
                }
            }

            public void Transpose()
            {
                for (int row = 1; row < Size; row++)
                {
                    for (int col = 0; col < row; col++)
                    {
                        char temp = Pixels[col, row];
                        Pixels[col, row] = Pixels[row, col];
                        Pixels[row, col] = temp;
                    }
                }
            }
        }
    }
}