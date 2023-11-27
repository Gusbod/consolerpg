using System.Numerics;

abstract class GameEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public CharInfo CharInfo { get; set; }
    public Vector2 Position { get; set; }
    public virtual bool IsBlocking { get; set; } = true;
    public virtual bool IsVisible { get; set; } = true;

    protected IWorldInteraction world;
    public IWorldInteraction World => world;

    public int Weight { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; }
    public int Strength { get; set; }

    public GameEntity(IWorldInteraction world)
    {
        this.world = world;
    }

    public virtual void Update()
    {
        // Default implementation
    }

    public virtual OnCollisionResult OnCollision(Player player)
    {
        return new OnCollisionResult("Bump!");
    }
}

class OnCollisionResult
{
    public string Message { get; private set; } = "";

    public OnCollisionResult(string message = "")
    {
        Message = message;
    }
}

interface IAttacker
{
    string Attack(IAttackable target);
}

interface IAttackable
{
    string OnAttacked(int damage);
}