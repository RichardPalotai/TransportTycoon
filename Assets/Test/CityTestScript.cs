using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

public class CityTestScript
{
    [Test]
    public void CityResourceDeliveredTest()
    {
        City city = new City();

        city.DeliverResource(Iron.Instance, 1000);

        Assert.AreEqual(1000, city.Need.Values.First());
        Assert.AreEqual(1, city.Need.Count);
    }
    [Test]
    public void CityDeliverTooMuchResource()
    {
        City city = new City();
        Cheese cheese = Cheese.Instance;

        city.DeliverResource(cheese, 4000);
        city.DeliverResource(cheese, 2000);

        Assert.AreEqual(5000, city.Need[cheese]);
    }

    [Test]
    public void CitySatisfactionWithMultipleNeedsAverageTest()
    {
        City city = new City();

        Cheese cheese = Cheese.Instance;
        Iron iron = Iron.Instance;

        city.DeliverResource(cheese, 2500); // 50%
        city.DeliverResource(iron, 5000); // skip

        double satisfaction = city.Satisfaction();

        Assert.AreEqual(50, satisfaction);
    }
    [Test]
    public void CityUpdateAfterOneDayFoodNeedIsDividedByThreeTest()
    {
        City city = new City();
        Iron iron = Iron.Instance;

        city.DeliverResource(iron, 3000);

        city.Update(City._inGameDayInSecs);

        Assert.AreEqual(1000, city.Need[iron]);
    }

}
