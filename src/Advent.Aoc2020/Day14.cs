namespace Advent.Aoc2020
{
    public class Day14
    {
        private readonly IInput input;
        private readonly Regex maskRegex = new(@"mask = ([X01]{36})");
        private readonly Regex memRegex = new(@"mem\[(\d+)\] = (\d+)");

        public Day14(IInput input)
        {
            this.input = input;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2437:Silly bit operations should not be performed", Justification = "False positive, this silly bit operation is crucial to the algorithm")]
        public void Part1()
        {
            var lines = input.GetLines();
            var registers = new Dictionary<ulong, ulong>();
            ulong zeroMask = 0;
            ulong oneMask = 0;
            foreach (string line in lines)
            {
                var result = maskRegex.Match(line);
                if (result.Success)
                {
                    (zeroMask, oneMask) = GetBitMasks(result.Groups[1].Value);
                }
                else
                {
                    result = memRegex.Match(line);
                    ulong index = ulong.Parse(result.Groups[1].Value);
                    ulong value = ulong.Parse(result.Groups[2].Value);
                    if (!registers.ContainsKey(index))
                    {
                        registers[index] = 0;
                    }

                    registers[index] = (value & ~zeroMask) | oneMask;
                }
            }
            Console.WriteLine(registers.Sum(r => (decimal)r.Value));
        }

        public void Part2()
        {
            var lines = input.GetLines();
            var registers = new Dictionary<ulong, ulong>();
            string mask = "";
            foreach (string line in lines)
            {
                var result = maskRegex.Match(line);
                if (result.Success)
                {
                    mask = result.Groups[1].Value;
                }
                else
                {
                    result = memRegex.Match(line);
                    ulong baseIndex = uint.Parse(result.Groups[1].Value);
                    ulong value = ulong.Parse(result.Groups[2].Value);
                    foreach (ulong index in GetMaskedIndexes(baseIndex, mask))
                    {
                        registers[index] = value;
                    }
                }
            }

            Console.WriteLine(registers.Sum(r => (decimal)r.Value));
        }

        private static (ulong, ulong) GetBitMasks(string mask)
        {
            ulong zeroMask = 0;
            ulong oneMask = 0;
            for (int i = 0; i < 36; i++)
            {
                if (mask[35 - i] == '1')
                {
                    oneMask |= 1UL << i;
                }
                else if (mask[35 - i] == '0')
                {
                    zeroMask |= 1UL << i;
                }
            }

            return (zeroMask, oneMask);
        }

        private static IEnumerable<ulong> GetMaskedIndexes(ulong baseIndex, string mask)
        {
            (_, ulong oneMask) = GetBitMasks(mask);
            baseIndex |= oneMask;
            return GetFloatedIndexes(baseIndex, mask, 0);
        }

        private static IEnumerable<ulong> GetFloatedIndexes(ulong baseIndex, string mask, int bit)
        {
            if (bit == 36)
            {
                yield return baseIndex;
            }
            else
            {
                foreach (ulong index in GetFloatedIndexes(baseIndex, mask, bit + 1))
                {
                    yield return index;
                    if (mask[bit] == 'X')
                    {
                        yield return index ^ (1UL << (35 - bit));
                    }
                }
            }
        }
    }
}
