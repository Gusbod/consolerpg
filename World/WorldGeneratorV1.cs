using System.Numerics;

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

        world.groundTiles = new CharInfo[mapSize, mapSize];

        int numberOfTreeClusters = mapSize / 2; // Number of tree clusters
        int clusterRadius = 10; // Radius around cluster center where trees are more likely

        List<Vector2> treeClusters = new List<Vector2>();
        for (int i = 0; i < numberOfTreeClusters; i++)
        {
            treeClusters.Add(new Vector2(Random.Next(world.MapSize), Random.Next(world.MapSize)));
        }

        // CharInfo grass = new CharInfo('.', ConsoleColor.Green);
        CharInfo grass = new CharInfo('·', ConsoleColor.DarkGreen);
        for (int x = 0; x < world.MapSize; x++)
        {
            for (int y = 0; y < world.MapSize; y++)
            {
                world.groundTiles[x, y] = grass; //GetRandomGrassChar(Random);

                if (Random.Next(0, 500) == 0)
                {
                    world.AddEntity(entityGenerator.GetEnemy(x, y, world));
                }
                else if (Random.Next(0, 100) == 0)
                {
                    world.AddEntity(entityGenerator.GetRock(x, y, world));
                }
                else if (Random.Next(0, 25) == 0)
                {
                    // world.AddEntity(entityGenerator.GetTree(x, y, world));
                    if (IsNearTreeCluster(x, y, treeClusters, clusterRadius, Random))
                    {
                        world.AddEntity(entityGenerator.GetTree(x, y, world));
                    }
                }
            }
        }
    }

    public WorldEntity GetPlayer(World world)
    {
        return entityGenerator.GetPlayer(mapSize / 2, mapSize / 2, world);
    }

    private bool IsNearTreeCluster(int x, int y, List<Vector2> treeClusters, int clusterRadius, Random random)
    {
        foreach (Vector2 cluster in treeClusters)
        {
            if (Vector2.Distance(cluster, new Vector2(x, y)) < clusterRadius)
            {
                return random.Next(0, 100) < 90;
            }
        }

        return false;
    }

    private CharInfo GetRandomGrassChar(Random random)
    {
        char[] grassSymbols = new char[] { '.', ',' }; // Different grass symbols
        ConsoleColor[] grassColors = new ConsoleColor[] { ConsoleColor.Green, ConsoleColor.DarkGreen }; // Different grass colors

        char symbol = grassSymbols[random.Next(grassSymbols.Length)];
        ConsoleColor color = grassColors[random.Next(grassColors.Length)];

        return new CharInfo(symbol, color);
    }
}

interface IWorldGenerator
{
    void PopulateWorld(World world);
    WorldEntity GetPlayer(World world);
}