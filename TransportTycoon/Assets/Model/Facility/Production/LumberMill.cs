public sealed class LumberMill<T> : ProdFacility where T : Commodity
{
    public LumberMill(bool isGenerated = true) : base(1, isGenerated) // dummy one
    {
        
    }
}