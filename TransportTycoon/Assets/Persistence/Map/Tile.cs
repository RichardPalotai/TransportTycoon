public sealed class Tile
{
    public bool IsFree { get; set; }
    public int X { get; init; }
    public int Y { get; init; }
    public Facility Type { get; set; }

    public Tile(int x, int y)
    {
        
    }
}