using System.Text;

//The game class deals with drawing the game to the console and handling input.
class Game
{
    readonly World world;
    readonly MessageLog messageLog;
    readonly int mapWidth;
    readonly int mapHeight;
    readonly int characterInfoWidth = 20;

    public Game(World world, MessageLog messageLog)
    {
        Console.Clear();
        Console.CursorVisible = false;

        mapWidth = Console.WindowWidth - characterInfoWidth - 1;
        mapHeight = Console.WindowHeight - messageLog.Lines - 1;
        this.world = world;
        this.messageLog = messageLog;
    }

    public void Run()
    {
        //This is the main game loop.
        while (true)
        {
            Draw();
            HandleInput();
            Update();
        }
    }

    private void HandleInput()
    {
        // if (Console.KeyAvailable)
        // {
        var key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                world.MovePlayer(0, -1);
                break;
            case ConsoleKey.DownArrow:
                world.MovePlayer(0, 1);
                break;
            case ConsoleKey.LeftArrow:
                world.MovePlayer(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                world.MovePlayer(1, 0);
                break;
            case ConsoleKey.Escape:
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
                break;
            default:
                break;
        }
        // }
    }

    public void Draw()
    {
        DrawMap();
        DrawPlayerInfo(mapWidth + 1, 0);

        // Draw the message log
        ConsoleUtils.DrawTextBlock(0, mapHeight,
                                   Console.WindowWidth,
                                   messageLog.GetLastMessages(),
                                   ConsoleColor.DarkGray);

        //Reset stuff for next draw
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 0);
    }

    private void DrawMap()
    {
        StringBuilder stringBuilder = new StringBuilder();
        ConsoleColor lastColor = Console.ForegroundColor;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                int worldX = (int)(world.Player.Position.X - mapWidth / 2 + x);
                int worldY = (int)(world.Player.Position.Y - mapHeight / 2 + y);

                CharInfo charInfo = world.GetCharInfoAt(worldX, worldY);

                if (world.Player.Position.X == worldX && world.Player.Position.Y == worldY)
                {
                    charInfo = world.Player.CharInfo;
                }

                if (charInfo.Color != lastColor)
                {
                    Console.Write(stringBuilder.ToString());
                    stringBuilder.Clear();
                    Console.ForegroundColor = charInfo.Color;
                    lastColor = charInfo.Color;
                }

                stringBuilder.Append(charInfo.Symbol);
            }
            stringBuilder.AppendLine();
        }

        // Write any remaining content in the stringBuilder to the console
        Console.Write(stringBuilder.ToString());
    }

    private void DrawPlayerInfo(int startX, int startY)
    {
        string[] playerInfo = {
            $"{world.Player.Name}",
            $"HP: {world.Player.Health}",
            $"Str: {world.Player.Strength}"
            //inventory and so on and so forth
        };
        ConsoleUtils.DrawTextBlock(startX, startY, characterInfoWidth, playerInfo, ConsoleColor.Gray);
    }

    public void Update()
    {
        messageLog.Update();
        world.Update();
    }
}