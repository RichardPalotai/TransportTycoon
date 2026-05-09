public sealed class Factory<T> : ProdFacility where T : Commodity
{
    public Factory(bool isGenerated = true) : base(1500, isGenerated)
    {
        producedPerSec = 12;
    }
    public Factory(int id, int x, int y, bool isGenerated) : base(1500, isGenerated, id, x, y)
    {
        producedPerSec = 12;
    }
}
