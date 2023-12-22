namespace Advent.Aoc2023
{
    public class Day13(IInput input)
    {
        public int Part1()
        {
            string[] lines = input.GetLines().ToArray();
            string[][] patterns = ParsePatterns(lines).ToArray();
            return patterns.Select(pattern => GetMirrorNumber(pattern, expectedSmudges: 0)).Sum();
        }

        public int Part2()
        {
            string[] lines = input.GetLines().ToArray();
            string[][] patterns = ParsePatterns(lines).ToArray();
            return patterns.Select(pattern => GetMirrorNumber(pattern, expectedSmudges: 1)).Sum();
        }

        private int GetMirrorNumber(string[] pattern, int expectedSmudges)
        {
            if (TryFindHorizontalMirror(pattern, expectedSmudges, out int linesAboveMirror))
            {
                return 100 * linesAboveMirror;
            }
            else if (TryFindVerticalMirror(pattern, expectedSmudges, out int linesBeforeMirror))
            {
                return linesBeforeMirror;
            }

            throw new ApplicationException("No mirror found");
        }

        private static bool TryFindHorizontalMirror(string[] pattern, int expectedSmudges, out int linesAboveMirror)
        {
            for (int rowsAboveMirror = 1; rowsAboveMirror < pattern.Length; rowsAboveMirror++)
            {
                if (HasHorizontalMirror(pattern, rowsAboveMirror, expectedSmudges))
                {
                    linesAboveMirror = rowsAboveMirror;
                    return true;
                }
            }

            linesAboveMirror = 0;
            return false;
        }

        private static bool HasHorizontalMirror(string[] pattern, int rowsAboveMirror, int expectedSmudges)
        {
            int smudges = 0;

            for (int y = 0; y < rowsAboveMirror; y++)
            {
                int yMirrored = (2 * rowsAboveMirror) - y - 1;

                if (yMirrored < pattern.Length)
                {
                    for (int x = 0; x < pattern[0].Length; x++)
                    {
                        if (pattern[y][x] != pattern[yMirrored][x])
                        {
                            if (smudges++ > expectedSmudges)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return smudges == expectedSmudges;
        }

        private static bool TryFindVerticalMirror(string[] pattern, int expectedSmudges, out int linesBeforeMirror)
        {
            for (int columnsBeforeMirror = 1; columnsBeforeMirror < pattern[0].Length; columnsBeforeMirror++)
            {
                if (CheckVerticalMirror(pattern, columnsBeforeMirror, expectedSmudges))
                {
                    linesBeforeMirror = columnsBeforeMirror;
                    return true;
                }
            }

            linesBeforeMirror = 0;
            return false;
        }

        private static bool CheckVerticalMirror(string[] pattern, int columnsBeforeMirror, int expectedSmudges)
        {
            int smudges = 0;

            for (int x = 0; x < columnsBeforeMirror; x++)
            {
                int xMirrored = (2 * columnsBeforeMirror) - x - 1;

                if (xMirrored >= pattern[0].Length)
                {
                    continue;
                }

                for (int y = 0; y < pattern.Length; y++)
                {
                    if (pattern[y][x] != pattern[y][xMirrored])
                    {
                        if (smudges++ > expectedSmudges)
                        {
                            return false;
                        }
                    }
                }
            }

            return smudges == expectedSmudges;
        }

        private IEnumerable<string[]> ParsePatterns(IEnumerable<string> lines)
        {
            while (lines.Any())
            {
                string[] pattern = lines.TakeWhile(l => l != "").ToArray();
                lines = lines.Skip(pattern.Length + 1);
                yield return pattern;
            }
        }
    }
}
