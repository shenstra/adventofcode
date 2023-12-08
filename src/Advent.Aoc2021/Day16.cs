namespace Advent.Aoc2021
{
    public class Day16(IInput input)
    {
        public void Part1()
        {
            int[] bits = ConvertToBits(input.GetSingleLine()).ToArray();
            var rootPacket = ParsePacket(bits, out _);
            Console.WriteLine(rootPacket.VersionSum);
        }

        public void Part2()
        {
            int[] bits = ConvertToBits(input.GetSingleLine()).ToArray();
            var rootPacket = ParsePacket(bits, out _);
            Console.WriteLine(rootPacket.Value);
        }

        private enum PacketType
        {
            Sum,
            Product,
            Minimum,
            Maximum,
            LiteralValue,
            GreaterThan,
            LessThan,
            EqualTo
        };

        private class Packet
        {
            public int Version { get; set; }
            public PacketType TypeId { get; set; }
            public long LiteralValue { get; set; }
            public List<Packet> SubPackets { get; set; } = [];

            public long VersionSum => Version + SubPackets.Sum(p => p.VersionSum);

            public long Value => TypeId switch
            {
                PacketType.Sum => SubPackets.Sum(p => p.Value),
                PacketType.Product => SubPackets.Select(p => p.Value).Aggregate((a, b) => a * b),
                PacketType.Minimum => SubPackets.Min(p => p.Value),
                PacketType.Maximum => SubPackets.Max(p => p.Value),
                PacketType.LiteralValue => LiteralValue,
                PacketType.GreaterThan => SubPackets[0].Value > SubPackets[1].Value ? 1 : 0,
                PacketType.LessThan => SubPackets[0].Value < SubPackets[1].Value ? 1 : 0,
                PacketType.EqualTo => SubPackets[0].Value == SubPackets[1].Value ? 1 : 0,
                _ => throw new NotImplementedException($"Unknown packet type {TypeId}"),
            };
        }

        private Packet ParsePacket(int[] bits, out int index)
        {
            var packet = new Packet
            {
                Version = ParseNumber(bits[0..3]),
                TypeId = (PacketType)ParseNumber(bits[3..6]),
            };

            if (packet.TypeId == PacketType.LiteralValue)
            {
                packet.LiteralValue = ParseLiteralValue(bits[6..], out int subIndex);
                index = 6 + subIndex;
            }
            else
            {
                if (bits[6] == 0)
                {
                    int subPacketsLength = ParseNumber(bits[7..22]);
                    index = 22;
                    while (index < subPacketsLength + 22)
                    {
                        packet.SubPackets.Add(ParsePacket(bits[index..], out int subIndex));
                        index += subIndex;
                    }
                }
                else
                {
                    int subPacketsCount = ParseNumber(bits[7..18]);
                    index = 18;
                    while (packet.SubPackets.Count < subPacketsCount)
                    {
                        packet.SubPackets.Add(ParsePacket(bits[index..], out int subIndex));
                        index += subIndex;
                    }
                }
            }

            return packet;
        }

        private long ParseLiteralValue(int[] bits, out int index)
        {
            index = 0;
            long value = 0;
            bool done = false;
            while (!done)
            {
                done = bits[index] == 0;
                value *= 16;
                value += ParseNumber(bits[index..][1..5]);
                index += 5;
            }

            return value;
        }

        private int ParseNumber(int[] bits)
        {
            int value = 0;
            foreach (int bit in bits)
            {
                value = (2 * value) + bit;
            }

            return value;
        }

        private IEnumerable<int> ConvertToBits(string input)
        {
            foreach (char c in input)
            {
                short value = Convert.ToInt16(c.ToString(), 16);
                for (int i = 0; i < 4; i++)
                {
                    yield return (value & 8) == 0 ? 0 : 1;
                    value <<= 1;
                }
            }
        }
    }
}