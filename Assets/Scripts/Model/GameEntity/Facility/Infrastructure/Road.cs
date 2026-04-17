public sealed class Road : Facility
{
    public bool IsCrossRoad { get; set; } = false;
    public Road(bool isGenerated) : base((int)Prices.ROAD, isGenerated)
    {

    }
}
