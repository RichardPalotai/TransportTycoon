public sealed class Factory<T> : ProdFacility where T : Commodity
{
    public Factory(bool isGenerated = true) : base(1500, isGenerated)
    {
        
    }
}
