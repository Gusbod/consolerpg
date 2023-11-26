public static class ConsoleUtils
{
    public static void WriteAt(int x, int y, string text)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(text);
    }

    public static void ClearScreenArea(int startX, int startY, int width, int height)
    {
        string blankLine = new(' ', width);
        for (int y = 0; y < height; y++)
        {
            Console.SetCursorPosition(startX, startY + y);
            Console.Write(blankLine);
        }
    }

    public static void DrawTextBlock(int startX, int startY, int width, IEnumerable<string> lines, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        ClearScreenArea(startX, startY, width, lines.Count());

        int y = startY;
        foreach (var line in lines)
        {
            WriteAt(startX, y, line);
            y++;
        }
    }
}