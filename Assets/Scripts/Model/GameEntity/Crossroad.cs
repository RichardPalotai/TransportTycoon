using System;
using System.Collections.Generic;

public class Crossroad : IUpdateable
{
    public List<TrafficLight> TrafficLights { get; private set; }
    public double GreenInterval { get; set; }
    private readonly double _yellowInterval = 2;
    private bool _northSouthGreen = true;
    private double _timer;
    private CurrentGreens _greens;
    public Crossroad()
    {
        TrafficLights = new();
        GreenInterval = 10;
        _timer = 0;
        _greens = CurrentGreens.NORTHSOUTH;
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
        _timer += deltaTime;
        if (_timer > GreenInterval + _yellowInterval)
        {
            if (_greens == CurrentGreens.NORTHSOUTH)
                _greens = CurrentGreens.EASTWEST;
            else
                _greens = CurrentGreens.NORTHSOUTH;
            _timer = 0;
        }
        ApplyLights();
    }
    /// <summary>
    /// Sets the color of the lights in the crossroad
    /// </summary>
    // Simpler is better (maybe)
    private void ApplyLights() 
    {
        if (_timer < GreenInterval) //Green state
        {
            foreach (var light in TrafficLights)
            {
                if (_greens == CurrentGreens.NORTHSOUTH)
                {
                    if (light.FacingDirection == Direction.NORTH ||
                        light.FacingDirection == Direction.SOUTH)
                    {
                        light.Color = TrafficLight.LightColor.GREEN;
                    }
                    else
                    {
                        light.Color = TrafficLight.LightColor.RED;
                    }
                }
                else
                {
                    if (light.FacingDirection == Direction.NORTH ||
                        light.FacingDirection == Direction.SOUTH)
                    {
                        light.Color = TrafficLight.LightColor.RED;
                    }
                    else
                    {
                        light.Color = TrafficLight.LightColor.GREEN;
                    }
                }
            }
        }
        else //Yellow state
        {
            foreach (var light in TrafficLights)
            {
                light.Color = TrafficLight.LightColor.YELLOW;
            }
        }
    }

    private enum CurrentGreens
    {
        NORTHSOUTH,
        EASTWEST
    }
}