public sealed class Farm<T> : ProdFacility where T : Food
{
    public Farm(bool isGenerated = true) : base(0, isGenerated)
    {
        producedPerSec = 5;
        producedCount = 0;
    }
    public Farm(int id, int x, int y, bool isGenerated) : base(0, isGenerated, id, x, y)
    {
        producedPerSec = 5;
        producedCount = 0;
    }
}
