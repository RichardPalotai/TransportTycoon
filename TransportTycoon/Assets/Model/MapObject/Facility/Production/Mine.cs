public sealed class Mine<T> : ProdFacility where T : Commodity
{
    public Mine(bool isGenerated = true) : base(1200, isGenerated)
    {
        
    }
}
