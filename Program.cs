MessageLog logger = new MessageLog();
World world = new World(1024, logger);
Game game = new Game(world, logger);

game.Run();