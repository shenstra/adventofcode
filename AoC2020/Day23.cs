using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2020
{
    internal class Day23
    {
        public void Part1()
        {
            var cups = new LinkedList<int>(Input.GetInts(2020, 23));
            int cupCount = cups.Count;
            var lookup = CreateLookup(cups, cupCount);
            var current = cups.First;
            for (int i = 0; i < 100; i++)
            {
                current = PlayRound(current, cups, lookup, cupCount);
            }

            Console.WriteLine(string.Join(string.Empty, GetValuesAfterOne(lookup, cupCount - 1)));
        }

        public void Part2()
        {
            var input = Input.GetInts(2020, 23);
            int cupCount = 1_000_000;
            var cups = new LinkedList<int>(input.Concat(Enumerable.Range(input.Max() + 1, cupCount - input.Count())));
            var lookup = CreateLookup(cups, cupCount);
            var current = cups.First;
            for (int i = 0; i < 10_000_000; i++)
            {
                current = PlayRound(current, cups, lookup, cupCount);
            }

            var nextCups = GetValuesAfterOne(lookup, 2).ToList();
            Console.WriteLine(Math.BigMul(nextCups[0], nextCups[1]));
        }

        private static LinkedListNode<int>[] CreateLookup(LinkedList<int> cups, int count)
        {
            var lookup = new LinkedListNode<int>[count + 1];
            for (var cup = cups.First; cup != null; cup = cup.Next)
            {
                lookup[cup.Value] = cup;
            }

            return lookup;
        }

        private static LinkedListNode<int> PlayRound(LinkedListNode<int> current, LinkedList<int> cups, LinkedListNode<int>[] lookup, int max)
        {
            int[] pickups = PickUpNextCups(current, 3);
            var destination = lookup[PickDestination(current, pickups, max)];
            lookup[pickups[0]] = cups.AddAfter(destination, pickups[0]);
            lookup[pickups[1]] = cups.AddAfter(destination.Next, pickups[1]);
            lookup[pickups[2]] = cups.AddAfter(destination.Next.Next, pickups[2]);
            current = current.Next ?? cups.First;
            return current;
        }

        private static int[] PickUpNextCups(LinkedListNode<int> current, int count)
        {
            int[] result = new int[count];
            for (int i = 0; i < count; i++)
            {
                var next = current.Next ?? current.List.First;
                result[i] = next.Value;
                current.List.Remove(next);
            }

            return result;
        }

        private static int PickDestination(LinkedListNode<int> current, int[] pickups, int max)
        {
            int destinationValue = current.Value == 1 ? max : current.Value - 1;
            while (pickups.Contains(destinationValue))
            {
                destinationValue--;
                if (destinationValue < 1)
                {
                    destinationValue = max;
                }
            }

            return destinationValue;
        }

        private static IEnumerable<int> GetValuesAfterOne(LinkedListNode<int>[] lookup, int count)
        {
            var node = lookup[1];
            for (int i = 0; i < count; i++)
            {
                node = node.Next ?? node.List.First;
                yield return node.Value;
            }
        }
    }
}