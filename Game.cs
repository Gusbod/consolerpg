using System.Linq.Expressions;
using System.Numerics;
using System.Text;

class Game
{
    readonly WorldMap worldMap;
    readonly Player player;

    readonly int viewWidth = 40;
    readonly int viewHeight = 20;

    private char[,] buffer;

    public Game()
    {
        buffer = new char[viewHeight, viewWidth];
        player = new Player(1024, 1024);
        worldMap = new WorldMap(2048);
    }

    public void Run()
    {
        while (true)
        {
            Draw();
            HandleInput();
            // Update();
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
                player.Move(0, -1);
                break;
            case ConsoleKey.DownArrow:
                player.Move(0, 1);
                break;
            case ConsoleKey.LeftArrow:
                player.Move(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                player.Move(1, 0);
                break;
            default:
                break;
        }
        // }
    }

    private void MovePlayer(int x, int y)
    {
        //Check if the player can move to the new position
        if (worldMap.GetSymbolAt((int)player.Position.X + x, (int)player.Position.Y + y) == '.')
        {
            //Move the player
            player.Position += new Vector2(x, y);
        }
    }

    // public void Draw()
    // {
    //     //Set cursors to top left
    //     Console.SetCursorPosition(0, 0);
    //     int halfViewWidth = viewWidth / 2;
    //     int halfViewHeight = viewHeight / 2;

    //     // Calculate the top-left position of the visible area
    //     int startX = (int)Math.Max(0, player.Position.X - halfViewWidth);
    //     int startY = (int)Math.Max(0, player.Position.Y - halfViewHeight);

    //     // Adjust start positions to avoid going beyond map boundaries
    //     startX = Math.Min(startX, worldMap.MapWidth - viewWidth);
    //     startY = Math.Min(startY, worldMap.MapWidth - viewHeight);

    //     char symbol;
    //     for (int y = startY; y < startY + viewHeight; y++)
    //     {
    //         for (int x = startX; x < startX + viewWidth; x++)
    //         {
    //             //Draw the player symbol at the player position
    //             if (player.Position.X == x && player.Position.Y == y)
    //             {
    //                 symbol = player.Symbol;
    //             }
    //             else
    //             {
    //                 //Otherwise draw the map tile
    //                 symbol = worldMap.GetSymbolAt(x, y);
    //             }

    //             Console.Write(symbol);
    //         }
    //         Console.WriteLine();
    //     }
    // }

    // public void Draw()
    // {
    //     StringBuilder stringBuilder = new StringBuilder();

    //     int halfViewWidth = viewWidth / 2;
    //     int halfViewHeight = viewHeight / 2;

    //     // Calculate the top-left position of the visible area
    //     int startX = (int)Math.Max(0, player.Position.X - halfViewWidth);
    //     int startY = (int)Math.Max(0, player.Position.Y - halfViewHeight);

    //     // Adjust start positions to avoid going beyond map boundaries
    //     startX = Math.Min(startX, worldMap.MapWidth - viewWidth);
    //     startY = Math.Min(startY, worldMap.MapWidth - viewHeight);

    //     for (int y = startY; y < startY + viewHeight; y++)
    //     {
    //         for (int x = startX; x < startX + viewWidth; x++)
    //         {
    //             char symbol = worldMap.GetSymbolAt(x, y);

    //             if (player.Position.X == x && player.Position.Y == y)
    //             {
    //                 symbol = player.Symbol;
    //             }

    //             stringBuilder.Append(symbol);
    //         }
    //         stringBuilder.AppendLine();
    //     }

    //     Console.SetCursorPosition(0, 0);
    //     Console.Write(stringBuilder.ToString());
    // }

    public void Draw()
    {
        int halfViewWidth = viewWidth / 2;
        int halfViewHeight = viewHeight / 2;

        // Calculate the top-left position of the visible area
        float startX = Math.Max(0, player.Position.X - halfViewWidth);
        float startY = Math.Max(0, player.Position.Y - halfViewHeight);

        // Adjust start positions to avoid going beyond map boundaries
        startX = Math.Min(startX, worldMap.MapWidth - viewWidth);
        startY = Math.Min(startY, worldMap.MapWidth - viewHeight);

        StringBuilder stringBuilder = new StringBuilder();

        for (int y = 0; y < viewHeight; y++)
        {
            for (int x = 0; x < viewWidth; x++)
            {
                int worldX = (int)startX + x;
                int worldY = (int)startY + y;

                char symbol = worldMap.GetSymbolAt(worldX, worldY);

                if (player.Position.X == worldX && player.Position.Y == worldY)
                {
                    symbol = player.Symbol;
                }

                buffer[y, x] = symbol;
            }
        }

        for (int y = 0; y < viewHeight; y++)
        {
            for (int x = 0; x < viewWidth; x++)
            {
                stringBuilder.Append(buffer[y, x]);
            }
            stringBuilder.AppendLine();
        }

        Console.SetCursorPosition(0, 0);
        Console.Write(stringBuilder.ToString());
    }

}