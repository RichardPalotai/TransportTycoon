public abstract class Facility : IBuildable, ITradeable, IUpdateable
{
    public int Cost => throw new System.NotImplementedException();

    public void Build(Map map, Tile tile)
    {
        throw new System.NotImplementedException();
    }

    public void Demolish()
    {
        throw new System.NotImplementedException();
    }

    public void Purchase(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void Sell()
    {
        throw new System.NotImplementedException();
    }

    public void Update(double deltaTime)
    {
        throw new System.NotImplementedException();
    }
}