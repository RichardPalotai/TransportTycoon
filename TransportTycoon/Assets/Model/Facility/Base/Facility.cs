public abstract class Facility : IBuildable, ITradeable, IUpdateable
{
    public int Cost => throw new System.NotImplementedException();
    
    public void Build(Map map, Tile tile)
    {
        
    }

    public void Demolish()
    {
        throw new System.NotImplementedException();
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
