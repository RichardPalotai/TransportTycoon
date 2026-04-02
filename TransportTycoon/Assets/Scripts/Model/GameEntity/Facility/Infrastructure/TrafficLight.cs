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
    public TrafficLight(bool isGenerated, double greenInterval) : base((int)Prices.TRAFFICLIGHT, isGenerated)
    {
        GreenInterval = greenInterval;
    }

    public void Synchronize()
    {
        
    }
    public override void Update(double deltaTime)
    {
        
    }
}