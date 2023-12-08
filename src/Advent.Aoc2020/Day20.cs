namespace Advent.Aoc2020
{
    public class Day20(IInput input)
    {
        public void Part1()
        {
            var tiles = GetTiles(input.GetLines().ToList());
            int size = (int)Math.Round(Math.Sqrt(tiles.Count));
            var arrangement = new Tile[size, size];
            ArrangeTiles(tiles, arrangement, size, 0, 0);
            Console.WriteLine(arrangement[0, 0].Id * arrangement[0, size - 1].Id * arrangement[size - 1, 0].Id * arrangement[size - 1, size - 1].Id);
        }

        public void Part2()
        {
            var tiles = GetTiles(input.GetLines().ToList());
            int size = (int)Math.Round(Math.Sqrt(tiles.Count));
            var arrangement = new Tile[size, size];
            ArrangeTiles(tiles, arrangement, size, 0, 0);
            char[,] image = StitchTiles(arrangement, size);
            MarkNessieLocations(image);
            Console.WriteLine(CountHashes(image));
        }

        private static bool ArrangeTiles(List<Tile> tiles, Tile[,] arrangement, int size, int row, int col)
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

                tile.Pixels = FlipX(tile.Pixels);
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.Pixels = FlipY(tile.Pixels);
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.Pixels = FlipX(tile.Pixels);
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.Pixels = Transpose(tile.Pixels);
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.Pixels = FlipX(tile.Pixels);
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.Pixels = FlipY(tile.Pixels);
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }

                tile.Pixels = FlipX(tile.Pixels);
                if (TryPlaceTile(tiles, arrangement, size, row, col, tile))
                {
                    return true;
                }
            }

            arrangement[col, row] = null;
            return false;
        }

        private static bool TryPlaceTile(List<Tile> tiles, Tile[,] arrangement, int size, int row, int col, Tile tile)
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

        private static char[,] StitchTiles(Tile[,] tiles, int size)
        {
            int tileSize = tiles[0, 0].Pixels.GetLength(0) - 2;
            char[,] image = new char[size * tileSize, size * tileSize];
            for (int tileRow = 0; tileRow < size; tileRow++)
            {
                for (int tileCol = 0; tileCol < size; tileCol++)
                {
                    for (int pixelRow = 0; pixelRow < tileSize; pixelRow++)
                    {
                        for (int pixelCol = 0; pixelCol < tileSize; pixelCol++)
                        {
                            image[(tileCol * tileSize) + pixelCol, (tileRow * tileSize) + pixelRow] = tiles[tileCol, tileRow].Pixels[pixelCol + 1, pixelRow + 1];
                        }
                    }
                }
            }

            return image;
        }

        private static void MarkNessieLocations(char[,] image)
        {
            foreach (char[,] nessieImage in GetNessieImages())
            {
                for (int row = 0; row <= image.GetLength(1) - nessieImage.GetLength(1); row++)
                {
                    for (int col = 0; col <= image.GetLength(0) - nessieImage.GetLength(0); col++)
                    {
                        if (SpotNessie(image, nessieImage, row, col))
                        {
                            MarkNessie(image, nessieImage, row, col);
                        }
                    }
                }
            }
        }

        private static List<char[,]> GetNessieImages()
        {
            char[,] image = new char[3, 20] {
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' '},
                {'#',' ',' ',' ',' ','#','#',' ',' ',' ',' ','#','#',' ',' ',' ',' ','#','#','#'},
                {' ','#',' ',' ','#',' ',' ','#',' ',' ','#',' ',' ','#',' ',' ','#',' ',' ',' '}
            };
            return
            [
                image,
                FlipX(image),
                FlipY(image),
                FlipX(FlipY(image)),
                Transpose(image),
                FlipX(Transpose(image)),
                FlipY(Transpose(image)),
                FlipX(FlipY(Transpose(image))),
            ];
        }

        private static bool SpotNessie(char[,] image, char[,] nessieImage, int row, int col)
        {
            for (int nessieRow = 0; nessieRow < nessieImage.GetLength(1); nessieRow++)
            {
                for (int nessieCol = 0; nessieCol < nessieImage.GetLength(0); nessieCol++)
                {
                    if (nessieImage[nessieCol, nessieRow] == '#' && image[col + nessieCol, row + nessieRow] == '.')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void MarkNessie(char[,] image, char[,] nessieImage, int row, int col)
        {
            for (int nessieRow = 0; nessieRow < nessieImage.GetLength(1); nessieRow++)
            {
                for (int nessieCol = 0; nessieCol < nessieImage.GetLength(0); nessieCol++)
                {
                    if (nessieImage[nessieCol, nessieRow] == '#')
                    {
                        image[col + nessieCol, row + nessieRow] = 'O';
                    }
                }
            }
        }

        private static int CountHashes(char[,] image)
        {
            int count = 0;
            foreach (char c in image)
            {
                if (c == '#')
                {
                    count++;
                }
            }

            return count;
        }

        private static char[,] FlipX(char[,] image)
        {
            char[,] result = new char[image.GetLength(0), image.GetLength(1)];
            for (int row = 0; row < image.GetLength(1); row++)
            {
                for (int col = 0; col < image.GetLength(0); col++)
                {
                    result[col, row] = image[image.GetLength(0) - col - 1, row];
                }
            }

            return result;
        }

        private static char[,] FlipY(char[,] image)
        {
            char[,] result = new char[image.GetLength(0), image.GetLength(1)];
            for (int row = 0; row < image.GetLength(1); row++)
            {
                for (int col = 0; col < image.GetLength(0); col++)
                {
                    result[col, row] = image[col, image.GetLength(1) - row - 1];
                }
            }

            return result;
        }

        private static char[,] Transpose(char[,] image)
        {
            char[,] result = new char[image.GetLength(1), image.GetLength(0)];
            for (int row = 0; row < image.GetLength(1); row++)
            {
                for (int col = 0; col < image.GetLength(0); col++)
                {
                    result[row, col] = image[col, row];
                }
            }

            return result;
        }

        private static List<Tile> GetTiles(List<string> lines)
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
                Pixels = new char[input.Count - 1, input.Count - 1];
                for (int row = 0; row < input.Count - 1; row++)
                {
                    for (int col = 0; col < input.Count - 1; col++)
                    {
                        Pixels[col, row] = input[row + 1][col];
                    }
                }
            }

            public long Id { get; set; }
            public char[,] Pixels { get; set; }

            public string Top => string.Join("", Enumerable.Range(0, Pixels.GetLength(0)).Select(i => Pixels[i, 0]));
            public string Bottom => string.Join("", Enumerable.Range(0, Pixels.GetLength(0)).Select(i => Pixels[i, Pixels.GetLength(1) - 1]));
            public string Left => string.Join("", Enumerable.Range(0, Pixels.GetLength(1)).Select(i => Pixels[0, i]));
            public string Right => string.Join("", Enumerable.Range(0, Pixels.GetLength(1)).Select(i => Pixels[Pixels.GetLength(0) - 1, i]));
        }
    }
}