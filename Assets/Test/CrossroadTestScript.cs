using NUnit.Framework;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TestTools;
using static NUnit.Framework.Assert;

public class CrossroadTestScript
{
    [Test]
    public void CrossroadConstructorTest()
    {
        Crossroad crossroad = new();
        NotNull(crossroad.TrafficLights);
        AreEqual(10, crossroad.GreenInterval);
    }

    [Test]
    public void CrossroadUpdateTest()
    {
        Crossroad crossroad = new();

        var north = new TrafficLight(false, Direction.NORTH);
        var south = new TrafficLight(false, Direction.SOUTH);
        var east = new TrafficLight(false, Direction.EAST);
        var west = new TrafficLight(false, Direction.WEST);

        crossroad.TrafficLights.Add(north);
        crossroad.TrafficLights.Add(south);
        crossroad.TrafficLights.Add(east);
        crossroad.TrafficLights.Add(west);

        crossroad.Update(0.1);

        AreEqual(TrafficLight.LightColor.GREEN, north.Color);
        AreEqual(TrafficLight.LightColor.RED, east.Color);

    }

    [Test]
    public void CrossroadUpdateAfterGreenIntervalAllYellowTest()
    {
        var crossroad = new Crossroad();

        var north = new TrafficLight(false, Direction.NORTH);
        var east = new TrafficLight(false, Direction.SOUTH);

        crossroad.TrafficLights.Add(north);
        crossroad.TrafficLights.Add(east);

        crossroad.Update(crossroad.GreenInterval + 0.1);

        AreEqual(TrafficLight.LightColor.YELLOW, north.Color);
        AreEqual(TrafficLight.LightColor.YELLOW, east.Color);
    }

    [Test]
    public void CrossroadUpdateAfterFullCycleSwitchesDirectionTest()
    {
        var crossroad = new Crossroad();

        var north = new TrafficLight(false, Direction.NORTH);
        var south = new TrafficLight(false, Direction.SOUTH);
        var east = new TrafficLight(false, Direction.EAST);
        var west = new TrafficLight(false, Direction.WEST);

        crossroad.TrafficLights.Add(north);
        crossroad.TrafficLights.Add(south);
        crossroad.TrafficLights.Add(east);
        crossroad.TrafficLights.Add(west);

        //Beginning of green
        crossroad.Update(0.1);
        AreEqual(TrafficLight.LightColor.GREEN, north.Color);
        AreEqual(TrafficLight.LightColor.RED, east.Color);

        //Then go into yellow state
        crossroad.Update(crossroad.GreenInterval);
        AreEqual(TrafficLight.LightColor.YELLOW, north.Color);
        AreEqual(TrafficLight.LightColor.YELLOW, east.Color);

        //Switch
        crossroad.Update(2 + 0.001);
        AreEqual(TrafficLight.LightColor.RED, north.Color);
        AreEqual(TrafficLight.LightColor.GREEN, east.Color);
    }

    [Test]
    public void CrossroadUpdateWithModifiedGreenIntervalTest()
    {
        var crossroad = new Crossroad();

        var north = new TrafficLight(false, Direction.NORTH);
        var south = new TrafficLight(false, Direction.SOUTH);
        var east = new TrafficLight(false, Direction.EAST);
        var west = new TrafficLight(false, Direction.WEST);

        crossroad.TrafficLights.Add(north);
        crossroad.TrafficLights.Add(south);
        crossroad.TrafficLights.Add(east);
        crossroad.TrafficLights.Add(west);

        crossroad.GreenLightIncrement();
        crossroad.GreenLightIncrement();

        // GREEN
        crossroad.Update(0.1);
        AreEqual(TrafficLight.LightColor.GREEN, north.Color);
        AreEqual(TrafficLight.LightColor.RED, east.Color);

        // YELLOW
        crossroad.Update(crossroad.GreenInterval);
        AreEqual(TrafficLight.LightColor.YELLOW, north.Color);

        // Switch
        crossroad.Update(2 + 0.001);
        AreEqual(TrafficLight.LightColor.GREEN, east.Color);
        AreEqual(TrafficLight.LightColor.RED, north.Color);
    }

    [Test]
    public void CrossroadGreenIntervalDecrementTest()
    {
        var crossroad = new Crossroad();

        for (int i = 0; i < 20; i++)
            crossroad.GreenLightDecrement();

        AreEqual(1, crossroad.GreenInterval);

        var north = new TrafficLight(false, Direction.NORTH);
        var east = new TrafficLight(false, Direction.EAST);

        crossroad.TrafficLights.Add(north);
        crossroad.TrafficLights.Add(east);

        crossroad.Update(0.1);
        AreEqual(TrafficLight.LightColor.GREEN, north.Color);

        crossroad.Update(1);
        AreEqual(TrafficLight.LightColor.YELLOW, north.Color);

        crossroad.Update(2 + 0.001);
        AreEqual(TrafficLight.LightColor.GREEN, east.Color);
    }

    [Test]
    public void CrossroadUpdateWithLargeUpdateDeltaTimeTest()
    {
        var crossroad = new Crossroad();

        var north = new TrafficLight(false, Direction.NORTH);
        var east = new TrafficLight(false, Direction.EAST);

        crossroad.TrafficLights.Add(north);
        crossroad.TrafficLights.Add(east);

        crossroad.Update(100);

        AreEqual(TrafficLight.LightColor.GREEN, east.Color);
        AreEqual(TrafficLight.LightColor.RED, north.Color);
    }
}
