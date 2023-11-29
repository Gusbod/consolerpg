using System.Numerics;

class EntityGeneratorV1 : IEntityGenerator
{
    public WorldEntity GetPlayer(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new WorldEntity(world)
        {
            Name = "Alf",
            // CharInfo = new CharInfo('@', ConsoleColor.White),
            CharInfo = new TileInfo('☻', ConsoleColor.Yellow),

            Position = new Vector2(x, y),
        };

        entity.SetAttribute("Strength", 3);
        entity.SetAttribute("Dexterity", 2);
        entity.SetAttribute("Protection", 1);

        return entity;
    }

    public WorldEntity GetEnemy(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new WorldEntity(world)
        {
            Name = "Bandit",
            // CharInfo = new CharInfo('B', ConsoleColor.Red),
            // CharInfo = new CharInfo('☻', ConsoleColor.Red),
            CharInfo = new TileInfo('☺', ConsoleColor.Red),
            // CharInfo = new CharInfo('☻', ConsoleColor.Red),

            Position = new Vector2(x, y),
            OnCollideAction = new Attack(),
            OnUpdateAction = new LookForTarget()
        };
        entity.SetAttribute("Strength", 1);
        entity.SetAttribute("Dexterity", 1);
        entity.SetAttribute("Protection", 1);

        return entity;
    }

    public WorldEntity GetTree(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new(world)
        {
            Name = "Tree",
            // CharInfo = new CharInfo('↑', ConsoleColor.Green),
            // CharInfo = new CharInfo('♣', ConsoleColor.Green),
            // CharInfo = new CharInfo('♠', ConsoleColor.Green),
            CharInfo = new TileInfo('↟', ConsoleColor.Green),
            Position = new Vector2(x, y)
        };

        return entity;
    }

    public WorldEntity GetRock(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new(world)
        {
            Name = "Rock",
            // CharInfo = new CharInfo('o', ConsoleColor.DarkGray),
            // CharInfo = new CharInfo('O', ConsoleColor.DarkGray),
            // CharInfo = new CharInfo('○', ConsoleColor.DarkGray),
            CharInfo = new TileInfo('●', ConsoleColor.DarkGray),

            Position = new Vector2(x, y),
            OnCollideAction = new Push()
        };

        return entity;
    }
}

interface IEntityGenerator
{
    WorldEntity GetPlayer(int x, int y, IWorldInteraction world);
    WorldEntity GetEnemy(int x, int y, IWorldInteraction world);
    WorldEntity GetTree(int x, int y, IWorldInteraction world);
    WorldEntity GetRock(int x, int y, IWorldInteraction world);
}