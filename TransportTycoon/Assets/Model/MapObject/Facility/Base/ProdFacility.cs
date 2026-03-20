public abstract class ProdFacility : Facility
{
    protected ProdFacility(int cost, bool isGenerated) : base(cost, isGenerated) {}
    public override void Build(Map map, Tile tile)
    {
        map.PlaceObject(tile.X, tile.Y, this);
    }
}
