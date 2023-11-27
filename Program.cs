MessageLog logger = new MessageLog();
World world = new World(new WorldGeneratorV1(1024), logger);

new Game(world, logger).Run();