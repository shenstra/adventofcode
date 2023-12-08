using Advent.Util;

namespace Advent.TestUtilities
{
    public class FixedInput(params string[] input) : IInput
    {
        public IEnumerable<string> GetLines()
        {
            return input;
        }
    }
}