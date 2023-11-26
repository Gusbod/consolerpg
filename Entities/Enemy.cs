using System.Numerics;

class Enemy : GameEntity, IAttacker, IAttackable
{
    public override bool IsBlocking => Health > 0;
    public override bool IsVisible => true;

    public Enemy(int x, int y, IWorldInteraction world) : base(world)
    {
        Name = "Goblin";
        Position = new Vector2(x, y);
        CharInfo = new CharInfo('g', ConsoleColor.Red);
        Health = 2;
        Strength = 1;
    }

    public string Attack(IAttackable target)
    {
        //Chance to hit based on health
        int chanceToHit = 50 + Health * 10;
        if (new Random().Next(0, 100) < chanceToHit)
        {
            string result = target.OnAttacked(Strength);
            return $"{Name} attack: {Strength} damage. {result}";
        }
        else
        {
            return $"{Name} attack: Missed!";
        }
    }

    public string OnAttacked(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            CharInfo = new CharInfo('x', ConsoleColor.DarkRed);
        }

        return $"{Name} health: " + Health;
    }

    public override OnCollisionResult OnCollision(Player player)
    {
        string attackedResult = player.Attack(this);
        string attackResult = Attack(player);
        return new OnCollisionResult(attackedResult + "\n" + attackResult);
    }
}