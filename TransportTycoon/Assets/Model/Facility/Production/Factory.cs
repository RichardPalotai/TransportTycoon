public sealed class Factory<T> : ProdFacility where T : Commodity
{
    public Factory() : base(1) // dummy one
    {
        
    }
}
