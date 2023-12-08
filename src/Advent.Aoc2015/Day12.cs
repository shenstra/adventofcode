using System.Text.Json;

namespace Advent.Aoc2015
{
    public class Day12(IInput input)
    {
        public void Part1()
        {
            string line = input.GetSingleLine();
            var doc = JsonDocument.Parse(line);
            Console.WriteLine(SumNumbers(doc.RootElement, (e) => false));
        }

        public void Part2()
        {
            string line = input.GetSingleLine();
            var doc = JsonDocument.Parse(line);
            Console.WriteLine(SumNumbers(doc.RootElement, HasRedProperty));
        }

        private long SumNumbers(JsonElement element, Func<JsonElement, bool> shouldSkipObject)
        {
            return element.ValueKind switch
            {
                JsonValueKind.Undefined => 0,
                JsonValueKind.Object => shouldSkipObject(element) ? 0 : element.EnumerateObject().Sum(p => SumNumbers(p.Value, shouldSkipObject)),
                JsonValueKind.Array => element.EnumerateArray().Sum(e => SumNumbers(e, shouldSkipObject)),
                JsonValueKind.String => long.TryParse(element.GetString(), out long l) ? l : 0,
                JsonValueKind.Number => element.GetInt64(),
                JsonValueKind.True => 0,
                JsonValueKind.False => 0,
                JsonValueKind.Null => 0,
                _ => 0
            };
        }

        private static bool HasRedProperty(JsonElement element)
        {
            return element.EnumerateObject().Any(p => p.Value.ValueKind == JsonValueKind.String && p.Value.GetString() == "red");
        }
    }
}