using System.Numerics;

class EntityGeneratorV1 : IEntityGenerator
{
    public GameEntity GetPlayer(int x, int y, IWorldInteraction world)
    {
        GameEntity entity = new GameEntity(world)
        {
            Name = "Alf",
            CharInfo = new CharInfo('@', ConsoleColor.White),
            Position = new Vector2(x, y),
        };

        entity.SetAttribute("Strength", 3);
        entity.SetAttribute("Dexterity", 2);
        entity.SetAttribute("Protection", 1);

        return entity;
    }

    public GameEntity GetEnemy(int x, int y, IWorldInteraction world)
    {
        GameEntity entity = new GameEntity(world)
        {
            Name = "Goblin",
            CharInfo = new CharInfo('g', ConsoleColor.Red),
            Position = new Vector2(x, y),
            OnCollideAction = new Attack(),
            //TODO OnUpdateAction = new MoveTowardsPlayer()
        };
        entity.SetAttribute("Strength", 1);
        entity.SetAttribute("Dexterity", 1);
        entity.SetAttribute("Protection", 2);

        return entity;
    }

    public GameEntity GetTree(int x, int y, IWorldInteraction world)
    {
        GameEntity entity = new(world)
        {
            Name = "Tree",
            CharInfo = new CharInfo('T', ConsoleColor.Green),
            Position = new Vector2(x, y)
        };

        return entity;
    }

    public GameEntity GetRock(int x, int y, IWorldInteraction world)
    {
        GameEntity entity = new(world)
        {
            Name = "Rock",
            CharInfo = new CharInfo('o', ConsoleColor.DarkGray),
            Position = new Vector2(x, y),
            OnCollideAction = new Push()
        };

        return entity;
    }
}

interface IEntityGenerator
{
    GameEntity GetPlayer(int x, int y, IWorldInteraction world);
    GameEntity GetEnemy(int x, int y, IWorldInteraction world);
    GameEntity GetTree(int x, int y, IWorldInteraction world);
    GameEntity GetRock(int x, int y, IWorldInteraction world);
}