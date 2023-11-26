using System.Numerics;

abstract class GameEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public Vector2 Position { get; set; }
    public CharInfo CharInfo { get; set; }
    public virtual bool IsBlocking { get; set; }
    public virtual bool IsVisible { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; }
    public int Strength { get; set; }

    public virtual void Update()
    {
        // Default implementation
    }

    public virtual OnCollisionResult OnCollision(Player player)
    {
        return new OnCollisionResult { Message = "Bumped into " + CharInfo.Symbol };
    }
}

class OnCollisionResult
{
    public string? Message { get; set; }
}

interface IAttacker
{
    string Attack(IAttackable target);
}

interface IAttackable
{
    string OnAttacked(int damage);
}

class Enemy : GameEntity, IAttacker, IAttackable
{
    public override bool IsBlocking => Health > 0;
    public override bool IsVisible => true;

    public Enemy(int x, int y)
    {
        Name = "Goblin";
        Position = new Vector2(x, y);
        CharInfo = new CharInfo('g', ConsoleColor.Red);
        Health = 5;
        Strength = 1;
    }

    public string Attack(IAttackable target)
    {
        //Chance to hit based on health
        int chanceToHit = 50 + Health * 10;
        if (new Random().Next(0, 100) < chanceToHit)
        {
            string result = target.OnAttacked(Strength);
            return $"{Name} attacked with {Strength} damage. {result}";
        }
        else
        {
            return "Missed!";
        }
    }

    public string OnAttacked(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            CharInfo = new CharInfo('x', ConsoleColor.DarkRed);
        }

        return "Enemy health: " + Health;
    }

    public override OnCollisionResult OnCollision(Player player)
    {
        string attackedResult = player.Attack(this);
        string attackResult = Attack(player);
        return new OnCollisionResult { Message = attackedResult + "\n" + attackResult };
    }
}

class Tree : GameEntity
{
    public override bool IsBlocking => true;
    public override bool IsVisible => true;

    public Tree(int x, int y)
    {
        Position = new Vector2(x, y);
        CharInfo = new CharInfo('T', ConsoleColor.Green);
    }
}

class Rock : GameEntity
{
    public override bool IsBlocking => true;
    public override bool IsVisible => true;

    public Rock(int x, int y)
    {
        char symbol = (new Random().Next(0, 10) < 8) ? 'o' : 'O';
        Position = new Vector2(x, y);
        //80% change to set symbol to 'o', 20% chance to set symbol to 'O'
        CharInfo = new CharInfo(symbol, ConsoleColor.Gray);
    }
}