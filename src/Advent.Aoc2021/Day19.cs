using Advent.Util;

namespace Advent.Aoc2021
{
    public class Day19
    {
        public void Part1()
        {
            var scanners = ParseScanners(Input.GetLines(2021, 19)).ToList();
            CalculatePositions(scanners);
            var beacons = scanners.SelectMany(s => s.Signals).Distinct();
            Console.WriteLine(beacons.Count());
        }

        public void Part2()
        {
            var scanners = ParseScanners(Input.GetLines(2021, 19)).ToList();
            CalculatePositions(scanners);
            var distances = scanners.SelectMany(s => scanners.Select(t => HamiltonDistance(s.Position, t.Position)));
            Console.WriteLine(distances.Max());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3267:Loops should be simplified with \"LINQ\" expressions", Justification = "False positive, this loop can't be turned into a LINQ expression")]
        private static void CalculatePositions(List<Scanner> scanners)
        {
            scanners[0].PositionKnown = true;
            while (scanners.Any(s => !s.PositionKnown))
            {
                foreach (var unknown in scanners.Where(s => !s.PositionKnown))
                {
                    foreach (var known in scanners.Where(s => s.PositionKnown))
                    {
                        if (TryCalculatePosition(unknown, known))
                        {
                            break;
                        }
                    }
                }
            }
        }

        private static bool TryCalculatePosition(Scanner unknown, Scanner known)
        {
            foreach (var rotate in EnumerateRotateFunctions())
            {
                var rotatedSignals = unknown.Signals.Select(rotate).ToList();
                for (int i = 0; i < unknown.Signals.Count; i++)
                {
                    for (int j = 0; j < known.Signals.Count; j++)
                    {
                        var translate = GetTranslateFunction(rotatedSignals[i], known.Signals[j]);
                        var translatedSignals = rotatedSignals.Select(translate).ToList();
                        if (translatedSignals.Intersect(known.Signals).Count() >= 12)
                        {
                            unknown.Position = translate(new(0, 0, 0));
                            unknown.PositionKnown = true;
                            unknown.Signals = translatedSignals;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static Func<RelativePosition, RelativePosition> GetTranslateFunction(RelativePosition from, RelativePosition to)
        {
            return point => new RelativePosition(
                point.X - from.X + to.X,
                point.Y - from.Y + to.Y,
                point.Z - from.Z + to.Z
            );
        }

        private static IEnumerable<Func<RelativePosition, RelativePosition>> EnumerateRotateFunctions()
        {
            yield return point => new RelativePosition(+point.X, +point.Y, +point.Z);
            yield return point => new RelativePosition(-point.Y, +point.X, +point.Z);
            yield return point => new RelativePosition(-point.X, -point.Y, +point.Z);
            yield return point => new RelativePosition(+point.Y, -point.X, +point.Z);

            yield return point => new RelativePosition(+point.X, +point.Z, -point.Y);
            yield return point => new RelativePosition(-point.Z, +point.X, -point.Y);
            yield return point => new RelativePosition(-point.X, -point.Z, -point.Y);
            yield return point => new RelativePosition(+point.Z, -point.X, -point.Y);

            yield return point => new RelativePosition(+point.X, -point.Y, -point.Z);
            yield return point => new RelativePosition(+point.Y, +point.X, -point.Z);
            yield return point => new RelativePosition(-point.X, +point.Y, -point.Z);
            yield return point => new RelativePosition(-point.Y, -point.X, -point.Z);

            yield return point => new RelativePosition(+point.X, -point.Z, +point.Y);
            yield return point => new RelativePosition(+point.Z, +point.X, +point.Y);
            yield return point => new RelativePosition(-point.X, +point.Z, +point.Y);
            yield return point => new RelativePosition(-point.Z, -point.X, +point.Y);

            yield return point => new RelativePosition(-point.Z, +point.Y, +point.X);
            yield return point => new RelativePosition(-point.Y, -point.Z, +point.X);
            yield return point => new RelativePosition(+point.Z, -point.Y, +point.X);
            yield return point => new RelativePosition(+point.Y, +point.Z, +point.X);

            yield return point => new RelativePosition(+point.Z, +point.Y, -point.X);
            yield return point => new RelativePosition(-point.Y, +point.Z, -point.X);
            yield return point => new RelativePosition(-point.Z, -point.Y, -point.X);
            yield return point => new RelativePosition(+point.Y, -point.Z, -point.X);
        }

        private static int HamiltonDistance(RelativePosition a, RelativePosition b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
        }

        private static IEnumerable<Scanner> ParseScanners(IEnumerable<string> input)
        {
            var current = new Scanner();
            foreach (string line in input)
            {
                if (line.StartsWith("---"))
                {
                    current.Id = int.Parse(line.Split(' ')[2]);
                }
                else if (line == string.Empty)
                {
                    yield return current;
                    current = new Scanner();
                }
                else
                {
                    int[] point = line.SplitToInts();
                    current.Signals.Add(new RelativePosition(point[0], point[1], point[2]));
                }
            }

            yield return current;
        }

        private class Scanner
        {
            public int Id { get; set; }
            public bool PositionKnown { get; set; }
            public RelativePosition Position { get; set; }
            public List<RelativePosition> Signals { get; set; } = new();
        }
        private record struct RelativePosition(int X, int Y, int Z);
    }

}