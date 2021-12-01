using Advent.Util;

namespace Advent.Aoc2015
{
    public class Day22
    {
        public void Part1()
        {
            var boss = new Boss(Input.GetLines(2015, 22));
            var player = new Player { HitPoints = 50, Mana = 500 };
            Console.WriteLine(CalculateLowestManaToWin(player, boss));
        }

        public void Part2()
        {
            var boss = new Boss(Input.GetLines(2015, 22));
            var player = new Player { HitPoints = 50, Mana = 500, Hard = true };
            Console.WriteLine(CalculateLowestManaToWin(player, boss));
        }

        private static int CalculateLowestManaToWin(Player player, Boss boss)
        {
            int lowestManaCostToWin = int.MaxValue;
            List<(Player player, Boss boss)> saveStates = new List<(Player, Boss)> { (player, boss) };
            while (saveStates.Any())
            {
                var state = saveStates.OrderBy(s => s.boss.HitPoints).First();
                saveStates.Remove(state);

                var newSaveStates = PossiblePlayerTurns(state).Where(s => s.player.ManaSpent < lowestManaCostToWin);

                var winningStates = newSaveStates.Where(s => s.boss.HitPoints <= 0);
                if (winningStates.Any())
                {
                    var winningState = winningStates.OrderBy(s => s.player.ManaSpent).First();
                    if (winningState.player.ManaSpent < lowestManaCostToWin)
                    {
                        lowestManaCostToWin = winningState.player.ManaSpent;
                    }
                }
                else
                {
                    saveStates.AddRange(newSaveStates);
                }
            }
            return lowestManaCostToWin;
        }

        private static IEnumerable<(Player player, Boss boss)> PossiblePlayerTurns((Player player, Boss boss) state)
        {
            state = ProcEffects(state, true);
            if (state.boss.HitPoints <= 0)
            {
                yield return state;
            }
            else
            {
                if (state.player.Mana >= 53)
                {
                    var result = BossTurn(CastMagicMissile(state));
                    if (result.player.HitPoints > 0)
                    {
                        yield return result;
                    }
                }

                if (state.player.Mana >= 73)
                {
                    var result = BossTurn(CastDrain(state));
                    if (result.player.HitPoints > 0)
                    {
                        yield return result;
                    }
                }

                if (state.player.Mana >= 113 && state.player.ShieldTimer == 0)
                {
                    var result = BossTurn(CastShield(state));
                    if (result.player.HitPoints > 0)
                    {
                        yield return result;
                    }
                }

                if (state.player.Mana >= 173 && state.boss.PoisonTimer == 0)
                {
                    var result = BossTurn(CastPoison(state));
                    if (result.player.HitPoints > 0)
                    {
                        yield return result;
                    }
                }

                if (state.player.Mana >= 229 && state.player.RechargeTimer == 0)
                {
                    var result = BossTurn(CastRecharge(state));
                    if (result.player.HitPoints > 0)
                    {
                        yield return result;
                    }
                }
            }
        }

        private static (Player player, Boss boss) BossTurn((Player player, Boss boss) state)
        {
            state = ProcEffects(state, false);
            if (state.player.HitPoints > 0 && state.boss.HitPoints > 0)
            {
                state = BossAttack(state);
            }

            return state;
        }

        private static (Player player, Boss boss) CastMagicMissile((Player player, Boss boss) state)
        {
            var newPlayer = state.player with
            {
                Mana = state.player.Mana - 53,
                ManaSpent = state.player.ManaSpent + 53,
            };
            var newBoss = state.boss with { HitPoints = state.boss.HitPoints - 4 };
            return (newPlayer, newBoss);
        }

        private static (Player player, Boss boss) CastDrain((Player player, Boss boss) state)
        {
            var newPlayer = state.player with
            {
                Mana = state.player.Mana - 73,
                ManaSpent = state.player.ManaSpent + 73,
                HitPoints = state.player.HitPoints + 2,
            };
            var newBoss = state.boss with { HitPoints = state.boss.HitPoints - 2 };
            return (newPlayer, newBoss);
        }

        private static (Player player, Boss boss) CastShield((Player player, Boss boss) state)
        {
            var newPlayer = state.player with
            {
                Mana = state.player.Mana - 113,
                ManaSpent = state.player.ManaSpent + 113,
                ShieldTimer = state.player.ShieldTimer + 6,
            };
            return (newPlayer, state.boss);
        }

        private static (Player player, Boss boss) CastPoison((Player player, Boss boss) state)
        {
            var newPlayer = state.player with
            {
                Mana = state.player.Mana - 173,
                ManaSpent = state.player.ManaSpent + 173,
            };
            var newBoss = state.boss with { PoisonTimer = state.boss.PoisonTimer + 6 };
            return (newPlayer, newBoss);
        }

        private static (Player player, Boss boss) CastRecharge((Player player, Boss boss) state)
        {
            var newPlayer = state.player with
            {
                Mana = state.player.Mana - 229,
                ManaSpent = state.player.ManaSpent + 229,
                RechargeTimer = state.player.RechargeTimer + 5,
            };
            return (newPlayer, state.boss);
        }

        private static (Player player, Boss boss) BossAttack((Player player, Boss boss) state)
        {
            int hitDamage = Math.Max(state.boss.Damage - state.player.Armor, 1);
            var newPlayer = state.player with { HitPoints = state.player.HitPoints - hitDamage };
            return (newPlayer, state.boss);
        }

        private static (Player player, Boss boss) ProcEffects((Player player, Boss boss) state, bool playerTurn)
        {
            var (player, boss) = state;
            if (playerTurn && player.Hard)
            {
                player = player with { HitPoints = player.HitPoints - 1 };
            }

            if (player.ShieldTimer > 0)
            {
                player = player with { ShieldTimer = player.ShieldTimer - 1 };
            }

            if (player.RechargeTimer > 0)
            {
                player = player with
                {
                    RechargeTimer = player.RechargeTimer - 1,
                    Mana = player.Mana + 101,
                };
            }

            if (boss.PoisonTimer > 0)
            {
                boss = boss with
                {
                    HitPoints = boss.HitPoints - 3,
                    PoisonTimer = boss.PoisonTimer - 1,
                };
            }

            return (player, boss);
        }

        private record Player
        {
            public int HitPoints { get; init; }
            public int Mana { get; init; }
            public int ManaSpent { get; init; }
            public int ShieldTimer { get; init; }
            public int RechargeTimer { get; init; }
            public int Armor => ShieldTimer > 0 ? 7 : 0;
            public bool Hard { get; init; }
        }

        private record Boss
        {
            public int HitPoints { get; init; }
            public int Damage { get; init; }
            public int PoisonTimer { get; init; }

            public Boss(IEnumerable<string> input)
            {
                HitPoints = int.Parse(input.ElementAt(0).Split(": ")[1]);
                Damage = int.Parse(input.ElementAt(1).Split(": ")[1]);
            }
        }
    }
}
