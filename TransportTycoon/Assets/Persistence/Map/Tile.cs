public sealed class Tile
{
    public bool IsFree { get; set; }
    public int X { get; init; }
    public int Y { get; init; }
    #nullable enable
    public Facility? Object;
    #nullable disable
    public Tile(int x, int y, Facility @object = null)
    {
        X = x;
        Y = y;
        Object = @object;
        IsFree = Object is null;
    }
}
