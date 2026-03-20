public abstract class Facility : IBuildable, ITradeable, IUpdateable
{
    public int Cost { get; }
    public bool IsGenerated { get; init; }
    protected Facility(int cost, bool isGenerated)
    {
        Cost = cost;
        IsGenerated = isGenerated;
    }

    public void Build(Map map, Tile tile)
    {
        map.SetTile(tile.X, tile.Y, this);
    }

    public void Purchase(Player player)
    {
        player.Facilities.Add(this);
    }

    public void Sell(Player player)
    {
        player.Facilities.Remove(this);
    }

    public void Update(double deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
