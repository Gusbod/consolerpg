using System.Numerics;

abstract class GameEntity
{
    public int Id { get; set; }
    public Vector2 Position { get; set; }
    public char Symbol { get; set; }
    public virtual bool IsBlocking { get; set; }
    public virtual bool IsVisible { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; }
    public int Strength { get; set; }

    public abstract void Update();
}

interface IAttacker
{
    void Attack(IAttackable target);
}

interface IAttackable
{
    void OnAttacked(int damage);
}

class Player : GameEntity, IAttacker, IAttackable
{
    public int XP { get; set; }

    public Player(int x, int y)
    {
        Position = new Vector2(x, y);
        Symbol = '@';
        Health = 10;
        Strength = 1;
    }

    public void Attack(IAttackable target)
    {
        target.OnAttacked(Strength);
        XP++;
    }

    public void OnAttacked(int damage)
    {
        Health -= damage;
    }

    public override void Update()
    {
        // Update logic
    }

    public void Move(int x, int y)
    {
        Position += new Vector2(x, y);
    }
}

class Enemy : GameEntity, IAttacker, IAttackable
{
    public override bool IsBlocking => true;
    public override bool IsVisible => true;

    public Enemy(int x, int y)
    {
        Position = new Vector2(x, y);
        Symbol = 'E';
        Health = 5;
        Strength = 1;
    }

    public void Attack(IAttackable target)
    {
        target.OnAttacked(Strength);
    }

    public void OnAttacked(int damage)
    {
        Health -= damage;
    }

    public override void Update()
    {
        // Update logic
    }
}

class Tree : GameEntity
{
    public override bool IsBlocking => true;
    public override bool IsVisible => true;

    public Tree(int x, int y)
    {
        Position = new Vector2(x, y);
        Symbol = 'T';
    }

    public override void Update()
    {
        // Update logic
    }
}

class Rock : GameEntity
{
    public override bool IsBlocking => true;
    public override bool IsVisible => true;

    public Rock(int x, int y)
    {
        Position = new Vector2(x, y);
        //80% change to set symbol to 'o', 20% chance to set symbol to 'O'
        Symbol = (new Random().Next(0, 10) < 8) ? 'o' : 'O';
    }

    public override void Update()
    {
        // Update logic
    }
}