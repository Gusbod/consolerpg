MessageLog logger = new();
EntityGeneratorV1 entityGenerator = new();
WorldGeneratorV1 worldGenerator = new(1024, entityGenerator);
World world = new(worldGenerator, logger);

new Game(world, logger).Run();