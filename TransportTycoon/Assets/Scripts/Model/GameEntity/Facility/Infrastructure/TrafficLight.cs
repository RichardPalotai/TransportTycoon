public sealed class TrafficLight : Facility
{
    private double _greenInterval;
    /// <summary>
    /// Green interval in seconds.
    /// </summary>
    public double GreenInterval
    {
        get => _greenInterval;
        set
        {
            if (value < 1)
            {
                throw new InvalidGreenIntervalException("Green interval must be greater or equal to 1!");
            }
            _greenInterval = value;
        }
    }
    public LightColor Color { get; private set; }
    private double _elapsedTime;
    public LightDirection Direction { get; private set; }
    public TrafficLight(bool isGenerated, double greenInterval, LightDirection dir) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        GreenInterval = greenInterval;
        _elapsedTime = 0.0;
        Direction = dir;
    }

    public void Synchronize()
    {
        // TODO
    }
    /// <summary>
    /// GreenInterval increased by one
    /// </summary>
    public void GreenLightIncrement()
    {
        GreenInterval += 1;
    }
    /// <summary>
    /// GreenInterval decreased by one
    /// </summary>
    public void GreenLightDecrement()
    {
        GreenInterval -= 1;
    }
    public override void Update(double deltaTime)
    {
        _elapsedTime += deltaTime;
        // [0..GreenInterval) -> green
        if (_elapsedTime < GreenInterval)
        {
            Color = LightColor.GREEN;
        }
        // [GreenInterval..GreenInterval+2) -> yellow
        else if (_elapsedTime < GreenInterval + 2)
        {
            Color = LightColor.YELLOW;
        }
        // [GreenInterval+2..GreenInterval + 2 + GreenInterval) -> red
        else if (_elapsedTime < GreenInterval + 2 + GreenInterval)
        {
            Color = LightColor.RED;
        }
        else
        {
            _elapsedTime = 0;
        }
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

