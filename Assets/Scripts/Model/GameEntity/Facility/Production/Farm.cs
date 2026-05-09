public sealed class Farm<T> : ProdFacility where T : Food
{
    public Farm(bool isGenerated = true) : base(1000, isGenerated)
    {
        producedPerSec = 5;
        producedCount = 0;
    }
    public Farm(int id, int x, int y, bool isGenerated) : base(1000, isGenerated, id, x, y)
    {
        producedPerSec = 5;
        producedCount = 0;
    }
}
