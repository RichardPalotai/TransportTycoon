using System.Collections.Generic;

public class Crossroad : IUpdateable
{
    public List<TrafficLight> TrafficLights { get; private set; }
    public double GreenInterval { get; set; }
    private readonly double _yellowInterval = 2;
    private bool _northSouthGreen = true;
    public Crossroad()
    {
        TrafficLights = new();
        GreenInterval = 10;
    }
    public void GreenLightIncrement()
    {
        GreenInterval += 1;
    }
    public void GreenLightDecrement()
    {
        if (GreenInterval <= 1) return;
        GreenInterval -= 1;
    }
    public void Update(double deltaTime)
    {
        //TODO
    }

}