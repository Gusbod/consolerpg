using System.Numerics;

class WorldEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public TileInfo CharInfo { get; set; }

    public Vector2 Position { get; set; }
    public float subPositionX = 0; //when -1 should move left, when 1 should move right
    public float subPositionY = 0; //when -1 should move up, when 1 should move down

    public virtual bool IsBlocking { get; set; } = true; //True means other entities cant occupy the same space (which is buggy anyways..)
    public virtual bool IsVisible { get; set; } = true; //Used for traps and ghosts and stuff maybe?

    protected IWorldInteraction world;
    public IWorldInteraction World => world;

    public IAction OnCollideAction { get; set; } = new Examine(); //What action will the entity perform if trying to move into the same space as another entity?
    public IAction OnUpdateAction { get; set; } = new NoAction(); //What action will the entity perform on each update tick?
    public IAction MoveAction { get; set; } = new Move(); //What action will the entity perform when trying to move?

    public int Weight { get; protected set; }

    private int health = 100;
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

    public WorldEntity(IWorldInteraction world)
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

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Health = 0;
        IsBlocking = false;
        CharInfo = new TileInfo('×', ConsoleColor.DarkRed);
        OnCollideAction = new NoAction();
        OnUpdateAction = new NoAction();
    }

    public ActionResult Update()
    {
        return OnUpdateAction.Execute(this, Position);
    }
}