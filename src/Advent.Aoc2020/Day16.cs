using System.Text.RegularExpressions;
using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day16
    {
        public void Part1()
        {
            var document = Document.Parse(Input.GetLines(2020, 16).ToList());
            var possibleValues = document.FieldRules.Values.SelectMany(i => i).ToList();
            var invalidValues = document.OtherTickets.SelectMany(ticket => ticket.Where(value => !possibleValues.Contains(value)));
            Console.WriteLine(invalidValues.Sum());
        }

        public void Part2()
        {
            var document = Document.Parse(Input.GetLines(2020, 16).ToList());
            var possibleValues = document.FieldRules.Values.SelectMany(i => i).ToList();
            var validTickets = document.OtherTickets.Where(ticket => ticket.All(value => possibleValues.Contains(value))).ToList();

            var possibleIndexesForField = GetPossibleIndexesPerField(document.FieldRules, validTickets);
            var fieldIndexes = SimpleSudoku(possibleIndexesForField);

            var myTicketValues = fieldIndexes.Where(fi => fi.Key.StartsWith("departure")).Select(fi => (long)document.MyTicket[fi.Value]);
            Console.WriteLine(myTicketValues.Aggregate((a, b) => a * b));
        }

        private static Dictionary<string, List<int>> GetPossibleIndexesPerField(Dictionary<string, List<int>> fieldRules, List<int[]> validTickets)
        {
            var possibleIndexesForField = new Dictionary<string, List<int>>();
            foreach (string field in fieldRules.Keys)
            {
                possibleIndexesForField[field] = new List<int>();
                for (int fieldIndex = 0; fieldIndex < validTickets.First().Length; fieldIndex++)
                {
                    if (validTickets.All(t => fieldRules[field].Contains(t[fieldIndex])))
                    {
                        possibleIndexesForField[field].Add(fieldIndex);
                    }
                }
            }

            return possibleIndexesForField;
        }

        private static Dictionary<string, int> SimpleSudoku(Dictionary<string, List<int>> possibleIndexesForField)
        {
            var fieldIndexes = new Dictionary<string, int>();
            while (possibleIndexesForField.Any())
            {
                foreach (string field in possibleIndexesForField.Keys)
                {
                    if (possibleIndexesForField[field].Count == 1)
                    {
                        int index = possibleIndexesForField[field].Single();
                        fieldIndexes[field] = index;
                        foreach (var p in possibleIndexesForField)
                        {
                            p.Value.Remove(index);
                        }

                        possibleIndexesForField.Remove(field);
                    }
                }

                var distinctIndexes = possibleIndexesForField.SelectMany(p => p.Value).Distinct().Except(fieldIndexes.Values);
                foreach (int index in distinctIndexes)
                {
                    var possibleFields = possibleIndexesForField.Where(p => p.Value.Contains(index)).Select(p => p.Key);
                    if (possibleFields.Count() == 1)
                    {
                        fieldIndexes[possibleFields.Single()] = index;
                        possibleIndexesForField.Remove(possibleFields.Single());
                    }
                }
            }

            return fieldIndexes;
        }

        private class Document
        {
            public Dictionary<string, List<int>> FieldRules { get; init; }
            public int[] MyTicket { get; init; }
            public List<int[]> OtherTickets { get; init; }

            public static Document Parse(List<string> lines)
            {
                int break1 = lines.IndexOf(string.Empty);
                int break2 = lines.IndexOf(string.Empty, break1 + 1);
                return new Document
                {
                    FieldRules = GetFieldRules(lines.Take(break1)),
                    MyTicket = lines.Skip(break1 + 2).First().SplitToInts(),
                    OtherTickets = lines.Skip(break2 + 2).Select(s => s.SplitToInts()).ToList()
                };
            }

            private static Dictionary<string, List<int>> GetFieldRules(IEnumerable<string> lines)
            {
                var fieldRegex = new Regex(@"^(?<field>.+): (?:(?<range>\d+-\d+)(?: or |$))+");
                return lines.Select(l => fieldRegex.Match(l)).ToDictionary(fm => fm.Groups["field"].Value, fm => GetRangeNumbers(fm));
            }

            private static List<int> GetRangeNumbers(Match fieldMatch)
            {
                return fieldMatch.Groups["range"].Captures.SelectMany(range =>
                {
                    var rangeEnds = range.Value.Split('-').Select(int.Parse).ToList();
                    return Enumerable.Range(rangeEnds[0], rangeEnds[1] - rangeEnds[0] + 1);
                }).ToList();
            }
        }
    }
}
