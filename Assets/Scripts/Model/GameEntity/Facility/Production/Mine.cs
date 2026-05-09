public sealed class Mine<T> : ProdFacility where T : Commodity
{
    public Mine(bool isGenerated = true) : base(1200, isGenerated)
    {
        producedPerSec = 10;
    }
    public Mine(int id, int x, int y, bool isGenerated) : base(1200, isGenerated, id, x, y)
    {
        producedPerSec = 10;
    }
}
