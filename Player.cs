using System.Numerics;

class Player : GameEntity, IAttacker, IAttackable
{
    public int XP { get; set; }
    public int Level => XP / 10 + 1;

    public Player(int x, int y)
    {
        Name = "Alf";
        Position = new Vector2(x, y);
        CharInfo = new CharInfo('@', ConsoleColor.White);
        Health = 10;
        Strength = 1;
    }

    public string Attack(IAttackable target)
    {
        // Chance to hit based on level
        int chanceToHit = 50 + Level * 10;
        if (new Random().Next(0, 100) < chanceToHit)
        {
            string result = target.OnAttacked(Strength);
            XP += 1;
            return "Attacked with " + Strength + " damage. " + result;
        }
        else
        {
            return "Missed!";
        }
    }

    public string OnAttacked(int damage)
    {
        Health -= damage;
        return "Player health: " + Health;
    }

    public void Move(int x, int y)
    {
        Position += new Vector2(x, y);
    }
}