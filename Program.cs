﻿EntityGeneratorV1 entityGenerator = new();
WorldGeneratorV1 worldGenerator = new(4, entityGenerator);
World world = new(worldGenerator);

new Game(world, new MessageLog()).Run();