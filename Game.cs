using System.Numerics;
using System.Text;

class Game
{
    readonly WorldMap worldMap = new(2048);
    readonly Player player = new(1024, 1024);
    readonly MessageLog MessageLog = new();
    readonly int viewWidth = 40;
    readonly int viewHeight = 20;

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
        Vector2 newPosition = player.Position + new Vector2(x, y);
        if (worldMap.CanMoveTo(newPosition))
        {
            player.Move(x, y);
        }
        else
        {
            GameEntity? entity = worldMap.GetEntityAt(newPosition);
            OnCollisionResult? result = entity?.OnCollision(player);
            if (result?.Message != null)
            {
                MessageLog.AddMessage(result.Message);
            }
        }
    }

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
                    charInfo = player.CharInfo;
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

        Console.ForegroundColor = ConsoleColor.DarkGray;
        foreach (var message in MessageLog.GetMessages())
        {
            Console.WriteLine(message);
        }

        Console.ForegroundColor = ConsoleColor.White; // Reset to default color
        Console.SetCursorPosition(0, 0);
    }
}