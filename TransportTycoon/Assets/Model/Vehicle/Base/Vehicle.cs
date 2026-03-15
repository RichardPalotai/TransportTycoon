public abstract class Vehicle : ITradeable, IUpdateable
{
    public int Cost { get; }
    public int? X { get; private set; }
    public int? Y { get; private set; }
    public int PurchaseCost { get; init; }
    public double Speed { get; init; }
    public double Condition { get; private set; }

    public Vehicle(int cost, double speed)
    {
        
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