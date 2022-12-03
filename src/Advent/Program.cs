using System.Diagnostics;
using Day = Advent.Aoc2022.Day03;

Console.WriteLine("Starting...");
var sw = Stopwatch.StartNew();

try
{
    new Day().Part1();
    new Day().Part2();
}
catch (Exception ex)
{
    Console.WriteLine($"Oops, exception thrown:\n{ex}");
}

sw.Stop();
Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms");
