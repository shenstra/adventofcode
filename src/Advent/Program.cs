using Advent.Aoc2023;
using Advent.Util;
using System.Diagnostics;

Console.WriteLine("Starting...");
var sw = Stopwatch.StartNew();

try
{
    var day = new Day10(new Input(2023, 10));
    Console.WriteLine($"Part 1: {day.Part1()}");
    Console.WriteLine($"Part 2: {day.Part2()}");
}
catch (Exception ex)
{
    Console.WriteLine($"Oops, exception thrown:\n{ex}");
}

sw.Stop();
Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms");
