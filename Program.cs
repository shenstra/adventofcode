using Advent.AoC2020;
using System;
using System.Diagnostics;

Console.WriteLine("Starting...");
var sw = Stopwatch.StartNew();
try
{
    new Day11().Problem2();
}
catch (Exception ex)
{
    Console.WriteLine($"Oops, exception thrown:\n{ex}");
}
sw.Stop();
Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms");
