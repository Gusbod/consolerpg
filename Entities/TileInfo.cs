struct TileInfo
{
    public char Symbol;
    public ConsoleColor Color;
    public bool IsBlocking;

    public TileInfo(char symbol, ConsoleColor color, bool isBlocking = false)
    {
        Symbol = symbol;
        Color = color;
        IsBlocking = isBlocking;
    }
}
