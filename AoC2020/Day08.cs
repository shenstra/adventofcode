using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day08
    {
        public void Part1()
        {
            var instructions = Input.GetLines(2020, 8).Select(l => new Instruction(l)).ToList();
            RunProgram(instructions, out int accumulator);
            Console.WriteLine($"Accumulator value: {accumulator}");
        }

        public void Part2()
        {
            var instructions = Input.GetLines(2020, 8).Select(l => new Instruction(l)).ToList();

            for (int i = 0; i < instructions.Count; i++)
            {
                if (instructions[i].SwapType())
                {
                    bool result = RunProgram(instructions, out int accumulator);
                    instructions[i].SwapType();
                    if (result)
                    {
                        Console.WriteLine("Program ran successfully");
                        Console.WriteLine($"Accumulator value: {accumulator}");
                        break;
                    }
                }
            }
        }

        private static bool RunProgram(List<Instruction> instructions, out int accumulator)
        {
            var history = new List<int>();
            int current = 0;
            accumulator = 0;
            while (current < instructions.Count)
            {
                if (history.Contains(current))
                {
                    return false;
                }
                history.Add(current);
                switch (instructions[current].Type)
                {
                    case InstructionType.Nop:
                        current++;
                        break;
                    case InstructionType.Jmp:
                        current += instructions[current].Value;
                        break;
                    case InstructionType.Acc:
                        accumulator += instructions[current].Value;
                        current++;
                        break;
                    default:
                        throw new ApplicationException($"Unknown instruction type {instructions[current].Type}");
                }
            }

            return true;
        }

        private class Instruction
        {
            public Instruction(string line)
            {
                string[] parts = line.Split(" ");
                Type = ParseInstructionType(parts[0]);
                Value = int.Parse(parts[1]);
            }

            public InstructionType Type { get; private set; }
            public int Value { get; private set; }

            public bool SwapType()
            {
                switch (Type)
                {
                    case InstructionType.Acc:
                        return false;
                    case InstructionType.Jmp:
                        Type = InstructionType.Nop;
                        return true;
                    case InstructionType.Nop:
                        Type = InstructionType.Jmp;
                        return true;
                    default:
                        return false;
                }
            }

            private static InstructionType ParseInstructionType(string input)
            {
                return input == "nop" ? InstructionType.Nop
                                                    : input == "jmp" ? InstructionType.Jmp
                                                    : input == "acc" ? InstructionType.Acc
                                                    : throw new ArgumentException($"Invalid instruction {input}");
            }
        }

        private enum InstructionType
        {
            Nop,
            Jmp,
            Acc
        }
    }
}
