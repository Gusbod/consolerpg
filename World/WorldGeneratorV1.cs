class WorldGeneratorV1 : IWorldGenerator
{
    int mapSize;
    IEntityGenerator entityGenerator;

    public WorldGeneratorV1(int _mapSize, IEntityGenerator entityGenerator)
    {
        this.entityGenerator = entityGenerator;
        mapSize = _mapSize;
    }

    public void PopulateWorld(World world)
    {
        Random Random = new Random();

        world.mapTiles = new CharInfo[mapSize, mapSize];

        CharInfo grass = new CharInfo('.', ConsoleColor.Green);
        for (int x = 0; x < world.MapSize; x++)
        {
            for (int y = 0; y < world.MapSize; y++)
            {
                world.mapTiles[x, y] = grass;

                if (Random.Next(0, 200) == 0)
                {
                    world.AddEntity(entityGenerator.GetEnemy(x, y, world));
                }
                else if (Random.Next(0, 100) == 0)
                {
                    world.AddEntity(entityGenerator.GetRock(x, y, world));
                }
                else if (Random.Next(0, 25) == 0)
                {
                    world.AddEntity(entityGenerator.GetTree(x, y, world));
                }
            }
        }
    }

    public WorldEntity GetPlayer(World world)
    {
        return entityGenerator.GetPlayer(mapSize / 2, mapSize / 2, world);
    }
}

interface IWorldGenerator
{
    void PopulateWorld(World world);
    WorldEntity GetPlayer(World world);
}