struct Tile
{
    public char Symbol;
    public ConsoleColor Color;
    public bool IsBlocking;

    public Tile(char symbol, ConsoleColor color, bool isBlocking = false)
    {
        Symbol = symbol;
        Color = color;
        IsBlocking = isBlocking;
    }
}
