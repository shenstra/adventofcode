using Advent.Util;

namespace Advent.Aoc2016
{
    public class Day02
    {
        public void Part1()
        {
            var instructions = Input.GetLines(2016, 2);
            char[,] keypad = new char[,] {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' },
            };
            Console.WriteLine(GetDoorCode(instructions, keypad, startButton: '5'));
        }

        public void Part2()
        {
            var instructions = Input.GetLines(2016, 2);
            char[,] keypad = new char[,] {
                { ' ', ' ', '1', ' ', ' ' },
                { ' ', '2', '3', '4', ' ' },
                { '5', '6', '7', '8', '9' },
                { ' ', 'A', 'B', 'C', ' ' },
                { ' ', ' ', 'D', ' ', ' ' },
            };
            Console.WriteLine(GetDoorCode(instructions, keypad, startButton: '5'));
        }

        private static string GetDoorCode(IEnumerable<string> instructions, char[,] keypad, char startButton)
        {
            (int row, int col) = FindButton(startButton, keypad);
            string code = string.Empty;
            foreach (string instruction in instructions)
            {
                (row, col) = FollowInstruction(instruction, row, col, keypad);
                code += keypad[row, col];
            }

            return code;
        }

        private static (int row, int col) FindButton(char button, char[,] keypad)
        {
            for (int row = 0; row < keypad.GetLength(0); row++)
            {
                for (int col = 0; col < keypad.GetLength(1); col++)
                {
                    if (keypad[row, col] == button)
                    {
                        return (row, col);
                    }
                }
            }

            throw new ArgumentException($"Provided keypad doesn't have a '{button}' button");
        }

        private static (int row, int col) FollowInstruction(string instruction, int row, int col, char[,] keypad)
        {
            foreach (char step in instruction)
            {
                (row, col) = step switch
                {
                    'U' when IsValidCoordinate(row - 1, col, keypad) => (row - 1, col),
                    'D' when IsValidCoordinate(row + 1, col, keypad) => (row + 1, col),
                    'L' when IsValidCoordinate(row, col - 1, keypad) => (row, col - 1),
                    'R' when IsValidCoordinate(row, col + 1, keypad) => (row, col + 1),
                    _ => (row, col),
                };
            }

            return (row, col);
        }

        private static bool IsValidCoordinate(int row, int col, char[,] keypad)
        {
            return row >= 0 && row < keypad.GetLength(0)
                && col >= 0 && col < keypad.GetLength(1)
                && keypad[row, col] != ' ';
        }
    }
}
