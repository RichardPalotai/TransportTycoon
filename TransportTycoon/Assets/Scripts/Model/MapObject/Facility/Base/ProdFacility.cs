public abstract class ProdFacility : Facility
{
    protected int producedPerSec;
    protected double producedCount = 0;
    protected double _prodBuffer;
    protected ProdFacility(int cost, bool isGenerated) : base(cost, isGenerated) {}
    public override void Build(Map map, Tile tile)
    {
        map.PlaceObject(tile.X, tile.Y, this);
    }
    protected void Produce(double deltaTime)
    {
        _prodBuffer += producedPerSec * deltaTime;
        int produced = (int)_prodBuffer;

        if (produced > 0)
        {
            _prodBuffer -= produced;
            producedCount += produced;
            Logger.ProductionLog(this.GetType(), produced);
        }
    }
    public override void Update(double deltaTime)
    {
        base.Update(deltaTime); //doesn't do anything yet
        Produce(deltaTime);
    }
}
