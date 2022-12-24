using Advent.Aoc2022;
using Advent.Util;
using System.Diagnostics;

Console.WriteLine("Starting...");
var sw = Stopwatch.StartNew();

try
{
    var day = new Day09(new Input(2022, 9));
    Console.WriteLine($"Part 1: {day.Part1()}");
    Console.WriteLine($"Part 2: {day.Part2()}");
}
catch (Exception ex)
{
    Console.WriteLine($"Oops, exception thrown:\n{ex}");
}

sw.Stop();
Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms");
