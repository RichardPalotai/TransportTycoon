public sealed class LumberMill<T> : ProdFacility where T : Commodity
{
    public LumberMill(bool isGenerated = true) : base(1200, isGenerated)
    {
        producedPerSec = 3;
    }
    public LumberMill(int id, int x, int y, bool isGenerated) : base(1200, isGenerated, id, x, y)
    {
        producedPerSec = 3;
    }
}
