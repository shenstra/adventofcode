using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2015
{
    class Day7
    {
        public void Problem1()
        {
            var rules = new Queue<string>(Input.GetLines(2015, 7));
            var wireValues = new Dictionary<string, ushort>();
            ApplyRules(rules, wireValues);
            Console.WriteLine(wireValues["a"]);
        }

        public void Problem2()
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
                var rule = rules.Dequeue();
                var parts = rule.Split(" -> ");
                var left = parts[0];
                var right = parts[1];
                if (TryComputeValue(left, wireValues, out ushort value))
                    wireValues[right] = value;
                else
                    rules.Enqueue(rule);
            }
        }

        private static bool TryComputeValue(string left, Dictionary<string, ushort> wireValues, out ushort value)
        {
            if (left.Contains("AND"))
            {
                var inputs = left.Split(" AND ");
                if (TryLookupValue(inputs[0], wireValues, out ushort input1)
                    && TryLookupValue(inputs[1], wireValues, out ushort input2))
                {
                    value = (ushort)(input1 & input2);
                    return true;
                }
            }
            else if (left.Contains("OR"))
            {
                var inputs = left.Split(" OR ");
                if (TryLookupValue(inputs[0], wireValues, out ushort input1)
                    && TryLookupValue(inputs[1], wireValues, out ushort input2))
                {
                    value = (ushort)(input1 | input2);
                    return true;
                }
            }
            else if (left.Contains("LSHIFT"))
            {
                var inputs = left.Split(" LSHIFT ");
                if (TryLookupValue(inputs[0], wireValues, out ushort input1)
                    && TryLookupValue(inputs[1], wireValues, out ushort input2))
                {
                    value = (ushort)(input1 << input2);
                    return true;
                }
            }
            else if (left.Contains("RSHIFT"))
            {
                var inputs = left.Split(" RSHIFT ");
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
                    value = (ushort)(~input);
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
