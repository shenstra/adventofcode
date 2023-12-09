using Advent.Util;
using System.Data;

namespace Advent.Aoc2023
{
    public class Day05(IInput input)
    {
        public long Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var seeds = ParseSeeds(lines[0]);
            var almanac = new Almanac(lines);
            return seeds.Select(almanac.LookUpSeedLocation).Min();
        }

        public long Part2()
        {
            string[] lines = input.GetLines().ToArray();
            var seedRanges = ParseSeedRanges(lines[0]);
            var almanac = new Almanac(lines);

            for (long location = 0; location < long.MaxValue; location++)
            {
                long seed = almanac.LookupLocationSeed(location);
                if (IsInRanges(seed, seedRanges))
                {
                    return location;
                }
            }

            return -1;
        }

        private bool IsInRanges(long seed, List<(long, long)> seedRanges)
        {
            foreach (var (start, length) in seedRanges)
            {
                if (seed >= start && seed < start + length)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<long> ParseSeeds(string line)
        {
            return line[7..].Split(" ").Select(long.Parse).ToList();
        }

        private static List<(long, long)> ParseSeedRanges(string line)
        {
            var seeds = new List<long>();
            long[] numbers = line[7..].Split(" ").Select(long.Parse).ToArray();
            return numbers.Chunk(2).Select(numbers => (numbers[0], numbers[1])).ToList();
        }

        private class Almanac
        {
            public Map SeedToSoilMap { get; }
            public Map SoilToFertilizerMap { get; }
            public Map FertilizerToWaterMap { get; }
            public Map WaterToLightMap { get; }
            public Map LightToTemperatureMap { get; }
            public Map TemperatureToHumidityMap { get; }
            public Map HumidityToLocationMap { get; }


            public Almanac(string[] lines)
            {
                int seedToSoilLineIndex = Array.IndexOf(lines, "seed-to-soil map:");
                int soilToFertilizerLineIndex = Array.IndexOf(lines, "soil-to-fertilizer map:");
                int fertilizerToWaterLineIndex = Array.IndexOf(lines, "fertilizer-to-water map:");
                int waterToLightLineIndex = Array.IndexOf(lines, "water-to-light map:");
                int lightToTemperatureLineIndex = Array.IndexOf(lines, "light-to-temperature map:");
                int temperatureToHumidityLineIndex = Array.IndexOf(lines, "temperature-to-humidity map:");
                int humidityToLocationLineIndex = Array.IndexOf(lines, "humidity-to-location map:");

                SeedToSoilMap = new Map(lines[(seedToSoilLineIndex + 1)..(soilToFertilizerLineIndex - 1)]);
                SoilToFertilizerMap = new Map(lines[(soilToFertilizerLineIndex + 1)..(fertilizerToWaterLineIndex - 1)]);
                FertilizerToWaterMap = new Map(lines[(fertilizerToWaterLineIndex + 1)..(waterToLightLineIndex - 1)]);
                WaterToLightMap = new Map(lines[(waterToLightLineIndex + 1)..(lightToTemperatureLineIndex - 1)]);
                LightToTemperatureMap = new Map(lines[(lightToTemperatureLineIndex + 1)..(temperatureToHumidityLineIndex - 1)]);
                TemperatureToHumidityMap = new Map(lines[(temperatureToHumidityLineIndex + 1)..(humidityToLocationLineIndex - 1)]);
                HumidityToLocationMap = new Map(lines[(humidityToLocationLineIndex + 1)..]);
            }

            public long LookUpSeedLocation(long seed)
            {
                long soil = SeedToSoilMap.Lookup(seed);
                long fertilizer = SoilToFertilizerMap.Lookup(soil);
                long water = FertilizerToWaterMap.Lookup(fertilizer);
                long light = WaterToLightMap.Lookup(water);
                long temperature = LightToTemperatureMap.Lookup(light);
                long humidity = TemperatureToHumidityMap.Lookup(temperature);
                long location = HumidityToLocationMap.Lookup(humidity);
                return location;
            }

            public long LookupLocationSeed(long location)
            {
                long humidity = HumidityToLocationMap.ReverseLookup(location);
                long temperature = TemperatureToHumidityMap.ReverseLookup(humidity);
                long light = LightToTemperatureMap.ReverseLookup(temperature);
                long water = WaterToLightMap.ReverseLookup(light);
                long fertilizer = FertilizerToWaterMap.ReverseLookup(water);
                long soil = SoilToFertilizerMap.ReverseLookup(fertilizer);
                long seed = SeedToSoilMap.ReverseLookup(soil);
                return seed;
            }
        }

        private class Map(string[] input)
        {
            public Mapping[] Maps { get; } = input.Select(line => new Mapping(line)).ToArray();

            public long Lookup(long source)
            {
                foreach (var map in Maps)
                {
                    if (source >= map.SourceStart && source <= map.SourceEnd)
                    {
                        return source + map.Offset;
                    }
                }

                return source;
            }

            public long ReverseLookup(long target)
            {
                foreach (var map in Maps)
                {
                    if (target >= map.SourceStart + map.Offset && target <= map.SourceEnd + map.Offset)
                    {
                        return target - map.Offset;
                    }
                }

                return target;
            }
        }

        private class Mapping
        {
            public Mapping(string input)
            {
                string[] parts = input.Split(" ");
                SourceStart = long.Parse(parts[1]);
                SourceEnd = SourceStart + long.Parse(parts[2]) - 1;
                Offset = long.Parse(parts[0]) - SourceStart;
            }

            public long SourceStart { get; }
            public long SourceEnd { get; }
            public long Offset { get; }
        }
    }
}
