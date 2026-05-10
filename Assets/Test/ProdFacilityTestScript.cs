using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static NUnit.Framework.Assert;
public class ProdFacilityTestScript
{
    [Test]
    public void ProdFacilityFactoryUpdateProduceTest()
    {
        var factory = new Factory<Steel>();

        factory.Update(1.0);
        AreEqual(12, factory.ProducedCount);
    }
    [Test]
    public void ProdFacilityFactoryUpdateBufferTest()
    {
        var factory = new Factory<Steel>();

        factory.Update(0.5);
        factory.Update(0.5);

        AreEqual(12, factory.ProducedCount);
    }
    [Test]
    public void ProdFacilityFactoryUpdateForALotOfSmallStepsTest()
    {
        var factory = new Factory<Steel>();

        for (int i = 0; i < 100; i++)
            factory.Update(0.01);

        AreEqual(12, factory.ProducedCount);
    }
    [Test]
    public void ProdFacilityFactoryProdInteractTest()
    {
        var factory = new Factory<Steel>();

        factory.Update(1.0);

        int taken = factory.ProdInteract(5);

        AreEqual(5, taken);
        AreEqual(7, factory.ProducedCount);
    }
    [Test]
    public void ProdFacilityTrafficTest()
    {
        Game game = new();
        Game.instance = game;
        game.NewGame(new DataAccess());
        var factory = new Factory<Steel>();
        Car c = new(5, game.Map);
        game.Player.Purchase(c);
        List<Facility> f = new List<Facility>() { factory };
        Queue r = new Queue(f);
        AreEqual(0, factory.Traffic(game.Player));
    }

    [Test]
    public void BusStopTestScript()
    {
        BusStop stop = new BusStop(false, 1, 1, 1);
        IsNotNull(stop);
        False(stop.IsGenerated);
        AreEqual(1, stop.X);
        AreEqual(1, stop.Y);
        AreEqual(1, stop.ID);
    }

    [Test]
    public void BusStopPassInteractFixedSeedTest()
    {
        BusStop stop = new BusStop(false, 1, 1, 1);
        int rand = stop.PassInteractFixedSeed(10);
        bool isBetween = rand <= 10 && rand >= -10;
        True(isBetween);
    }
    [Test]
    public void RoadTestScript()
    {
        Road road = new(false, 1, 1, 1);
        IsNotNull(road);
        False(road.IsGenerated);
        AreEqual(1, road.X);
        AreEqual(1, road.Y);
        AreEqual(1, road.ID);
    }
    [Test]
    public void FactoryPersistenceConstructorScript()
    {
        Factory<Paper> f = new Factory<Paper>(1, 2, 3, false);
        AreEqual(1, f.ID);
        AreEqual(2, f.X);
        AreEqual(3, f.Y);
        False(f.IsGenerated);
    }
    [Test]
    public void FarmPersistenceConstructorScript()
    {
        Farm<Milk> f = new Farm<Milk>(1, 2, 3, false);
        AreEqual(1, f.ID);
        AreEqual(2, f.X);
        AreEqual(3, f.Y);
        False(f.IsGenerated);
    }
    [Test]
    public void MillPersistenceConstructorScript()
    {
        LumberMill<Wood> f = new(1, 2, 3, false);
        AreEqual(1, f.ID);
        AreEqual(2, f.X);
        AreEqual(3, f.Y);
        False(f.IsGenerated);
    }
    [Test]
    public void MinePersistenceConstructorScript()
    {
        Mine<Iron> f = new(1, 2, 3, false);
        AreEqual(1, f.ID);
        AreEqual(2, f.X);
        AreEqual(3, f.Y);
        False(f.IsGenerated);
    }
}
