namespace Advent.Aoc2015
{
    public class Day21(IInput input)
    {
        public void Part1()
        {
            var boss = new Character("boss", GetBossStats(input.GetLines()));
            var player = new Character("player", (100, 0, 0));

            int lowestCost = int.MaxValue;
            foreach (var equipmentSet in EnumerateEquipmentSets())
            {
                player.Equip(equipmentSet);
                if (DefeatBoss(player, boss))
                {
                    lowestCost = Math.Min(lowestCost, equipmentSet.Sum(i => i.Cost));
                }
            }

            Console.WriteLine(lowestCost);
        }

        public void Part2()
        {
            var boss = new Character("boss", GetBossStats(input.GetLines()));
            var player = new Character("player", (100, 0, 0));

            int highestCost = 0;
            foreach (var equipmentSet in EnumerateEquipmentSets())
            {
                player.Equip(equipmentSet);
                if (!DefeatBoss(player, boss))
                {
                    highestCost = Math.Max(highestCost, equipmentSet.Sum(i => i.Cost));
                }
            }

            Console.WriteLine(highestCost);
        }

        private static (int hitPoints, int damage, int armor) GetBossStats(IEnumerable<string> lines)
        {
            int hitPoints = int.Parse(lines.ElementAt(0).Split(": ")[1]);
            int damage = int.Parse(lines.ElementAt(1).Split(": ")[1]);
            int armor = int.Parse(lines.ElementAt(2).Split(": ")[1]);
            return (hitPoints, damage, armor);
        }

        private static IEnumerable<Item[]> EnumerateEquipmentSets()
        {
            var (weapons, armors, rings) = GetItems();
            foreach (var weapon in weapons)
            {
                foreach (var armor in armors)
                {
                    foreach (var ring1 in rings)
                    {
                        foreach (var ring2 in rings.Where(r => r.Name == "None" || string.Compare(r.Name, ring1.Name) > 0))
                        {
                            yield return new Item[] { weapon, armor, ring1, ring2 };
                        }
                    }
                }
            }
        }

        private static (Item[] weapons, Item[] armors, Item[] rings) GetItems()
        {
            var weapons = new Item[] {
                new("Dagger", 8, 4, 0),
                new("Shortsword", 10, 5, 0),
                new("Warhammer", 25, 6, 0),
                new("Longsword", 40, 7, 0),
                new("Greataxe", 74, 8, 0),
            };
            var armors = new Item[] {
                new("None", 0, 0, 0),
                new("Leather", 13, 0, 1),
                new("Chainmail", 31, 0, 2),
                new("Splintmail", 53, 0, 3),
                new("Bandedmail", 75, 0, 4),
                new("Platemail", 102, 0, 5),
            };
            var rings = new Item[]
            {
                new("None", 0, 0, 0),
                new("Damage + 1", 25, 1, 0),
                new("Damage + 2", 50, 2, 0),
                new("Damage + 3", 100, 3, 0),
                new("Defense + 1", 20, 0, 1),
                new("Defense + 2", 40, 0, 2),
                new("Defense + 3", 80, 0, 3),
            };
            return (weapons, armors, rings);
        }

        private static bool DefeatBoss(Character player, Character boss)
        {
            player.Heal();
            boss.Heal();
            while (player.HitPoints > 0)
            {
                player.Attack(boss);
                if (boss.HitPoints <= 0)
                {
                    return true;
                }

                boss.Attack(player);
            }

            return false;
        }

        private class Character(string name, (int hitPoints, int damage, int armor) stats)
        {
            public string Name { get; private init; } = name;
            public int MaxHitPoints { get; private init; } = stats.hitPoints;
            public int HitPoints { get; private set; } = stats.hitPoints;
            public int Damage { get; private set; } = stats.damage;
            public int Armor { get; private set; } = stats.armor;

            public void Attack(Character target)
            {
                target.HitPoints -= Math.Max(Damage - target.Armor, 1);
            }

            public void Heal()
            {
                HitPoints = MaxHitPoints;
            }

            public void Equip(Item[] equipmentSet)
            {
                Damage = equipmentSet.Sum(i => i.Damage);
                Armor = equipmentSet.Sum(i => i.Armor);
            }
        }

        private record Item(string Name, int Cost, int Damage, int Armor);
    }
}
