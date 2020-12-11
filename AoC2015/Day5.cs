﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.AoC2015
{
    class Day5
    {
        private List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };

        public void Problem1()
        {
            var names = Input.GetLines(2015, 5);
            var niceNames = names.Where(IsNice);
            Console.WriteLine(niceNames.Count());
        }

        public void Problem2()
        {
            var names = Input.GetLines(2015, 5);
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
            return name.Count(c => vowels.Contains(c)) >= 3;
        }

        private bool HasDoubleLetter(string name)
        {
            for (var i = 0; i < name.Length - 1; i++)
                if (name[i] == name[i + 1])
                    return true;
            return false;
        }

        private bool ContainsNaughtSequence(string name)
        {
            return name.Contains("ab")
                || name.Contains("cd")
                || name.Contains("pq")
                || name.Contains("xy");
        }
        private bool HasRepeatingPair(string name)
        {
            for (var i = 0; i < name.Length - 3; i++)
                for (var j = i + 2; j < name.Length - 1; j++)
                    if (name[i] == name[j] && name[i + 1] == name[j + 1])
                        return true;
            return false;
        }

        private bool HasRepeatAfterOneLetter(string name)
        {
            for (var i = 0; i < name.Length - 2; i++)
                if (name[i] == name[i + 2])
                    return true;
            return false;
        }

    }
}
