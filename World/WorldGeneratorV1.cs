using System.Numerics;

class WorldGeneratorV1 : IWorldGenerator
{
    int mapSize;
    IEntityGenerator entityGenerator;
    Random random = new Random();

    public WorldGeneratorV1(int _mapSize, IEntityGenerator entityGenerator)
    {
        this.entityGenerator = entityGenerator;
        mapSize = _mapSize;
    }

    public WorldEntity GetPlayer(World world)
    {
        return entityGenerator.GetPlayer(mapSize / 2, mapSize / 2, world);
    }

    public void PopulateWorld(World world)
    {
        world.groundTiles = new Tile[mapSize, mapSize];

        for (int x = 0; x < world.MapSize; x++)
        {
            for (int y = 0; y < world.MapSize; y++)
            {
                world.groundTiles[x, y] = GetNoiseBasedTile(x, y);
                // world.groundTiles[x, y] = GetRandomGrassTile();
            }
        }

        int numberOfTreeClusters = mapSize / 2; // Number of tree clusters
        int clusterRadius = 10; // Radius around cluster center where trees are more likely

        List<Vector2> treeClusters = new List<Vector2>();
        for (int i = 0; i < numberOfTreeClusters; i++)
        {
            treeClusters.Add(new Vector2(random.Next(world.MapSize), random.Next(world.MapSize)));
        }

        for (int x = 0; x < world.MapSize; x++)
        {
            for (int y = 0; y < world.MapSize; y++)
            {
                if (random.Next(0, 500) == 0)
                {
                    if (!world.CanMoveTo(new Vector2(x, y))) continue;

                    world.AddEntity(entityGenerator.GetEnemy(x, y, world));
                }
                else if (random.Next(0, 100) == 0)
                {
                    Tile tile = world.GetTileInfoAt(x, y);
                    if (tile.Color == ConsoleColor.DarkGreen)
                        world.AddEntity(entityGenerator.GetTree(x, y, world));
                }
                else if (random.Next(0, 25) == 0)
                {
                    if (!world.CanMoveTo(new Vector2(x, y))) continue;

                    if (IsNearTreeCluster(x, y, treeClusters, clusterRadius))
                    {
                        world.AddEntity(entityGenerator.GetRock(x, y, world));
                    }
                }
            }
        }
    }

    private bool IsNearTreeCluster(int x, int y, List<Vector2> treeClusters, int clusterRadius)
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

    private Tile GetNoiseBasedTile(int x, int y)
    {
        double noise1 = Math.Sin((x + random.NextDouble() * 10) * 0.05) * Math.Cos((y + random.NextDouble() * 10) * 0.15);
        double noise2 = Math.Sin(x * 0.1 + y * 0.1) * Math.Cos(y * 0.2 - x * 0.2);
        double noise = noise1 * 0.5 + noise2 * 0.5;

        if (noise > 0.6)
            return new Tile('~', ConsoleColor.DarkBlue, true);
        else if (noise > 0.5)
            return new Tile('·', ConsoleColor.DarkYellow);
        else
            return GetRandomGrassTile();
    }

    private Tile GetRandomGrassTile()
    {
        ConsoleColor[] grassColors = new ConsoleColor[] { ConsoleColor.DarkGreen }; // Different grass colors
        ConsoleColor color = grassColors[random.Next(grassColors.Length)];

        return new Tile(GetRandomGrassChar(), color);
    }

    private char GetRandomGrassChar()
    {
        char[] grassSymbols = new char[] { '·', '.', ',' }; // Different grass symbols
        return grassSymbols[random.Next(grassSymbols.Length)];
    }
}

interface IWorldGenerator
{
    void PopulateWorld(World world);
    WorldEntity GetPlayer(World world);
}