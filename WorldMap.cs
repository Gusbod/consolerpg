class WorldMap
{
    public char[,] mapTiles;
    public int MapWidth => mapTiles.GetLength(0);
    private List<GameEntity> entities = new List<GameEntity>();

    public WorldMap(int mapWidth)
    {
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
                if (Random.Next(0, 10) == 0)
                {
                    mapTiles[x, y] = ',';
                }
                else
                {
                    mapTiles[x, y] = '.';
                }

                if (Random.Next(0, 100) == 0)
                {
                    entities.Add(new Tree(x, y));
                }
                else if (Random.Next(0, 100) == 0)
                {
                    entities.Add(new Rock(x, y));
                }
                else if (Random.Next(0, 100) == 0)
                {
                    entities.Add(new Enemy(x, y));
                }
            }
        }

        // Update: entities.Add(new Tree(x, y)); or entities.Add(new Enemy(x, y));
    }

    internal char GetSymbolAt(int x, int y)
    {
        char symbol = mapTiles[x, y];
        GameEntity? entity = entities.FirstOrDefault(e => e.Position.X == x && e.Position.Y == y);
        if (entity != null)
        {
            symbol = entity.Symbol; // Override symbol if an entity is present
        }
        return symbol;
    }

    // public void Update()
    // {
    // foreach (var entity in entities)
    // {
    //     entity.Update();
    // }
    // }

    // Additional methods like collision detection, enemy movement, etc.
}
