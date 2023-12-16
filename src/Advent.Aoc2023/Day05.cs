using Advent.Util;

namespace Advent.Aoc2023
{
    public class Day05(IInput input)
    {
        public uint Part1()
        {
            string[] lines = input.GetLines().ToArray();
            var seeds = ParseSeeds(lines[0]);
            var almanac = new Almanac(lines);
            return seeds.Select(almanac.LookUpSeedLocation).Min();
        }

        public uint Part2()
        {
            string[] lines = input.GetLines().ToArray();
            var seedRanges = ParseSeedRanges(lines[0]);
            var almanac = new Almanac(lines);

            const uint parallellism = 12;
            const uint increments = 1_000_000;

            for (uint start = 0; ; start += increments)
            {
                var jobs = Enumerable.Range(0, (int)parallellism).Select(job =>
                    Task.Run(() => ReverseSearchJob(seedRanges, almanac, parallellism, start + (uint)job, start + increments))).ToArray();
                Task.WaitAll(jobs);
                uint result = jobs.Select(job => job.Result).Min();
                if (result < uint.MaxValue)
                {
                    return result;
                }
            }
        }

        private uint ReverseSearchJob(List<(uint, uint)> seedRanges, Almanac almanac, uint increment, uint start, uint max)
        {
            for (uint location = start; location < max; location += increment)
            {
                uint seed = almanac.LookupLocationSeed(location);
                if (IsInRanges(seed, seedRanges))
                {
                    return location;
                }
            }

            return uint.MaxValue;
        }

        private bool IsInRanges(uint seed, List<(uint, uint)> seedRanges)
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

        private static List<uint> ParseSeeds(string line)
        {
            return line[7..].Split(" ").Select(uint.Parse).ToList();
        }

        private static List<(uint, uint)> ParseSeedRanges(string line)
        {
            var seeds = new List<uint>();
            uint[] numbers = line[7..].Split(" ").Select(uint.Parse).ToArray();
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

            public uint LookUpSeedLocation(uint seed)
            {
                uint soil = SeedToSoilMap.Lookup(seed);
                uint fertilizer = SoilToFertilizerMap.Lookup(soil);
                uint water = FertilizerToWaterMap.Lookup(fertilizer);
                uint light = WaterToLightMap.Lookup(water);
                uint temperature = LightToTemperatureMap.Lookup(light);
                uint humidity = TemperatureToHumidityMap.Lookup(temperature);
                uint location = HumidityToLocationMap.Lookup(humidity);
                return location;
            }

            public uint LookupLocationSeed(uint location)
            {
                uint humidity = HumidityToLocationMap.ReverseLookup(location);
                uint temperature = TemperatureToHumidityMap.ReverseLookup(humidity);
                uint light = LightToTemperatureMap.ReverseLookup(temperature);
                uint water = WaterToLightMap.ReverseLookup(light);
                uint fertilizer = FertilizerToWaterMap.ReverseLookup(water);
                uint soil = SoilToFertilizerMap.ReverseLookup(fertilizer);
                uint seed = SeedToSoilMap.ReverseLookup(soil);
                return seed;
            }
        }

        private class Map(string[] input)
        {
            public Mapping[] Maps { get; } = input.Select(line => new Mapping(line)).ToArray();

            public uint Lookup(uint source)
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

            public uint ReverseLookup(uint target)
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
                SourceStart = uint.Parse(parts[1]);
                SourceEnd = SourceStart + uint.Parse(parts[2]) - 1;
                Offset = uint.Parse(parts[0]) - SourceStart;
            }

            public uint SourceStart { get; }
            public uint SourceEnd { get; }
            public uint Offset { get; }
        }
    }
}
