public sealed class Road : Facility
{
    public bool IsCrossRoad { get; set; } = false;
    public Road(bool isGenerated) : base((int)Prices.ROAD, isGenerated)
    {

    }
    public Road(bool isGenerated, int id, int x, int y) : base((int)Prices.ROAD, isGenerated, id, x, y)
    {

    }
}
