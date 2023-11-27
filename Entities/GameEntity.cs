using System.Collections.Specialized;
using System.Numerics;

class GameEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public CharInfo CharInfo { get; set; }
    public Vector2 Position { get; set; }
    public virtual bool IsBlocking { get; set; } = true;
    public virtual bool IsVisible { get; set; } = true;

    protected IWorldInteraction world;
    public IWorldInteraction World => world;

    public IAction OnCollideAction { get; set; } = new Examine();
    public IAction OnUpdateAction { get; set; } = new NoAction();

    public int Weight { get; protected set; }

    private int health;
    public int Health
    {
        get => health;
        protected set
        {
            health = value;
            Math.Clamp(health, 0, 100);
        }
    }

    private readonly Dictionary<string, int> attributes = new();
    private readonly List<Thing> inventory = new();

    public GameEntity(IWorldInteraction world)
    {
        this.world = world;
    }

    public int GetAttribute(string name)
    {
        if (attributes.ContainsKey(name.ToLower()))
        {
            return attributes[name.ToLower()];
        }
        else
        {
            return 0;
        }
    }

    public void SetAttribute(string name, int value)
    {
        attributes[name.ToLower()] = value;
    }

    public void AddInventory(Thing thing)
    {
        inventory.Add(thing);
    }

    public void RemoveInventory(Thing thing)
    {
        inventory.Remove(thing);
    }

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void Die()
    {
        IsBlocking = false;
        CharInfo = new CharInfo('x', ConsoleColor.DarkRed);
    }

    public virtual void Update()
    {
        OnUpdateAction.Execute(this, Position);
    }
}