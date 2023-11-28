using System.Numerics;

interface IWorldInteraction
{
    bool CanMoveTo(Vector2 position);
    bool TryMoveEntity(WorldEntity entity, Vector2 newPosition);
    WorldEntity? GetEntityAt(Vector2 position);
    WorldEntity Player { get; }
}

class World : IWorldInteraction
{
    public CharInfo[,] mapTiles = new CharInfo[0, 0];
    public int MapSize => mapTiles.GetLength(0);
    public WorldEntity Player { get; set; }

    readonly List<WorldEntity> entities = new();
    readonly Dictionary<Vector2, WorldEntity> entityPositionMap = new();


    public World(IWorldGenerator worldGenerator)
    {
        worldGenerator.PopulateWorld(this);
        Player = worldGenerator.GetPlayer(this);
    }

    public void Update()
    {
        // Check a 50x50 area around the player and update all entities in that area
        int area = 50;
        int playerX = (int)Player.Position.X;
        int playerY = (int)Player.Position.Y;

        for (int dx = playerX - area; dx <= playerX + area; dx++)
        {
            for (int dy = playerY - area; dy <= playerY + area; dy++)
            {
                WorldEntity? entity = GetEntityAt(new Vector2(dx, dy));
                if (entity != null)
                {
                    ActionResult result = entity.Update();
                    if (result.Success)
                    {
                        Console.WriteLine(result.Message);
                    }
                }
            }
        }
    }

    public void AddEntity(WorldEntity entity)
    {
        entityPositionMap[entity.Position] = entity;
        entities.Add(entity);
    }

    public void UpdateEntityPosition(WorldEntity entity, Vector2 newPosition)
    {
        entityPositionMap.Remove(entity.Position);
        entity.Position = newPosition;
        entityPositionMap[entity.Position] = entity;
    }

    public CharInfo GetCharInfoAt(int x, int y)
    {
        if (x < 0 || x >= MapSize || y < 0 || y >= MapSize)
        {
            return new CharInfo(' ', ConsoleColor.Black); // Empty space
        }

        Vector2 position = new Vector2(x, y);

        if (entityPositionMap.TryGetValue(position, out WorldEntity? entity))
        {
            return entity.CharInfo;
        }
        else
        {
            return mapTiles[x, y];
        }
    }

    public WorldEntity? GetEntityAt(Vector2 position)
    {
        entityPositionMap.TryGetValue(position, out WorldEntity? entity);
        return entity;
    }

    public bool CanMoveTo(Vector2 vector2)
    {
        if (vector2.X < 0 || vector2.X >= MapSize || vector2.Y < 0 || vector2.Y >= MapSize)
        {
            return false;
        }

        if (entityPositionMap.TryGetValue(vector2, out WorldEntity? entity))
        {
            return !entity.IsBlocking;
        }

        return true;
    }

    public bool TryMoveEntity(WorldEntity entity, Vector2 newPosition)
    {
        if (CanMoveTo(newPosition))
        {
            UpdateEntityPosition(entity, newPosition);
            return true;
        }
        else
        {
            return false;
        }
    }
}
