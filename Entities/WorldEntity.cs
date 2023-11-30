using System.Numerics;

class WorldEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public Tile Tile { get; set; }

    public virtual bool IsBlocking { get; set; } = true; //True means other entities cant occupy the same space (which is buggy anyways..)
    public virtual bool IsVisible { get; set; } = true; //Used for traps and ghosts and stuff maybe?

    private IWorldInteraction world;
    public IWorldInteraction World => world;

    public IAction OnCollideAction { get; set; } = new Examine(); //What action will the entity perform if trying to move into the same space as another entity?
    public IAction OnUpdateAction { get; set; } = new NoAction(); //What action will the entity perform on each update tick?
    public IAction MoveAction { get; set; } = new Move(); //What action will the entity perform when trying to move?

    public Vector2 Position { get; set; }
    public float subPositionX = 0; //when -1 should move left, when 1 should move right
    public float subPositionY = 0; //when -1 should move up, when 1 should move down
    public int Weight { get; set; }
    public int Size { get; set; } //How much space does this entity occupy in percent of a tile?

    private readonly Dictionary<string, int> attributes = new();
    public readonly List<StatusEffect> StatusEffects = new();
    private readonly List<Thing> inventory = new();

    public WorldEntity(IWorldInteraction world)
    {
        this.world = world;
        SetAttribute("weight", 1);
        SetAttribute("size", 1);
        SetAttribute("hitpoints", 1);
    }

    public ActionResult Update()
    {
        foreach (StatusEffect effect in StatusEffects)
        {
            effect.Apply(this);
        }
        return OnUpdateAction.Execute(this, Position);
    }

    public int GetAttribute(string name)
    {
        string lowerName = name.ToLower();
        return attributes.ContainsKey(lowerName) ? attributes[lowerName] : 0;
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
        int defense = GetAttribute("defense") + GetAttribute("sturdyness");
        attributes["hitpoints"] -= amount - defense;
        if (attributes["hitpoints"] <= 0) Die();
    }

    private void Die()
    {
        IsBlocking = false;
        Tile = new Tile('×', ConsoleColor.DarkRed);
        OnCollideAction = new NoAction();
        OnUpdateAction = new NoAction();
        MoveAction = new NoAction();
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        StatusEffects.Add(effect);
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        StatusEffects.Remove(effect);
    }
}

abstract class StatusEffect //fear, poison, pain, starvation, thirsting
{
    public string Name { get; set; } = "";
    public int TicksSinceAdded { get; set; } = 0;

    protected Action<WorldEntity> CalculateEffect = (entity) => { };
    protected Func<bool> CheckIfEffectExpired = () => false;

    public void Apply(WorldEntity entity)
    {
        TicksSinceAdded++;
        CalculateEffect.Invoke(entity);
        if (CheckIfEffectExpired.Invoke()) entity.RemoveStatusEffect(this);
    }
}

class Poison : StatusEffect
{
    public int Duration { get; set; } = 10;
    public int Strength { get; set; } = 1;

    public Poison()
    {
        Name = "Poison";
        CalculateEffect = (entity) => entity.TakeDamage(Strength);
        CheckIfEffectExpired = () => TicksSinceAdded >= Duration;
    }

}