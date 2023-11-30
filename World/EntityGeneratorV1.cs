using System.Numerics;
using System.Xml.Serialization;

class EntityGeneratorV1 : IEntityGenerator
{
    Random random = new();

    public WorldEntity GetPlayer(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new WorldEntity(world)
        {
            Name = "Alf",
            Tile = new Tile('☻', ConsoleColor.Yellow),
            Position = new Vector2(x, y),
        };
        SetCommonAttributes(entity, 100, 5);
        SetLivingAttributes(entity, 50, 50, 50);

        return entity;
    }

    public WorldEntity GetEnemy(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new WorldEntity(world)
        {
            Name = "Bandit",
            Tile = new Tile('☺', ConsoleColor.Red), //B☻☺
            Position = new Vector2(x, y),
            OnCollideAction = new Attack(),
            OnUpdateAction = new LookForTarget()
        };
        SetCommonAttributes(entity, random.Next(30, 81), random.Next(10, 61));
        SetLivingAttributes(entity, random.Next(10, 61), random.Next(10, 61), random.Next(10, 61));

        return entity;
    }

    public WorldEntity GetTree(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new(world)
        {
            Name = "Tree",
            Tile = new Tile('↟', ConsoleColor.Green), //↑♣♠♦♥↥⇞↠
            Position = new Vector2(x, y)
        };
        SetCommonAttributes(entity, 1000, 75);

        return entity;
    }

    public WorldEntity GetRock(int x, int y, IWorldInteraction world)
    {
        WorldEntity entity = new(world)
        {
            Name = "Rock",
            Tile = new Tile('●', ConsoleColor.DarkGray), //oO○●
            Position = new Vector2(x, y),
            OnCollideAction = new Push()
        };
        SetCommonAttributes(entity, 500, 90);

        return entity;
    }

    private void SetCommonAttributes(WorldEntity entity, int hitpoints, int sturdyness)
    {
        entity.SetAttribute("hitpoints", hitpoints);
        entity.SetAttribute("sturdyness", 1);
    }

    private void SetLivingAttributes(WorldEntity entity, int perception, int stamina, int dexterity)
    {
        entity.SetAttribute("perception", perception);
        entity.SetAttribute("stamina", stamina);
        entity.SetAttribute("dexterity", dexterity);
        //hunger, thirst, wakeness, bravery, hearing, sight
    }
}

interface IEntityGenerator
{
    WorldEntity GetPlayer(int x, int y, IWorldInteraction world);
    WorldEntity GetEnemy(int x, int y, IWorldInteraction world);
    WorldEntity GetTree(int x, int y, IWorldInteraction world);
    WorldEntity GetRock(int x, int y, IWorldInteraction world);
}