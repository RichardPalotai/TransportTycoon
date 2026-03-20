public sealed class Farm<T> : ProdFacility where T : Food
{
    public Farm(bool isGenerated = true) : base(1, isGenerated) // dummy one
    {
        
    }
}