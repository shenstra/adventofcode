using System.Text;

namespace Advent.Aoc2022
{
    public class Day10(IInput input)
    {
        public int Part1()
        {
            var instructions = input.GetLines();
            var device = new Device();
            device.RunInstructions(instructions);
            return device.AccumulatedSignalStrength;
        }

        public string[] Part2()
        {
            var instructions = input.GetLines();
            var device = new Device();
            device.RunInstructions(instructions);
            return device.GetDisplay();
        }

        private class Device
        {
            private int cycle = 1;
            private int xRegister = 1;
            private readonly char[,] display = new char[40, 6];

            public int AccumulatedSignalStrength { get; private set; }

            public Device()
            {
                for (int row = 0; row < 6; row++)
                {
                    for (int col = 0; col < 40; col++)
                    {
                        display[col, row] = '.';
                    }
                }
            }

            public void RunInstructions(IEnumerable<string> instructions)
            {
                foreach (string instruction in instructions)
                {
                    IncrementCycle();
                    if (instruction != "noop")
                    {
                        IncrementCycle();
                        xRegister += int.Parse(instruction.Split(' ')[1]);
                    }
                }
            }

            public string[] GetDisplay()
            {
                string[] output = new string[6];

                for (int row = 0; row < 6; row++)
                {
                    var stringBuilder = new StringBuilder();
                    for (int col = 0; col < 40; col++)
                    {
                        stringBuilder.Append(display[col, row]);
                    }
                    output[row] = stringBuilder.ToString();
                }

                return output;
            }

            private void IncrementCycle()
            {
                int row = (cycle - 1) / 40;
                int col = (cycle - 1) % 40;

                if (col >= xRegister - 1 && col <= xRegister + 1)
                {
                    display[col, row] = '#';
                }

                if (col == 19)
                {
                    AccumulatedSignalStrength += cycle * xRegister;
                }

                cycle++;
            }
        }
    }
}