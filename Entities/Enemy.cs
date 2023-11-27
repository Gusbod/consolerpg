// using System.Numerics;

// class Enemy : GameEntity//, IAttacker, IAttackable
// {
//     public override bool IsBlocking => Health > 0;

//     public Enemy(int x, int y, IWorldInteraction world) : base(x, y, world)
//     {
//         Position = new Vector2(x, y);
//         DefaultAction = new Attack();
//         Name = "Goblin";
//         CharInfo = new CharInfo('g', ConsoleColor.Red);
//         Health = 2;
//         Strength = 1;
//     }
// }