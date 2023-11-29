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
    public Tile[,] groundTiles = new Tile[0, 0];
    public int MapSize => groundTiles.GetLength(0);
    public WorldEntity Player { get; set; }

    readonly List<WorldEntity> entities = new();
    readonly Dictionary<Vector2, WorldEntity> entityPositionMap = new();


    public World(IWorldGenerator worldGenerator)
    {
        Player = worldGenerator.GetPlayer(this);
        worldGenerator.PopulateWorld(this);
    }

    public void Update()
    {
        // Check a 50x50 area around the player and update all entities in that area
        int area = 25;
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
                    //TODO handle the result so that it can be logged to the message log
                }
            }
        }
    }

    public void AddEntity(WorldEntity entity)
    {
        if (entityPositionMap.ContainsKey(entity.Position)) return;

        entityPositionMap[entity.Position] = entity;
        entities.Add(entity);
    }

    private void SetEntityPosition(WorldEntity entity, Vector2 newPosition)
    {
        var oldPosition = entity.Position;

        entityPositionMap.Remove(entity.Position);
        entity.Position = newPosition;
        entityPositionMap[entity.Position] = entity;

        //FIXME A bit hacky way to make sure that entities other entities walk into, show up
        //again on the map. Keep until we have a way to have multiple entities on the same spot
        foreach (var otherEntity in entities)
        {
            if (otherEntity == entity) continue;

            if (otherEntity.Position == oldPosition)
            {
                entityPositionMap[otherEntity.Position] = otherEntity;
                break;
            }
        }
    }

    public Tile GetTileInfoAt(int x, int y)
    {
        if (x < 0 || x >= MapSize || y < 0 || y >= MapSize)
        {
            return new Tile(' ', ConsoleColor.Black); // Empty space
        }

        Vector2 position = new(x, y);


        if (Player.Position.X == x && Player.Position.Y == y)
        {
            return Player.CharInfo;
        }

        if (entityPositionMap.TryGetValue(position, out WorldEntity? entity))
        {
            return entity.CharInfo;
        }
        else
        {
            return groundTiles[x, y];
        }
    }

    public WorldEntity? GetEntityAt(Vector2 position)
    {
        entityPositionMap.TryGetValue(position, out WorldEntity? entity);
        return entity;
    }

    public bool CanMoveTo(int x, int y)
    {
        return CanMoveTo(new Vector2(x, y));
    }

    public bool CanMoveTo(Vector2 vector2)
    {
        if (vector2.X < 0 || vector2.X >= MapSize || vector2.Y < 0 || vector2.Y >= MapSize)
        {
            return false;
        }

        if (GetTileInfoAt((int)vector2.X, (int)vector2.Y).IsBlocking)
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
            SetEntityPosition(entity, newPosition);
            return true;
        }
        else
        {
            return false;
        }
    }
}
