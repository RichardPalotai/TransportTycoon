public sealed class TrafficLight : Facility
{
    /// <summary>
    /// Green interval in seconds.
    /// </summary>
    public LightColor Color { get; set; }
    private double _elapsedTime;
    public Direction FacingDirection { get; private set; }
    public Crossroad Crossroad { get;  set; }
    public TrafficLight(bool isGenerated, Direction dir) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        _elapsedTime = 0.0;
        Crossroad = null;
        FacingDirection = dir;
    }
    public TrafficLight(bool isGenerated) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        _elapsedTime = 0.0;
        Crossroad = null;
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
}

