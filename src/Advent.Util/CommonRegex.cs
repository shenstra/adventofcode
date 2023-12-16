using System.Text.RegularExpressions;

namespace Advent.Util
{
    public static partial class CommonRegex
    {
        [GeneratedRegex(@"\s+")]
        public static partial Regex Whitespace();
    }
}
