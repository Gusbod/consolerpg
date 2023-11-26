using System.Numerics;

interface IWorldInteraction
{
    bool CanMoveTo(Vector2 position);
    bool TryMoveEntity(GameEntity entity, Vector2 newPosition);
    GameEntity? GetEntityAt(Vector2 position);
}

class World : IWorldInteraction
{
    public CharInfo[,] mapTiles;
    public int MapSize => mapTiles.GetLength(0);
    public Player Player { get; private set; }

    readonly List<GameEntity> entities = new();
    readonly Dictionary<Vector2, GameEntity> entityPositionMap = new();

    MessageLog messageLog;

    public World(int mapWidth, MessageLog logger)
    {
        mapTiles = new CharInfo[mapWidth, mapWidth];
        messageLog = logger;
        Player = new Player(mapWidth / 2, mapWidth / 2, this);
        GenerateMap();
    }

    public void MovePlayer(int x, int y)
    {
        Vector2 newPosition = Player.Position + new Vector2(x, y);
        if (CanMoveTo(newPosition))
        {
            Player.Move(x, y);
        }
        else
        {
            GameEntity? entity = GetEntityAt(newPosition);
            OnCollisionResult? result = entity?.OnCollision(Player);
            if (result?.Message != null)
            {
                messageLog.AddMessage(result.Message);
            }
        }
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

    public GameEntity? GetEntityAt(Vector2 position)
    {
        entityPositionMap.TryGetValue(position, out GameEntity? entity);
        return entity;
    }

    private void GenerateMap()
    {
        Random Random = new Random();

        CharInfo grass = new CharInfo('.', ConsoleColor.Green);
        for (int x = 0; x < MapSize; x++)
        {
            for (int y = 0; y < MapSize; y++)
            {
                // if (Random.Next(0, 10) == 0)
                // {
                //     mapTiles[x, y] = ',';
                // }
                // else
                // {
                mapTiles[x, y] = grass;
                // }

                if (Random.Next(0, 50) == 0)
                {
                    AddEntity(new Tree(x, y, this));
                }
                else if (Random.Next(0, 100) == 0)
                {
                    AddEntity(new Rock(x, y, this));
                }
                else if (Random.Next(0, 200) == 0)
                {
                    AddEntity(new Enemy(x, y, this));
                }
            }
        }
    }
}
