using System.Buffers;
using System.Text.RegularExpressions;

namespace Advent.Aoc2023
{
    public class Day15(IInput input)
    {
        private readonly Regex stepRegex = new(@"([a-z]+)([-=])(\d+)?");

        public int Part1()
        {
            string[] sequence = input.GetSingleLine().Split(",");
            return sequence.Sum(HolidayAsciiStringHelper);
        }

        public int Part2()
        {
            var sequence = input.GetSingleLine().Split(",").Select(ParseStep).ToArray();
            return HashManualArrangementProcedure(sequence);
        }

        private static int HolidayAsciiStringHelper(string v)
        {
            int current = 0;
            foreach (char c in v)
            {
                current += c;
                current *= 17;
                current %= 256;
            }
            return current;
        }

        private int HashManualArrangementProcedure(Step[] sequence)
        {
            var lensBoxes = Enumerable.Range(0, 256).Select(i => new LensBox(i)).ToArray();
            foreach (var step in sequence)
            {
                if (step.Operation == '=')
                {
                    lensBoxes[step.BoxNumber].AddLens(step.Label, step.FocalLength);
                }
                else if (step.Operation == '-')
                {
                    lensBoxes[step.BoxNumber].TryRemoveLens(step.Label);
                }
            }
            return lensBoxes.Sum(box => box.TotalFocalLength);
        }

        private record struct Step(string Label, char Operation, int FocalLength)
        {
            public readonly int BoxNumber => HolidayAsciiStringHelper(Label);
        }

        private class LensBox(int boxNumber)
        {
            private readonly List<Lens> lenses = [];

            public int TotalFocalLength => lenses.Select((lens, slotNumber)
                => (1 + boxNumber) * (slotNumber + 1) * lens.FocalLength).Sum();

            public void AddLens(string label, int focalLength)
            {
                int slot = lenses.FindIndex(l => l.Label == label);
                if (slot == -1)
                {
                    lenses.Add(new(label, focalLength));
                }
                else
                {
                    lenses[slot] = new(label, focalLength);
                }
            }

            public void TryRemoveLens(string label)
            {
                int slot = lenses.FindIndex(l => l.Label == label);
                if (slot != -1)
                {
                    lenses.RemoveAt(slot);
                }
            }
        }

        private record struct Lens(string Label, int FocalLength);

        private Step ParseStep(string step)
        {
            var groups = stepRegex.Match(step).Groups;
            string label = groups[1].Value;
            char operation = groups[2].Value[0];
            int focalLength = operation == '=' ? int.Parse(groups[3].Value) : 0;
            return new Step(label, operation, focalLength);
        }
    }
}
