namespace Advent.Aoc2020
{
    public class Day21
    {
        private readonly IInput input;

        public Day21(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var entries = input.GetLines().Select(ToEntry).ToList();
            FindAndRemoveIngredientAllergenes(entries);
            Console.WriteLine(entries.Sum(e => e.Ingredients.Count));
        }

        public void Part2()
        {
            var entries = input.GetLines().Select(ToEntry).ToList();
            var ingredientAllergenes = FindAndRemoveIngredientAllergenes(entries);
            Console.WriteLine(string.Join(",", ingredientAllergenes.OrderBy(kvp => kvp.Value).Select(kvp => kvp.Key)));
        }

        private static Dictionary<string, string> FindAndRemoveIngredientAllergenes(List<Entry> entries)
        {
            var ingredientAllergenes = new Dictionary<string, string>();

            while (entries.Any(e => e.Allergens.Any()))
            {
                (string ingredient, string allergen) = FindUniqueIngredientAllergen(entries);
                ingredientAllergenes[ingredient] = allergen;
                foreach (var entry in entries)
                {
                    entry.Ingredients.Remove(ingredient);
                    entry.Allergens.Remove(allergen);
                }
            }

            return ingredientAllergenes;
        }


        private static (string, string) FindUniqueIngredientAllergen(List<Entry> entries)
        {
            foreach (string allergen in entries.SelectMany(e => e.Allergens).Distinct())
            {
                var matchingEntries = entries.Where(e => e.Allergens.Contains(allergen));
                var possibleIngredients = matchingEntries.Select(e => e.Ingredients).Aggregate(ListIntersect).ToList();
                if (possibleIngredients.Count == 1)
                {
                    return (possibleIngredients.Single(), allergen);
                }
            }

            throw new ApplicationException("No more unique ingredient/allergen options found");
        }

        private static List<T> ListIntersect<T>(List<T> a, List<T> b) => a.Intersect(b).ToList();

        private static Entry ToEntry(string input)
        {
            string[] parts = input.TrimEnd(')').Split(" (contains ");
            return new Entry
            {
                Ingredients = parts[0].Split(" ").ToList(),
                Allergens = parts.Length > 1 ? parts[1].Split(", ").ToList() : new List<string>()
            };
        }

        private struct Entry
        {
            public List<string> Ingredients { get; set; }
            public List<string> Allergens { get; set; }
        }
    }
}