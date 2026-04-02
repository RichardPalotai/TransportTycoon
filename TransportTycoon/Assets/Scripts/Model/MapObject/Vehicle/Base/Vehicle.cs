public abstract class Vehicle : MapObject, ITradeable, IUpdateable
{
    public int Cost { get; }
    /// <summary>
    /// X coord on the map
    /// </summary>
    public int? X { get; private set; }
    /// <summary>
    /// Y coord on the map
    /// </summary>
    public int? Y { get; private set; }
    public int PurchaseCost { get; init; }
    public double Speed { get; init; }
    private double _condition;
    /// <summary>
    /// 10 seconds
    /// </summary>
    private const double _updateCondInterval = 10.0;
    /// <summary>
    /// It accumulates the seconds during every update. If >= 10, then condition updates, and -= _updateCondInterval
    /// </summary>
    private double _elapsedTimeSinceLastUpdate;
    /// <summary>
    /// Vehicles condition in % ([0-100] double value).
    /// </summary>
    public double Condition
    {
        get => _condition;
        private set
        {
            if (value < 0 || value > 100)
                throw new VehicleConditionException("Condition only can be between 0 and 100!");
            _condition = value;
        }
    }

    protected Vehicle(int cost, double speed)
    {
        Condition = 100;
        Cost = cost;
        Speed = speed;
        _elapsedTimeSinceLastUpdate = 0.0;
    }
    /// <summary>
    /// Adds vehicle to the vehicles list
    /// </summary>
    /// <param name="player"></param>
    public void Purchase(Player player)
    {
        player.Vehicles.Add(this);
    }
    /// <summary>
    /// Removes vehicle from the vehicles list
    /// </summary>
    /// <param name="player"></param>
    public void Sell(Player player)
    {
        player.Vehicles.Remove(this);
    }
    /// <summary>
    /// Update runs in every _updateCondInterval (10) seconds
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(double deltaTime)
    {
        _elapsedTimeSinceLastUpdate += deltaTime;
        if (_elapsedTimeSinceLastUpdate >= _updateCondInterval)
        {
            _elapsedTimeSinceLastUpdate -= _updateCondInterval;
            Condition -= 0.01;
        }
    }
    
}