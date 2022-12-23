namespace Advent.Aoc2015
{
    public class Day01
    {
        private readonly IInput input;

        public Day01(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            string line = input.GetSingleLine();
            Console.WriteLine(line.Count(c => c == '(') - line.Count(c => c == ')'));
        }

        public void Part2()
        {
            string line = input.GetSingleLine();
            int floor = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '(')
                {
                    floor++;
                }
                else if (line[i] == ')')
                {
                    floor--;
                }

                if (floor < 0)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}
