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
                MovePlayer(0, -1);
                break;
            case ConsoleKey.DownArrow:
                MovePlayer(0, 1);
                break;
            case ConsoleKey.LeftArrow:
                MovePlayer(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                MovePlayer(1, 0);
                break;
            default:
                break;
        }
        // }
    }

    private void MovePlayer(int x, int y)
    {
        //Check if the player can move to the new position
        if (worldMap.CanMoveTo(player.Position + new Vector2(x, y)))
        {
            player.Move(x, y);
        }
    }

    // public void Draw()
    // {
    //     StringBuilder stringBuilder = new StringBuilder();
    //     ConsoleColor lastColor = Console.ForegroundColor;

    //     for (int y = 0; y < viewHeight; y++)
    //     {
    //         for (int x = 0; x < viewWidth; x++)
    //         {
    //             int worldX = (int)(player.Position.X - viewWidth / 2 + x);
    //             int worldY = (int)(player.Position.Y - viewHeight / 2 + y);

    //             CharInfo charInfo = worldMap.GetCharInfoAt(worldX, worldY);

    //             if (player.Position.X == worldX && player.Position.Y == worldY)
    //             {
    //                 charInfo = player.CharInfo;
    //             }

    //             if (charInfo.Color != lastColor)
    //             {
    //                 if (stringBuilder.Length > 0)
    //                 {
    //                     Console.Write(stringBuilder.ToString());
    //                     stringBuilder.Clear();
    //                 }
    //                 Console.ForegroundColor = charInfo.Color;
    //                 lastColor = charInfo.Color;
    //             }

    //             stringBuilder.Append(charInfo.Symbol);
    //         }
    //         stringBuilder.AppendLine();
    //     }

    //     Console.SetCursorPosition(0, 0);
    //     Console.Write(stringBuilder.ToString());
    //     Console.ForegroundColor = ConsoleColor.White; // Reset to default color
    // }

    public void Draw()
    {
        StringBuilder stringBuilder = new StringBuilder();
        ConsoleColor lastColor = Console.ForegroundColor;

        for (int y = 0; y < viewHeight; y++)
        {
            for (int x = 0; x < viewWidth; x++)
            {
                int worldX = (int)(player.Position.X - viewWidth / 2 + x);
                int worldY = (int)(player.Position.Y - viewHeight / 2 + y);

                CharInfo charInfo = worldMap.GetCharInfoAt(worldX, worldY);

                if (player.Position.X == worldX && player.Position.Y == worldY)
                {
                    charInfo = player.CharInfo; // Assuming Player has a CharInfo property
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

        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.White; // Reset to default color
    }

}