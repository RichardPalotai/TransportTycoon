using System;
using System.Linq;

public abstract class ProdFacility : Facility, IProdInteractable
{
    protected int producedPerSec;
    protected int producedCount = 0;
    protected double _prodBuffer;
    public int VehiclesWhoAreVisitingThisFacilityCount(Player player) =>
        player.Vehicles.Count(x => x.Route.Contains(this));
    protected ProdFacility(int cost, bool isGenerated) : base(cost, isGenerated) { }
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
#if DEBUG
            Logger.ProductionLog(this.GetType(), produced);
#endif
        }
    }
    public override void Update(double deltaTime)
    {
        base.Update(deltaTime); //doesn't do anything yet
        Produce(deltaTime);
    }
    public double Traffic(Player player)
    {
        return Math.Round((double)VehiclesWhoAreVisitingThisFacilityCount(player) /
            player.Vehicles.Count * 100.0, 2);
    }

    public int Interact(int freeCapacity)
    {
        if (freeCapacity == 0)
            return 0;
        if (freeCapacity < producedCount)
        {
            producedCount -= freeCapacity;
            return freeCapacity;
        }
        producedCount = 0;
        return producedCount;
    }
}
