using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day23
    {
        public void Part1()
        {
            int[] numbers = Input.GetInts(2020, 23).ToArray();
            int cupCount = numbers.Length;
            var lookup = CreateLookup(numbers, cupCount);
            var current = lookup[numbers.First()];

            for (int i = 0; i < 100; i++)
            {
                current = PlayRound(current, lookup, cupCount);
            }

            Console.WriteLine(string.Join(string.Empty, GetValuesAfterOne(lookup, cupCount - 1)));
        }

        public void Part2()
        {
            var input = Input.GetInts(2020, 23);
            int cupCount = 1_000_000;
            int[] numbers = input.Concat(Enumerable.Range(input.Max() + 1, cupCount - input.Count())).ToArray();
            var lookup = CreateLookup(numbers, cupCount);
            var current = lookup[numbers.First()];

            for (int i = 0; i < 10_000_000; i++)
            {
                current = PlayRound(current, lookup, cupCount);
            }

            var nextCups = GetValuesAfterOne(lookup, 2).ToList();
            Console.WriteLine(Math.BigMul(nextCups[0], nextCups[1]));
        }

        private static Cup[] CreateLookup(int[] numbers, int cupCount)
        {
            var lookup = new Cup[cupCount + 1];
            for (int i = 0; i < cupCount; i++)
            {
                lookup[numbers[i]] = new Cup { Value = numbers[i] };
            }

            for (int i = 0; i < cupCount; i++)
            {
                lookup[numbers[i]].Next = lookup[numbers[(i + 1) % cupCount]];
            }

            return lookup;
        }

        private static Cup PlayRound(Cup current, Cup[] lookup, int cupCount)
        {
            var pickups = new Cup[3] { current.Next, current.Next.Next, current.Next.Next.Next };
            current.Next = current.Next.Next.Next.Next;

            var destination = lookup[PickDestination(current, cupCount, pickups)];
            pickups[2].Next = destination.Next;
            destination.Next = pickups[0];

            return current.Next;
        }

        private static int PickDestination(Cup current, int cupCount, Cup[] pickups)
        {
            int nextValue = current.Value > 1 ? current.Value - 1 : cupCount;
            while (pickups.Any(c => c.Value == nextValue))
            {
                nextValue--;
                if (nextValue == 0)
                {
                    nextValue = cupCount;
                }
            }

            return nextValue;
        }

        private IEnumerable<int> GetValuesAfterOne(Cup[] lookup, int count)
        {
            var cup = lookup[1];
            for (int i = 0; i < count; i++)
            {
                cup = cup.Next;
                yield return cup.Value;
            }
        }

        private class Cup
        {
            public int Value { get; set; }
            public Cup Next { get; set; }
        }
    }
}