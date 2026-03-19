public sealed class Tile
{
    public bool IsFree { get; set; }
    public int X { get; init; }
    public int Y { get; init; }
    public Facility Object;

    public Tile(int x, int y, Facility @object)
    {
        X = x;
        Y = y;
        Object = @object;
    }
}
