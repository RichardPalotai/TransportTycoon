public sealed class TrafficLight : Facility
{
    /// <summary>
    /// Green interval in seconds.
    /// </summary>
    public LightColor Color { get; set; }
    public Direction FacingDirection { get; set; } = Direction.SOUTH;
    public Crossroad Crossroad { get;  set; }
    public TrafficLight(bool isGenerated, Direction dir) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        Crossroad = null;
        FacingDirection = dir;
    }
    public TrafficLight(bool isGenerated) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        Crossroad = null;
    }
    public TrafficLight(bool isGenerated, int id, int x, int y, Direction dir) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        Crossroad = null;
        FacingDirection = dir;
        ID = id;
        X = x;
        Y = y;
    }
    /// <summary>
    /// GreenInterval increased by one
    /// </summary>
    public void GreenLightIncrement()
    {
        Crossroad?.GreenLightIncrement();
    }
    /// <summary>
    /// GreenInterval decreased by one
    /// </summary>
    public void GreenLightDecrement()
    {
        Crossroad?.GreenLightDecrement();
    }
    public override void Update(double deltaTime)
    {
        
    }
    public override void Sell(Player player)
    {
        base.Sell(player);
        Crossroad.TrafficLights.Remove(this);
    }

    public enum LightColor
    {
        RED,
        YELLOW,
        GREEN
    }
}

