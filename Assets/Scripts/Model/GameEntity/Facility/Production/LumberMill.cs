public sealed class LumberMill<T> : ProdFacility where T : Commodity
{
    public LumberMill(bool isGenerated = true) : base(0, isGenerated)
    {
        producedPerSec = 3;
    }
    public LumberMill(int id, int x, int y, bool isGenerated) : base(0, isGenerated, id, x, y)
    {
        producedPerSec = 3;
    }
}
