namespace Advent.Aoc2022
{
    public class Day13(IInput input)
    {
        public int Part1()
        {
            var packetPairs = input.GetLines().Chunk(3).ToList();
            int result = 0;

            for (int i = 0; i < packetPairs.Count; i++)
            {
                if (AreInOrder(packetPairs[i][0], packetPairs[i][1]))
                {
                    result += i + 1;
                }
            }

            return result;
        }

        public int Part2()
        {
            var packets = input.GetLines().Where(s => s != string.Empty).Select(s => new Data(s)).ToList();
            var divider1 = new Data("[[2]]");
            var divider2 = new Data("[[6]]");

            packets.Add(divider1);
            packets.Add(divider2);
            packets.Sort();

            return (packets.IndexOf(divider1) + 1) * (packets.IndexOf(divider2) + 1);
        }

        public bool AreInOrder(string leftInput, string rightInput)
        {
            var left = new Data(leftInput);
            var right = new Data(rightInput);
            return left.CompareTo(right) == -1;
        }

        private class Data : IComparable<Data>
        {
            public bool IsInteger { get; init; }
            public int Value { get; init; }
            public List<Data> Values { get; init; } = [];

            public Data(string input)
            {
                if (!input.StartsWith('['))
                {
                    IsInteger = true;
                    Value = int.Parse(input);
                }
                else
                {
                    int nesting = 0;
                    int startIndex = 1;
                    for (int i = 1; i < input.Length; i++)
                    {

                        if (input[i] is ',' or ']')
                        {
                            if (nesting == 0 && startIndex != i)
                            {
                                Values.Add(new Data(input[startIndex..i]));
                                startIndex = i + 1;
                            }
                        }

                        if (input[i] == '[')
                        {
                            nesting++;
                        }
                        else if (input[i] == ']')
                        {
                            nesting--;
                        }
                    }
                }
            }

            public Data(Data data)
            {
                Values = [data];
            }

            public int CompareTo(Data? other)
            {
                if (other == null)
                {
                    return 1;
                }

                if (IsInteger && other.IsInteger)
                {
                    return Value.CompareTo(other.Value);
                }

                var self = IsInteger ? new Data(this) : this;
                other = other.IsInteger ? new Data(other) : other;

                int minCount = Math.Min(self.Values.Count, other.Values.Count);
                for (int i = 0; i < minCount; i++)
                {
                    int result = self.Values[i].CompareTo(other.Values[i]);
                    if (result != 0)
                    {
                        return result;
                    }
                }

                return self.Values.Count.CompareTo(other.Values.Count);
            }
        }
    }
}