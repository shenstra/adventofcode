namespace Advent.Aoc2015
{
    public class Day04(IInput input)
    {
        public void Part1()
        {
            string line = input.GetSingleLine();
            var md5 = new MD5Hasher();
            for (int i = 1; ; i++)
            {
                string hashString = md5.Hash($"{line}{i}");
                if (hashString.StartsWith("00000"))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }

        public void Part2()
        {
            string line = input.GetSingleLine();
            var md5 = new MD5Hasher();
            for (int i = 1; ; i++)
            {
                string hashString = md5.Hash($"{line}{i}");
                if (hashString.StartsWith("000000"))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }
    }
}
