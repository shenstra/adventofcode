﻿namespace Advent.Aoc2015
{
    public class Day05(IInput input)
    {
        private readonly List<char> vowels = ['a', 'e', 'i', 'o', 'u'];

        public void Part1()
        {
            var names = input.GetLines();
            var niceNames = names.Where(IsNice);
            Console.WriteLine(niceNames.Count());
        }

        public void Part2()
        {
            var names = input.GetLines();
            var niceNames = names.Where(IsNicer);
            Console.WriteLine(niceNames.Count());
        }

        private bool IsNice(string name)
        {
            return HasThreeVowels(name)
                && HasDoubleLetter(name)
                && !ContainsNaughtSequence(name);
        }

        private bool IsNicer(string name)
        {
            return HasRepeatingPair(name)
                && HasRepeatAfterOneLetter(name);
        }

        private bool HasThreeVowels(string name)
        {
            return name.Count(vowels.Contains) >= 3;
        }

        private static bool HasDoubleLetter(string name)
        {
            for (int i = 0; i < name.Length - 1; i++)
            {
                if (name[i] == name[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsNaughtSequence(string name)
        {
            return name.Contains("ab")
                || name.Contains("cd")
                || name.Contains("pq")
                || name.Contains("xy");
        }

        private static bool HasRepeatingPair(string name)
        {
            for (int i = 0; i < name.Length - 3; i++)
            {
                for (int j = i + 2; j < name.Length - 1; j++)
                {
                    if (name[i] == name[j] && name[i + 1] == name[j + 1])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool HasRepeatAfterOneLetter(string name)
        {
            for (int i = 0; i < name.Length - 2; i++)
            {
                if (name[i] == name[i + 2])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
