using System.Numerics;

class Tree : GameEntity
{
    public Tree(int x, int y, IWorldInteraction world) : base(world)
    {
        Position = new Vector2(x, y);
        CharInfo = new CharInfo('T', ConsoleColor.Green);
    }

    public override OnCollisionResult OnCollision(Player player)
    {
        return new OnCollisionResult("You bump into a tree.");
    }
}