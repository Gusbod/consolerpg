using System.Numerics;

interface IWorldInteraction
{
    bool CanMoveTo(Vector2 position);
    bool TryMoveEntity(GameEntity entity, Vector2 newPosition);
    GameEntity? GetEntityAt(Vector2 position);
}

class World : IWorldInteraction
{
    public CharInfo[,] mapTiles = new CharInfo[0, 0];
    public int MapSize => mapTiles.GetLength(0);
    public GameEntity Player { get; set; }

    readonly List<GameEntity> entities = new();
    readonly Dictionary<Vector2, GameEntity> entityPositionMap = new();


    public World(IWorldGenerator worldGenerator)
    {
        Player = new GameEntity(this);
        worldGenerator.PopulateWorld(this);
    }

    public void Update()
    {

    }

    public void AddEntity(GameEntity entity)
    {
        entityPositionMap[entity.Position] = entity;
        entities.Add(entity);
    }

    public void UpdateEntityPosition(GameEntity entity, Vector2 newPosition)
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

        if (entityPositionMap.TryGetValue(position, out GameEntity? entity))
        {
            return entity.CharInfo;
        }
        else
        {
            return mapTiles[x, y];
        }
    }

    public GameEntity? GetEntityAt(Vector2 position)
    {
        entityPositionMap.TryGetValue(position, out GameEntity? entity);
        return entity;
    }

    public bool CanMoveTo(Vector2 vector2)
    {
        if (vector2.X < 0 || vector2.X >= MapSize || vector2.Y < 0 || vector2.Y >= MapSize)
        {
            return false;
        }

        if (entityPositionMap.TryGetValue(vector2, out GameEntity? entity))
        {
            return !entity.IsBlocking;
        }

        return true;
    }

    public bool TryMoveEntity(GameEntity entity, Vector2 newPosition)
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
