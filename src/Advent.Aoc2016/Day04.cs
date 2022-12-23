using Advent.Util;
using System.Text.RegularExpressions;

namespace Advent.Aoc2016
{
    public class Day04
    {
        private readonly IInput input;
        private readonly Regex roomRegex = new(@"([a-z-]+)-(\d+)\[([a-z]+)\]");

        public Day04(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var rooms = input.GetLines().Select(ParseRoomCode);
            var realRooms = rooms.Where(r => GetChecksum(r.name) == r.checksum);
            Console.WriteLine(realRooms.Sum(r => r.sectorId));
        }

        public void Part2()
        {
            var rooms = input.GetLines().Select(ParseRoomCode);
            var realRooms = rooms.Where(r => GetChecksum(r.name) == r.checksum);
            var northyRooms = realRooms.Where(r => DecryptRoomName(r.name, r.sectorId).Contains("north"));
            foreach (var (name, sectorId, checksum) in northyRooms)
            {
                Console.WriteLine($"{DecryptRoomName(name, sectorId)}: {sectorId}");
            }
        }

        private (string name, int sectorId, string checksum) ParseRoomCode(string input)
        {
            var groups = roomRegex.Match(input).Groups;
            return (groups[1].Value, int.Parse(groups[2].Value), groups[3].Value);
        }

        private static string GetChecksum(string name)
        {
            return string.Concat(name.Replace("-", "")
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Select(g => g.Key)
                .Take(5));
        }

        private static string DecryptRoomName(string name, int sectorId)
        {
            return string.Concat(name.Select(c => c switch
            {
                '-' => ' ',
                _ => (char)('a' + ((c - 'a' + sectorId) % 26)),
            }));
        }
    }
}
