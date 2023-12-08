namespace Advent.Aoc2016
{
    public class Day07(IInput input)
    {
        public void Part1()
        {
            var ips = input.GetLines();
            Console.WriteLine(ips.Count(SupportsTls));
        }

        public void Part2()
        {
            var ips = input.GetLines();
            Console.WriteLine(ips.Count(SupportsSsl));
        }

        private static bool SupportsTls(string ip)
        {
            var (snets, hnets) = ChunkIp(ip);
            return snets.Any(ContainsAbba)
                && !hnets.Any(ContainsAbba);
        }

        private bool SupportsSsl(string ip)
        {
            var (snets, hnets) = ChunkIp(ip);
            var abas = snets.SelectMany(EnumerateAbas);
            return abas.Select(InvertAba).Any(bab => hnets.Any(net => net.Contains(bab)));
        }

        private static bool ContainsAbba(string net)
        {
            return Enumerable.Range(0, net.Length - 3)
                .Any(i => StartsWithAbba(net[i..]));
        }

        private static IEnumerable<string> EnumerateAbas(string net)
        {
            return Enumerable.Range(0, net.Length - 2)
                .Where(i => StartsWithAba(net[i..]))
                .Select(i => net[i..(i + 3)]);
        }

        private static string InvertAba(string net)
        {
            return string.Concat(net[1], net[0], net[1]);
        }

        private static bool StartsWithAbba(string net)
        {
            return net[0] != net[1]
                && net[0] == net[3]
                && net[1] == net[2];
        }

        private static bool StartsWithAba(string net)
        {
            return net[0] != net[1]
                && net[0] == net[2];
        }

        private static (List<string> snets, List<string> hnets) ChunkIp(string input)
        {
            var snets = new List<string>();
            var hnets = new List<string>();
            while (input != string.Empty)
            {
                int openBrace = input.IndexOf('[');
                if (openBrace != -1)
                {
                    int closeBrace = input.IndexOf(']');
                    snets.Add(input[..openBrace++]);
                    hnets.Add(input[openBrace..closeBrace++]);
                    input = input[closeBrace..];
                }
                else
                {
                    snets.Add(input);
                    input = string.Empty;
                }
            }

            return (snets, hnets);
        }
    }
}
