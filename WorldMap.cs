using System.Numerics;

class WorldMap
{
    public char[,] mapTiles;
    public int MapWidth => mapTiles.GetLength(0);
    private List<GameEntity> entities = new List<GameEntity>();
    private Dictionary<Vector2, GameEntity> entityPositionMap;

    public WorldMap(int mapWidth)
    {
        entityPositionMap = new Dictionary<Vector2, GameEntity>();
        entities = new List<GameEntity>();
        mapTiles = new char[mapWidth, mapWidth];
        GenerateMap();
    }

    private void GenerateMap()
    {
        Random Random = new Random();

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapWidth; y++)
            {
                // if (Random.Next(0, 10) == 0)
                // {
                //     mapTiles[x, y] = ',';
                // }
                // else
                // {
                mapTiles[x, y] = '.';
                // }

                if (Random.Next(0, 50) == 0)
                {
                    AddEntity(new Tree(x, y));
                }
                else if (Random.Next(0, 100) == 0)
                {
                    AddEntity(new Rock(x, y));
                }
                else if (Random.Next(0, 200) == 0)
                {
                    AddEntity(new Enemy(x, y));
                }
            }
        }
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

    internal char GetSymbolAt(int x, int y)
    {
        char symbol;
        Vector2 position = new Vector2(x, y);

        if (entityPositionMap.TryGetValue(position, out GameEntity? entity))
        {
            symbol = entity.Symbol;
        }
        else
        {
            symbol = mapTiles[x, y];
        }
        return symbol;
    }

    internal bool CanMoveTo(Vector2 vector2)
    {
        if (vector2.X < 0 || vector2.X >= MapWidth || vector2.Y < 0 || vector2.Y >= MapWidth)
        {
            return false;
        }

        if (entityPositionMap.TryGetValue(vector2, out GameEntity? entity))
        {
            return !entity.IsBlocking;
        }

        return true;
    }
}
