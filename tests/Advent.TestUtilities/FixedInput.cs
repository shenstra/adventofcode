using Advent.Util;

namespace Advent.TestUtilities
{
    public class FixedInput : IInput
    {
        private readonly string[] input;

        public FixedInput(params string[] input)
        {
            this.input = input;
        }

        public IEnumerable<string> GetLines()
        {
            return input;
        }
    }
}