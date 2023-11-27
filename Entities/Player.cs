// using System.Numerics;

// class Player : GameEntity//, IAttacker, IAttackable
// {
//     public int Gold { get; private set; }
//     public int XP { get; private set; }
//     public int Level => XP / 10 + 1;

//     public Player(int x, int y, IWorldInteraction world) : base(x, y, world)
//     {
//         Name = "Alf";
//         CharInfo = new CharInfo('@', ConsoleColor.White);
//     }

//     // public void AddGold(int amount)
//     // {
//     //     Gold += amount;
//     // }

//     // public bool TryRemoveGold(int amount)
//     // {
//     //     if (Gold >= amount)
//     //     {
//     //         Gold -= amount;
//     //         return true;
//     //     }
//     //     else
//     //     {
//     //         return false;
//     //     }
//     // }

//     // public string Attack(IAttackable target)
//     // {
//     //     // Chance to hit based on level
//     //     int chanceToHit = 50 + Level * 10;
//     //     if (new Random().Next(0, 100) < chanceToHit)
//     //     {
//     //         string result = target.OnAttacked(Strength);
//     //         XP += 1;
//     //         return "You attack: " + Strength + " damage. " + result;
//     //     }
//     //     else
//     //     {
//     //         return "You missed!";
//     //     }
//     // }

//     // public string OnAttacked(int damage)
//     // {
//     //     Health -= damage;
//     //     return "";
//     // }
// }