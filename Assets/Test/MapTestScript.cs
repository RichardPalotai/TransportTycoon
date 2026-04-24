using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class MapTestScript
{
    [Test]
    public void GenerateMapTest()
    {
        var game = new Game();
        game.NewGame();
        Assert.AreEqual(100, game.Map.Size);

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Assert.IsNotNull(game.Map.GetTile(i, j));
                Assert.AreEqual(i, game.Map.GetTile(i, j).X);
                Assert.AreEqual(j, game.Map.GetTile(i, j).Y);
            }
        }

    }
    [Test]
    public void GetTileTest()
    {
        var game = new Game();
        game.NewGame();

        Assert.Throws<IndexOutOfRangeException>(() => game.Map.GetTile(-1, -1));
        Assert.Throws<IndexOutOfRangeException>(() => game.Map.GetTile(101, 101));
        Assert.Throws<IndexOutOfRangeException>(() => game.Map.GetTile(0, 101));
        Assert.Throws<IndexOutOfRangeException>(() => game.Map.GetTile(101, 0));
        Assert.DoesNotThrow(() => game.Map.GetTile(0, 0));
        Assert.DoesNotThrow(() => game.Map.GetTile(99, 0));
        Assert.DoesNotThrow(() => game.Map.GetTile(99, 99));
        Assert.DoesNotThrow(() => game.Map.GetTile(0, 99));
    }

    [Test]
    public void PlaceObjectTest()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new Factory<Steel>());
        game.Map.PlaceObject(game.Map.Size - 3, 0, new Mine<Iron>());
        game.Map.PlaceObject(0, game.Map.Size - 3, new Farm<Milk>());
        game.Map.PlaceObject(game.Map.Size - 3, game.Map.Size - 3, new LumberMill<Wood>());


        Assert.IsInstanceOf<Factory<Steel>>(game.Map.GetTile(0, 0).Entity);
        for (int i = 0; i < Map.GetAreaSize(game.Map.GetTile(0, 0).Entity); ++i)
        {
            for (int j = 0; j < Map.GetAreaSize(game.Map.GetTile(0, 0).Entity); ++j)
            {
                if (i == 0 && j == 0)
                    continue;
                Assert.AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(0, 0).Entity.ID);
                Assert.IsNull(game.Map.GetTile(i, j).Entity);
            }
        }


        Assert.IsInstanceOf<Mine<Iron>>(game.Map.GetTile(game.Map.Size - 3, 0).Entity);
        for (int i = game.Map.Size - 3; i < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 3, 0).Entity); ++i)
        {
            for (int j = 0; j < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 3, 0).Entity); ++j)
            {
                if (i == game.Map.Size - 3 && j == 0)
                    continue;
                Assert.AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(game.Map.Size - 3, 0).Entity.ID);
                Assert.IsNull(game.Map.GetTile(i, j).Entity);
            }
        }


        Assert.IsInstanceOf<Farm<Milk>>(game.Map.GetTile(0, game.Map.Size - 3).Entity);
        for (int i = 0; i < Map.GetAreaSize(game.Map.GetTile(0, game.Map.Size - 3).Entity); ++i)
        {
            for (int j = game.Map.Size - 3; j < Map.GetAreaSize(game.Map.GetTile(0, game.Map.Size - 3).Entity); ++j)
            {
                if (i == 0 && j == game.Map.Size - 3)
                    continue;
                Assert.AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(0, game.Map.Size - 3).Entity.ID);
                Assert.IsNull(game.Map.GetTile(i, j).Entity);
            }
        }

        Assert.IsInstanceOf<LumberMill<Wood>>(game.Map.GetTile(game.Map.Size - 3, game.Map.Size - 3).Entity);
        for (int i = game.Map.Size - 3; i < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 3, game.Map.Size - 3).Entity); ++i)
        {
            for (int j = game.Map.Size - 3; j < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 3, game.Map.Size - 3).Entity); ++j)
            {
                if (i == game.Map.Size - 3 && j == game.Map.Size - 3)
                    continue;
                Assert.AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(game.Map.Size - 3, game.Map.Size - 3).Entity.ID);
                Assert.IsNull(game.Map.GetTile(i, j).Entity);
            }
        }

        Assert.Throws<IndexOutOfRangeException>(() => game.Map.PlaceObject(game.Map.Size, game.Map.Size, new LumberMill<Wood>()));
        Assert.Throws<NotEnoughSpaceForObjectException>(() => game.Map.PlaceObject(game.Map.Size - 1, game.Map.Size - 1, new LumberMill<Wood>()));
    }

    [Test]
    public void AreaSizeTest()
    {
        Mine<Iron> mine = new();
        City city = new();
        TrafficLight trafficLight = new(true, Direction.NORTH);

        Assert.AreEqual(2, Map.GetAreaSize(mine));
        Assert.AreEqual(3, Map.GetAreaSize(city));
        Assert.AreEqual(1, Map.GetAreaSize(trafficLight));
    }

    [Test]
    public void CityPlacementTest()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new Factory<Steel>());
        game.Map.PlaceObject(game.Map.Size - 3, 0, new Mine<Iron>());
        game.Map.PlaceObject(0, game.Map.Size - 3, new Farm<Milk>());
        game.Map.PlaceObject(game.Map.Size - 3, game.Map.Size - 3, new LumberMill<Wood>());

        var city = new City();
        game.Map.PlaceObject(25, 25, city);


        Assert.IsInstanceOf<City>(game.Map.GetTile(25, 25).Entity);
        Assert.AreEqual(city.ID, game.Map.GetTile(25, 25).ObjectId);

        for (int i = 25; i < 25 + Map.GetAreaSize(game.Map.GetTile(25, 25).Entity); ++i)
        {
            for (int j = 25; j < 25 + Map.GetAreaSize(game.Map.GetTile(25, 25).Entity); ++j)
            {
                Logger.Log($"x={i},y={j} helyen {game.Map.GetTile(i, j).ObjectId} volt.");
                if (i == 25 && j == 25)
                    continue;

                if ((i == 25 && j == 27) || (i == 27 && j == 27) || (i == 27 && j == 25))
                    Assert.AreEqual(city.ID, game.Map.GetTile(i, j).ObjectId);
                else
                    Assert.IsInstanceOf<Road>(game.Map.GetTile(i, j).Entity);
            }
        }


    }
}
