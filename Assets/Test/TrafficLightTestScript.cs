using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static NUnit.Framework.Assert;

public class TrafficLightTestScript
{
    [Test]
    public void TrafficLightGreenLightIncrementTest()
    {
        TrafficLight trafficLight = new(false);
        AreEqual(Direction.SOUTH, trafficLight.FacingDirection);

        trafficLight.Crossroad = new();
        AreEqual(10, trafficLight.Crossroad.GreenInterval);
        
        trafficLight.GreenLightIncrement();
        AreEqual(11, trafficLight.Crossroad.GreenInterval);

        trafficLight.GreenLightIncrement();
        AreEqual(12, trafficLight.Crossroad.GreenInterval);

        for (int i = 0; i < 10; i++)
        {
            trafficLight.GreenLightDecrement();
        }

        AreEqual(2, trafficLight.Crossroad.GreenInterval);
        trafficLight.GreenLightDecrement();
        AreEqual(1, trafficLight.Crossroad.GreenInterval);
        trafficLight.GreenLightDecrement();
        trafficLight.GreenLightDecrement();
        trafficLight.GreenLightDecrement();
        trafficLight.GreenLightDecrement();
        AreEqual(1, trafficLight.Crossroad.GreenInterval);


    }


}
