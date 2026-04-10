public abstract class Facility : GameEntity, IBuildable, ITradeable, IUpdateable
{
    public int Cost { get; protected set;  }
    public bool IsGenerated { get; init; }
    public int X { get; private set; }
    public int Y { get; private set; }
    protected Facility(int cost, bool isGenerated)
    {
        Cost = cost;
        IsGenerated = isGenerated;
    }

    public virtual void Build(Map map, Tile tile)
    {
        X = tile.X;
        Y = tile.Y;
        map.PlaceObject(tile.X, tile.Y, this);
    }

    public void Purchase(Player player)
    {
        player.Facilities.Add(this);
        player.Money -= Cost;
    }

    public void Sell(Player player)
    {
        player.Facilities.Remove(this);
        player.Money += Cost * 0.8;
    }

    public virtual void Update(double deltaTime)
    {
        
    }

}
