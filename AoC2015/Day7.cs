using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2015
{
    internal class Day7
    {
        public void Part1()
        {
            var rules = new Queue<string>(Input.GetLines(2015, 7));
            var wireValues = new Dictionary<string, ushort>();
            ApplyRules(rules, wireValues);
            Console.WriteLine(wireValues["a"]);
        }

        public void Part2()
        {
            var lines = Input.GetLines(2015, 7);
            var rules = new Queue<string>(lines);
            var wireValues = new Dictionary<string, ushort>();
            ApplyRules(rules, wireValues);
            ushort intermediateResult = wireValues["a"];

            rules = new Queue<string>(lines.Select(l => l.EndsWith("-> b") ? $"{intermediateResult} -> b" : l));
            wireValues.Clear();
            ApplyRules(rules, wireValues);
            Console.WriteLine(wireValues["a"]);
        }

        private static void ApplyRules(Queue<string> rules, Dictionary<string, ushort> wireValues)
        {
            while (rules.Any())
            {
                string rule = rules.Dequeue();
                string[] parts = rule.Split(" -> ");
                string left = parts[0];
                string right = parts[1];
                if (TryComputeValue(left, wireValues, out ushort value))
                {
                    wireValues[right] = value;
                }
                else
                {
                    rules.Enqueue(rule);
                }
            }
        }

        private static bool TryComputeValue(string left, Dictionary<string, ushort> wireValues, out ushort value)
        {
            if (left.Contains("AND"))
            {
                string[] inputs = left.Split(" AND ");
                if (TryLookupValue(inputs[0], wireValues, out ushort input1)
                    && TryLookupValue(inputs[1], wireValues, out ushort input2))
                {
                    value = (ushort)(input1 & input2);
                    return true;
                }
            }
            else if (left.Contains("OR"))
            {
                string[] inputs = left.Split(" OR ");
                if (TryLookupValue(inputs[0], wireValues, out ushort input1)
                    && TryLookupValue(inputs[1], wireValues, out ushort input2))
                {
                    value = (ushort)(input1 | input2);
                    return true;
                }
            }
            else if (left.Contains("LSHIFT"))
            {
                string[] inputs = left.Split(" LSHIFT ");
                if (TryLookupValue(inputs[0], wireValues, out ushort input1)
                    && TryLookupValue(inputs[1], wireValues, out ushort input2))
                {
                    value = (ushort)(input1 << input2);
                    return true;
                }
            }
            else if (left.Contains("RSHIFT"))
            {
                string[] inputs = left.Split(" RSHIFT ");
                if (TryLookupValue(inputs[0], wireValues, out ushort input1)
                    && TryLookupValue(inputs[1], wireValues, out ushort input2))
                {
                    value = (ushort)(input1 >> input2);
                    return true;
                }
            }
            else if (left.Contains("NOT"))
            {
                if (TryLookupValue(left[4..], wireValues, out ushort input))
                {
                    value = (ushort)~input;
                    return true;
                }
            }
            else
            {
                return TryLookupValue(left, wireValues, out value);
            }

            value = 0;
            return false;
        }

        private static bool TryLookupValue(string token, Dictionary<string, ushort> wireValues, out ushort output)
        {
            if (wireValues.ContainsKey(token))
            {
                output = wireValues[token];
                return true;
            }

            return ushort.TryParse(token, out output);
        }
    }
}
