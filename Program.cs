using Advent.AoC2015;
using System;
using System.Diagnostics;

Console.WriteLine("Starting...");
var sw = Stopwatch.StartNew();
try
{
    new Day6().Problem1();
    new Day6().Problem2();
}
catch (Exception ex)
{
    Console.WriteLine($"Oops, exception thrown:\n{ex}");
}
sw.Stop();
Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms");
