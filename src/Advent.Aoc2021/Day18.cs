namespace Advent.Aoc2021
{
    public class Day18(IInput input)
    {
        public void Part1()
        {
            var snailfishNumbers = input.GetLines().Select(ParseElements);
            var result = snailfishNumbers.Aggregate(Add);
            Console.WriteLine(Magnitude(result));
        }

        public void Part2()
        {
            var snailfishNumbers = input.GetLines().Select(ParseElements).ToList();
            int highestMagnitude = 0;
            for (int i = 0; i < snailfishNumbers.Count; i++)
            {
                for (int j = 0; j < snailfishNumbers.Count; j++)
                {
                    if (i != j)
                    {
                        var result = Add(snailfishNumbers[i], snailfishNumbers[j]);
                        highestMagnitude = Math.Max(Magnitude(result), highestMagnitude);
                    }
                }
            }

            Console.WriteLine(highestMagnitude);
        }

        private static List<Element> Add(IEnumerable<Element> a, IEnumerable<Element> b)
        {
            var result = new List<Element>
            {
                new('['),
                new(','),
                new(']'),
            };
            result.InsertRange(2, b);
            result.InsertRange(1, a);
            Reduce(result);
            return result;
        }

        private static void Reduce(List<Element> elements)
        {
            while (TryExplode(elements) || TrySplit(elements))
            {
                // Keep reducing
            }
        }

        private static bool TryExplode(List<Element> elements)
        {
            int depth = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Character == '[')
                {
                    depth++;
                }
                else if (elements[i].Character == ']')
                {
                    depth--;
                }

                if (depth == 5)
                {
                    Explode(elements, i);
                    return true;
                }
            }

            return false;
        }

        private static void Explode(List<Element> elements, int index)
        {
            int leftNumber = elements.Skip(index + 1).First().Number;
            int rightNumber = elements.Skip(index + 3).First().Number;

            elements.RemoveRange(index, 5);
            elements.Insert(index, new Element(0));

            for (int i = index - 1; i >= 0; i--)
            {
                if (elements[i].IsNumber)
                {
                    elements[i].Number += leftNumber;
                    break;
                }
            }

            for (int i = index + 1; i < elements.Count; i++)
            {
                if (elements[i].IsNumber)
                {
                    elements[i].Number += rightNumber;
                    break;
                }
            }
        }

        private static bool TrySplit(List<Element> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].IsNumber && elements[i].Number > 9)
                {
                    Split(elements, i);
                    return true;
                }
            }

            return false;
        }

        private static void Split(List<Element> elements, int index)
        {
            int number = elements[index].Number;
            var splitElements = new List<Element>
            {
                new('['),
                new(number / 2),
                new(','),
                new((number + 1) / 2),
                new(']'),
            };
            elements.RemoveAt(index);
            elements.InsertRange(index, splitElements);
        }

        private int Magnitude(IEnumerable<Element> elements)
        {
            if (elements.First().IsNumber)
            {
                return elements.First().Number;
            }

            int depth = 0;
            for (int i = 0; i < elements.Count(); i++)
            {
                if (elements.ElementAt(i).Character == '[')
                {
                    depth++;
                }
                else if (elements.ElementAt(i).Character == ']')
                {
                    depth--;
                }
                else if (elements.ElementAt(i).Character == ',' && depth == 1)
                {
                    var left = elements.Skip(1).Take(i - 1);
                    var right = elements.Skip(i + 1);
                    return (3 * Magnitude(left)) + (2 * Magnitude(right));
                }
            }

            return 0;
        }

        private static IEnumerable<Element> ParseElements(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (char.IsDigit(number[i]))
                {
                    string digits = number[i].ToString();
                    while (char.IsDigit(number[i + 1]))
                    {
                        digits += number[++i];
                    }
                    yield return new Element(int.Parse(digits));
                }
                else
                {
                    yield return new Element(number[i]);
                }
            }
        }

        private class Element
        {
            public bool IsNumber { get; set; }
            public int Number { get; set; }
            public char Character { get; set; }

            public Element(int number)
            {
                IsNumber = true;
                Number = number;
            }

            public Element(char character)
            {
                IsNumber = false;
                Character = character;

            }
        }
    }
}