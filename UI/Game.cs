using System.Diagnostics;
using System.Numerics;
using System.Text;

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

        mapWidth = Console.WindowWidth - characterInfoWidth - 1; //-1 because of reasons I guess
        mapHeight = Console.WindowHeight - messageLog.Lines - 1;
        this.world = world;
        this.messageLog = messageLog;
    }

    double tick = 1;
    double lastTick = 0;

    const double frameDuration = 1000.0 / 30.0; // Duration of each frame in milliseconds for 30 FPS
    readonly Stopwatch stopwatch = new();

    public void Run()
    {
        while (true)
        {
            stopwatch.Restart();

            DrawStuffOnScreen();

            if (tick > lastTick)
            {
                lastTick = tick;
                world.Update();
                messageLog.Update();
            }

            HandleInput();

            // Calculate remaining time in the frame and wait if necessary
            stopwatch.Stop();
            int frameTime = (int)stopwatch.ElapsedMilliseconds;
            int timeToWait = (int)(frameDuration - frameTime);

            if (timeToWait > 0)
            {
                Thread.Sleep(timeToWait);
            }
        }
    }

    private void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            ActionResult? result = null;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    result = world.Player.MoveAction.Execute(world.Player, world.Player.Position + new Vector2(0, -1));
                    AdvanceTick();
                    break;
                case ConsoleKey.DownArrow:
                    result = world.Player.MoveAction.Execute(world.Player, world.Player.Position + new Vector2(0, 1));
                    AdvanceTick();
                    break;
                case ConsoleKey.LeftArrow:
                    result = world.Player.MoveAction.Execute(world.Player, world.Player.Position + new Vector2(-1, 0));
                    AdvanceTick();
                    break;
                case ConsoleKey.RightArrow:
                    result = world.Player.MoveAction.Execute(world.Player, world.Player.Position + new Vector2(1, 0));
                    AdvanceTick();
                    break;
                case ConsoleKey.Escape:
                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

            if (result != null)
            {
                messageLog.AddMessage(result.Message);
            }

        }
    }

    private void AdvanceTick()
    {
        tick += 1;
    }

    private void DrawStuffOnScreen()
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

                Tile charInfo = world.GetTileInfoAt(worldX, worldY);

                if (charInfo.Color != lastColor)
                {
                    if (stringBuilder.Length > 0)
                    {
                        Console.Write(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }
                    Console.ForegroundColor = charInfo.Color;
                    lastColor = charInfo.Color;
                }

                stringBuilder.Append(charInfo.Symbol);
            }
            stringBuilder.AppendLine();
        }

        // Write any remaining content in the stringBuilder to the console
        if (stringBuilder.Length > 0)
        {
            Console.Write(stringBuilder.ToString());
        }

        // Reset console color
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void DrawPlayerInfo(int startX, int startY)
    {
        string[] playerInfo = {
            "-----------",
            $"Day: {world.Time.Day}",
            $"Time: {world.Time.ToShortTimeString()}",
            "-----------",
            $"{world.Player.Name}",
            $"HP: {world.Player.HitPoints}",
            $"Str: {world.Player.GetAttribute("Strength")}",
            //inventory and so on and so forth
        };
        ConsoleUtils.DrawTextBlock(startX, startY, characterInfoWidth, playerInfo, ConsoleColor.Gray);
    }
}