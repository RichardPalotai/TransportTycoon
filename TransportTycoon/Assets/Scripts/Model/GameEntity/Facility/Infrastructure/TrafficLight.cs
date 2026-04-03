public sealed class TrafficLight : Facility
{
    /// <summary>
    /// Green interval in seconds.
    /// </summary>
    public LightColor Color { get; set; }
    private double _elapsedTime;
    public LightDirection Direction { get; private set; }
    public Crossroad Crossroad { get;  set; }
    public TrafficLight(bool isGenerated, LightDirection dir) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        _elapsedTime = 0.0;
        Crossroad = null;
        Direction = dir;
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

    public enum LightColor
    {
        RED,
        YELLOW,
        GREEN
    }

    public enum LightDirection
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
}

