class WorldGeneratorV1 : IWorldGenerator
{
    int mapSize;

    public WorldGeneratorV1(int _mapSize)
    {
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

                if (Random.Next(0, 50) == 0)
                {
                    world.AddEntity(new Tree(x, y, world));
                }
                else if (Random.Next(0, 100) == 0)
                {
                    world.AddEntity(new Rock(x, y, world));
                }
                else if (Random.Next(0, 200) == 0)
                {
                    world.AddEntity(new Enemy(x, y, world));
                }
            }
        }
    }
}

interface IWorldGenerator
{
    void PopulateWorld(World world);
}