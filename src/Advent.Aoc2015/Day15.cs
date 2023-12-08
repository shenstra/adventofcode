namespace Advent.Aoc2015
{
    public class Day15(IInput input)
    {
        public void Part1()
        {
            var ingredients = input.GetLines().Select(l => new Ingredient(l)).ToArray();
            var bestRecipe = EnumerateRecipes(ingredients, 100).OrderByDescending(CalculateScore).First();
            Console.WriteLine(CalculateScore(bestRecipe));
        }

        public void Part2()
        {
            var ingredients = input.GetLines().Select(l => new Ingredient(l)).ToArray();
            var bestRecipe = EnumerateRecipes(ingredients, 100).Where(r => CalculateCalories(r) == 500)
                .OrderByDescending(CalculateScore).First();
            Console.WriteLine(CalculateScore(bestRecipe));
        }

        private static IEnumerable<(Ingredient, int)[]> EnumerateRecipes(Ingredient[] ingredients, int maxIngredients)
        {
            if (ingredients.Length == 1)
            {
                yield return new (Ingredient, int)[] { (ingredients.Single(), maxIngredients) };
            }
            else
            {
                var remainingIngredients = ingredients.Skip(1).ToArray();
                for (int amount = 0; amount <= maxIngredients; amount++)
                {
                    foreach (var recipe in EnumerateRecipes(remainingIngredients, maxIngredients - amount).ToList())
                    {
                        yield return recipe.Append((ingredients[0], amount)).ToArray();
                    }
                }
            }
        }

        private static long CalculateScore((Ingredient ingredient, int amount)[] recipe)
        {
            long capacity = Math.Max(recipe.Sum(entry => entry.ingredient.Capacity * entry.amount), 0);
            long durability = Math.Max(recipe.Sum(entry => entry.ingredient.Durability * entry.amount), 0);
            long flavor = Math.Max(recipe.Sum(entry => entry.ingredient.Flavor * entry.amount), 0);
            long texture = Math.Max(recipe.Sum(entry => entry.ingredient.Texture * entry.amount), 0);
            return capacity * durability * flavor * texture;
        }

        private long CalculateCalories((Ingredient ingredient, int amount)[] recipe)
        {
            return recipe.Sum(entry => entry.ingredient.Calories * entry.amount);
        }

        private class Ingredient
        {
            private readonly Regex ingredientRegex = new(@"^(.+): capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)");

            public Ingredient(string input)
            {
                var match = ingredientRegex.Match(input);
                Name = match.Groups[1].Value;
                Capacity = long.Parse(match.Groups[2].Value);
                Durability = long.Parse(match.Groups[3].Value);
                Flavor = long.Parse(match.Groups[4].Value);
                Texture = long.Parse(match.Groups[5].Value);
                Calories = long.Parse(match.Groups[6].Value);
            }

            public string Name { get; set; }
            public long Capacity { get; set; }
            public long Durability { get; set; }
            public long Flavor { get; set; }
            public long Texture { get; set; }
            public long Calories { get; set; }
        }
    }
}
