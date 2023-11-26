using System.Numerics;

class Rock : GameEntity
{
    public override bool IsBlocking => true;
    public override bool IsVisible => true;

    private float xShift = 0;
    private float yShift = 0;

    public Rock(int x, int y, IWorldInteraction world) : base(world)
    {
        char symbol = (new Random().Next(0, 10) < 8) ? 'o' : 'O';
        Position = new Vector2(x, y);
        //80% change to set symbol to 'o', 20% chance to set symbol to 'O'
        CharInfo = new CharInfo(symbol, ConsoleColor.Gray);
    }

    public override OnCollisionResult OnCollision(Player player)
    {
        //check what direction the player is coming from
        Vector2 direction = player.Position - Position;
        //adust xShift or yShift based on direction
        if (direction.X > 0)
        {
            xShift += player.Strength * 0.1f;
        }
        else if (direction.X < 0)
        {
            xShift -= player.Strength * 0.1f;
        }
        else if (direction.Y > 0)
        {
            yShift += player.Strength * 0.1f;
        }
        else if (direction.Y < 0)
        {
            yShift -= player.Strength * 0.1f;
        }

        if (Math.Abs(xShift) > 1)
        {
            world.TryMoveEntity(this, new Vector2((int)xShift, 0));
            xShift = 0;
        }

        if (Math.Abs(yShift) > 1)
        {
            world.TryMoveEntity(this, new Vector2(0, (int)yShift));
            yShift = 0;
        }

        return new OnCollisionResult($"xShift: {xShift}. yShift: {yShift}.");
    }
}