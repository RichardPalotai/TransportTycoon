using System.Collections.Generic;

public class Crossroad
{
    public List<TrafficLight> TrafficLights { get; private set; }
    public Crossroad()
    {
        TrafficLights = new();
    }
}