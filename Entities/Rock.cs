// using System.Numerics;

// class Rock : GameEntity
// {
//     public override bool IsBlocking => true;
//     public override bool IsVisible => true;

//     // private float xShift = 0;
//     // private float yShift = 0;

//     public Rock(int x, int y, IWorldInteraction world) : base(world)
//     {
//         DefaultAction = new Push();
//         Name = "Rock";
//         Position = new Vector2(x, y);
//         Weight = new Random().Next(25, 5000);

//         char symbol;

//         if (Weight < 500)
//         {
//             symbol = 'o';
//         }
//         else if (Weight < 2500)
//         {
//             symbol = 'O';
//         }
//         else
//         {
//             symbol = '0';
//         }

//         CharInfo = new CharInfo(symbol, ConsoleColor.Gray);
//     }

//     /*
//         public override OnCollisionResult OnCollision(GameEntity collider)
//         {
//             if (collider.Strength * 500 < Weight)
//             {
//                 return new OnCollisionResult("This rock is too heavy for you to move.");
//             }

//             // Calculate the direction from the entity to the player
//             Vector2 direction = collider.Position - Position;

//             // Adjust xShift or yShift based on direction and player's strength
//             if (Math.Abs(direction.X) >= Math.Abs(direction.Y))
//             {
//                 // Player is more aligned horizontally with the entity
//                 xShift += (direction.X > 0 ? -collider.Strength : collider.Strength) * 0.1f;
//             }
//             else
//             {
//                 // Player is more aligned vertically with the entity
//                 yShift += (direction.Y > 0 ? -collider.Strength : collider.Strength) * 0.1f;
//             }

//             string returnMessage = "You push on the rock and it moves slightly.";
//             // Check if the accumulated shift is enough to move the entity
//             if (Math.Abs(xShift) >= 1)
//             {
//                 bool moved = world.TryMoveEntity(this, Position + new Vector2(Math.Sign(xShift), 0));
//                 xShift %= 1; // Keep the fractional part of the shift
//                 returnMessage = moved ? "You push the rock." : "You push on the rock but something is preventing it to move.";
//             }
//             if (Math.Abs(yShift) >= 1)
//             {
//                 bool moved = world.TryMoveEntity(this, Position + new Vector2(0, Math.Sign(yShift)));
//                 yShift %= 1; // Keep the fractional part of the shift
//                 returnMessage = moved ? "You push the rock." : "You push on the rock but something is preventing it to move.";
//             }

//             return new OnCollisionResult(returnMessage);
//         }
//     */
// }