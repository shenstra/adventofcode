namespace Advent.Aoc2015
{
    public class Day23
    {
        private readonly IInput input;

        public Day23(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var commands = input.GetLines().Select(l => new Command(l)).ToArray();
            var registers = new Dictionary<char, long> { { 'a', 0 }, { 'b', 0 } };
            RunProgram(commands, registers);
            Console.WriteLine(registers['b']);
        }

        public void Part2()
        {
            var commands = input.GetLines().Select(l => new Command(l)).ToArray();
            var registers = new Dictionary<char, long> { { 'a', 1 }, { 'b', 0 } };
            RunProgram(commands, registers);
            Console.WriteLine(registers['b']);
        }

        private static void RunProgram(Command[] commands, Dictionary<char, long> registers)
        {
            int iPtr = 0;
            while (iPtr < commands.Length)
            {
                var command = commands[iPtr];
                switch (command.Instruction)
                {
                    case "hlf":
                        registers[command.Register] /= 2;
                        iPtr++;
                        break;
                    case "tpl":
                        registers[command.Register] *= 3;
                        iPtr++;
                        break;
                    case "inc":
                        registers[command.Register]++;
                        iPtr++;
                        break;
                    case "jmp":
                        iPtr += command.Offset;
                        break;
                    case "jie":
                        iPtr += registers[command.Register] % 2 == 0 ? command.Offset : 1;
                        break;
                    case "jio":
                        iPtr += registers[command.Register] == 1 ? command.Offset : 1;
                        break;
                    default:
                        throw new ApplicationException($"Invalid instruction {command.Instruction}");
                }
            }
        }

        private class Command
        {
            public string Instruction { get; set; }
            public char Register { get; set; }
            public int Offset { get; set; }

            public Command(string input)
            {
                Instruction = input[..3];
                if (Instruction != "jmp")
                {
                    Register = input[4];
                }

                if (Instruction == "jmp")
                {
                    Offset = int.Parse(input[4..]);
                }

                if (Instruction is "jio" or "jie")
                {
                    Offset = int.Parse(input[7..]);
                }
            }
        }
    }
}
