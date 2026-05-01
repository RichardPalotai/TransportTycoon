using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using static NUnit.Framework.Assert;
public class MapTestScript
{
    [Test]
    public void GenerateMapTest()
    {
        var game = new Game();
        game.NewGame();
        AreEqual(100, game.Map.Size);

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                IsNotNull(game.Map.GetTile(i, j));
                AreEqual(i, game.Map.GetTile(i, j).X);
                AreEqual(j, game.Map.GetTile(i, j).Y);
            }
        }

    }
    [Test]
    public void GetTileTest()
    {
        var game = new Game();
        game.NewGame();

        Throws<IndexOutOfRangeException>(() => game.Map.GetTile(-1, -1));
        Throws<IndexOutOfRangeException>(() => game.Map.GetTile(101, 101));
        Throws<IndexOutOfRangeException>(() => game.Map.GetTile(0, 101));
        Throws<IndexOutOfRangeException>(() => game.Map.GetTile(101, 0));
        DoesNotThrow(() => game.Map.GetTile(0, 0));
        DoesNotThrow(() => game.Map.GetTile(99, 0));
        DoesNotThrow(() => game.Map.GetTile(99, 99));
        DoesNotThrow(() => game.Map.GetTile(0, 99));
    }

    [Test]
    public void PlaceObjectTest()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new Factory<Steel>());
        game.Map.PlaceObject(game.Map.Size - 2, 0, new Mine<Iron>());
        game.Map.PlaceObject(0, game.Map.Size - 2, new Farm<Milk>());
        game.Map.PlaceObject(game.Map.Size - 2, game.Map.Size - 2, new LumberMill<Wood>());


        IsInstanceOf<Factory<Steel>>(game.Map.GetTile(0, 0).Entity);
        for (int i = 0; i < Map.GetAreaSize(game.Map.GetTile(0, 0).Entity); ++i)
        {
            for (int j = 0; j < Map.GetAreaSize(game.Map.GetTile(0, 0).Entity); ++j)
            {
                if (i == 0 && j == 0)
                    continue;
                AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(0, 0).Entity.ID);
                IsNull(game.Map.GetTile(i, j).Entity);
            }
        }


        IsInstanceOf<Mine<Iron>>(game.Map.GetTile(game.Map.Size - 2, 0).Entity);
        for (int i = game.Map.Size - 2; i < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 2, 0).Entity); ++i)
        {
            for (int j = 0; j < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 2, 0).Entity); ++j)
            {
                if (i == game.Map.Size - 2 && j == 0)
                    continue;
                AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(game.Map.Size - 2, 0).Entity.ID);
                IsNull(game.Map.GetTile(i, j).Entity);
            }
        }


        IsInstanceOf<Farm<Milk>>(game.Map.GetTile(0, game.Map.Size - 2).Entity);
        for (int i = 0; i < Map.GetAreaSize(game.Map.GetTile(0, game.Map.Size - 2).Entity); ++i)
        {
            for (int j = game.Map.Size - 2; j < Map.GetAreaSize(game.Map.GetTile(0, game.Map.Size - 2).Entity); ++j)
            {
                if (i == 0 && j == game.Map.Size - 2)
                    continue;
                AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(0, game.Map.Size - 2).Entity.ID);
                IsNull(game.Map.GetTile(i, j).Entity);
            }
        }

        IsInstanceOf<LumberMill<Wood>>(game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 2).Entity);
        for (int i = game.Map.Size - 2; i < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 2).Entity); ++i)
        {
            for (int j = game.Map.Size - 2; j < Map.GetAreaSize(game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 2).Entity); ++j)
            {
                if (i == game.Map.Size - 2 && j == game.Map.Size - 2)
                    continue;
                AreEqual(game.Map.GetTile(i, j).ObjectId, game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 2).Entity.ID);
                IsNull(game.Map.GetTile(i, j).Entity);
            }
        }

        Throws<IndexOutOfRangeException>(() => game.Map.PlaceObject(game.Map.Size, game.Map.Size, new LumberMill<Wood>()));
        Throws<NotEnoughSpaceForObjectException>(() => game.Map.PlaceObject(game.Map.Size - 1, game.Map.Size - 1, new LumberMill<Wood>()));
    }

    [Test]
    public void AreaSizeTest()
    {
        Mine<Iron> mine = new();
        City city = new();
        TrafficLight trafficLight = new(true, Direction.NORTH);

        AreEqual(2, Map.GetAreaSize(mine));
        AreEqual(3, Map.GetAreaSize(city));
        AreEqual(1, Map.GetAreaSize(trafficLight));
    }

    [Test]
    public void CityPlacementTest()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new Factory<Steel>());
        game.Map.PlaceObject(game.Map.Size - 2, 0, new Mine<Iron>());
        game.Map.PlaceObject(0, game.Map.Size - 2, new Farm<Milk>());
        game.Map.PlaceObject(game.Map.Size - 2, game.Map.Size - 2, new LumberMill<Wood>());

        var city = new City();
        DoesNotThrow(() => game.Map.PlaceObject(25, 25, city));
        Throws<NotEnoughSpaceForObjectException>(() => game.Map.PlaceObject(25, 25, city));


        IsInstanceOf<City>(game.Map.GetTile(25, 25).Entity);
        AreEqual(city.ID, game.Map.GetTile(25, 25).ObjectId);

        for (int i = 25; i < 25 + Map.GetAreaSize(game.Map.GetTile(25, 25).Entity); ++i)
        {
            for (int j = 25; j < 25 + Map.GetAreaSize(game.Map.GetTile(25, 25).Entity); ++j)
            {
                //Logger.Log($"x={i},y={j} helyen {game.Map.GetTile(i, j).ObjectId} volt.");
                if (i == 25 && j == 25)
                    continue;

                if ((i == 25 && j == 27) || (i == 27 && j == 27) || (i == 27 && j == 25))
                    AreEqual(city.ID, game.Map.GetTile(i, j).ObjectId);
                else
                    IsInstanceOf<Road>(game.Map.GetTile(i, j).Entity);
            }
        }

        DoesNotThrow(() => game.Map.PlaceObject(94, 94, city));
        Throws<NotEnoughSpaceForObjectException>(() => game.Map.PlaceObject(95, 95, city));
    }
    [Test]
    public void IsFreeTest()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new Factory<Steel>()); //2x2
        game.Map.PlaceObject(game.Map.Size - 2, 0, new Mine<Iron>()); //2x2
        game.Map.PlaceObject(0, game.Map.Size - 2, new Farm<Milk>()); //2x2
        game.Map.PlaceObject(game.Map.Size - 2, game.Map.Size - 2, new LumberMill<Wood>()); //2x2
        var city = new City();
        game.Map.PlaceObject(25, 25, city);//3x3
        game.Map.PlaceObject(50, 50, city);//3x3

        int notFreeTilesCount = 0;
        for (int i = 0; i < game.Map.Size; i++)
        {
            for (int j = 0; j < game.Map.Size; j++)
            {
                if (!game.Map.GetTile(i, j).IsFree)
                    notFreeTilesCount++;
            }
        }
        AreEqual(34, notFreeTilesCount);

        //Logger.FreeMap(game.Map);
    }

    [Test]
    public void MarkAreaTilesWithIdTest()
    {
        var game = new Game();
        game.NewGame();
        var fact = new Factory<Steel>();
        var mine = new Mine<Iron>();
        var farm = new Farm<Milk>();
        var mill = new LumberMill<Wood>();
        game.Map.PlaceObject(0, 0, fact); //2x2
        game.Map.PlaceObject(game.Map.Size - 2, 0, mine); //2x2
        game.Map.PlaceObject(0, game.Map.Size - 2, farm); //2x2
        game.Map.PlaceObject(game.Map.Size - 2, game.Map.Size - 2, mill); //2x2

        IsInstanceOf<Factory<Steel>>(game.Map.GetTile(0, 0).Entity);
        AreEqual(fact.ID, game.Map.GetTile(0, 0).ObjectId);
        IsNotNull(game.Map.GetTile(0, 0).Entity);
        AreEqual(fact.ID, game.Map.GetTile(0, 1).ObjectId);
        IsNull(game.Map.GetTile(0, 1).Entity);
        AreEqual(fact.ID, game.Map.GetTile(1, 0).ObjectId);
        IsNull(game.Map.GetTile(1, 0).Entity);
        AreEqual(fact.ID, game.Map.GetTile(1, 1).ObjectId);
        IsNull(game.Map.GetTile(1, 1).Entity);

        IsInstanceOf<Mine<Iron>>(game.Map.GetTile(game.Map.Size - 2, 0).Entity);
        AreEqual(mine.ID, game.Map.GetTile(game.Map.Size - 2, 0).ObjectId);
        IsNotNull(game.Map.GetTile(game.Map.Size - 2, 0).Entity);
        AreEqual(mine.ID, game.Map.GetTile(game.Map.Size - 2, 1).ObjectId);
        IsNull(game.Map.GetTile(game.Map.Size - 2, 1).Entity);
        AreEqual(mine.ID, game.Map.GetTile(game.Map.Size - 1, 0).ObjectId);
        IsNull(game.Map.GetTile(game.Map.Size - 1, 0).Entity);
        AreEqual(mine.ID, game.Map.GetTile(game.Map.Size - 1, 1).ObjectId);
        IsNull(game.Map.GetTile(game.Map.Size - 1, 1).Entity);

        IsInstanceOf<Farm<Milk>>(game.Map.GetTile(0, game.Map.Size - 2).Entity);
        AreEqual(farm.ID, game.Map.GetTile(0, game.Map.Size - 2).ObjectId);
        IsNotNull(game.Map.GetTile(0, game.Map.Size - 2).Entity);
        AreEqual(farm.ID, game.Map.GetTile(0, game.Map.Size - 1).ObjectId);
        IsNull(game.Map.GetTile(0, game.Map.Size - 1).Entity);
        AreEqual(farm.ID, game.Map.GetTile(1, game.Map.Size - 2).ObjectId);
        IsNull(game.Map.GetTile(1, game.Map.Size - 2).Entity);
        AreEqual(farm.ID, game.Map.GetTile(1, game.Map.Size - 1).ObjectId);
        IsNull(game.Map.GetTile(1, game.Map.Size - 1).Entity);

        IsInstanceOf<LumberMill<Wood>>(game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 2).Entity);
        AreEqual(mill.ID, game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 2).ObjectId);
        IsNotNull(game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 2).Entity);
        AreEqual(mill.ID, game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 1).ObjectId);
        IsNull(game.Map.GetTile(game.Map.Size - 2, game.Map.Size - 1).Entity);
        AreEqual(mill.ID, game.Map.GetTile(game.Map.Size - 1, game.Map.Size - 2).ObjectId);
        IsNull(game.Map.GetTile(game.Map.Size - 1, game.Map.Size - 2).Entity);
        AreEqual(mill.ID, game.Map.GetTile(game.Map.Size - 1, game.Map.Size - 1).ObjectId);
        IsNull(game.Map.GetTile(game.Map.Size - 1, game.Map.Size - 1).Entity);



    }

    [Test]
    public void GetFacilityNeighborRoadsWithSingleRoadTest()
    {
        var game = new Game();
        game.NewGame();

        game.Map.PlaceObject(0, 0, new Factory<Steel>());
        game.Map.PlaceObject(2, 0, new Road(false));

        var res = game.Map.GetFacilityNeighborRoads(game.Map.GetTile(0, 0).Entity as Facility);
        AreEqual(1, res.Count);
        AreEqual((2, 0), res.First());
    }

    [Test]
    public void GetFacilityNeighborRoadsWithMultipleRoads2x2Test()
    {
        var game = new Game();
        game.NewGame();

        game.Map.PlaceObject(0, 0, new Factory<Steel>());
        game.Map.PlaceObject(2, 0, new Road(false));

        var res = game.Map.GetFacilityNeighborRoads(game.Map.GetTile(0, 0).Entity as Facility);
        AreEqual(1, res.Count);
        AreEqual((2, 0), res.First());

        game.Map.PlaceObject(0, 2, new Road(false));
        game.Map.PlaceObject(2, 1, new Road(false));
        res = game.Map.GetFacilityNeighborRoads(game.Map.GetTile(0, 0).Entity as Facility);

        AreEqual(3, res.Count);
        That(res, Is.EquivalentTo(new List<(int x, int y)>() { (2, 0), (0, 2), (2, 1) }));

        game.Map.PlaceObject(2, 2, new Road(false));

        res = game.Map.GetFacilityNeighborRoads(game.Map.GetTile(0, 0).Entity as Facility);

        AreEqual(3, res.Count);
        That(res, Is.EquivalentTo(new List<(int x, int y)>() { (2, 0), (0, 2), (2, 1) }));
    }
    [Test]
    public void GetFacilityNeighborRoads3x3Test()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new City());
        game.Map.PlaceObject(3, 0, new Road(false));


        var res = game.Map.GetFacilityNeighborRoads(game.Map.GetTile(0, 0).Entity as Facility);

        AreEqual(6, res.Count);
        That(res, Is.EquivalentTo(new List<(int x, int y)>() { (0, 1), (1, 0), (1, 1), (1, 2), (2, 1), (3, 0) }));

        game.Map.PlaceObject(0, 3, new Road(false));
        game.Map.PlaceObject(3, 1, new Road(false));

        res = game.Map.GetFacilityNeighborRoads(game.Map.GetTile(0, 0).Entity as Facility);

        AreEqual(8, res.Count);
        That(res, Is.EquivalentTo(new List<(int x, int y)>() { (0, 1), (1, 0), (1, 1), (0, 3), (1, 2), (2, 1), (3, 0), (3, 1) }));

        game.Map.PlaceObject(3, 2, new Road(false));

        res = game.Map.GetFacilityNeighborRoads(game.Map.GetTile(0, 0).Entity as Facility);

        AreEqual(9, res.Count);
        That(res, Is.EquivalentTo(new List<(int x, int y)>() { (0, 1), (1, 0), (1, 1), (0, 3), (1, 2), (2, 1), (3, 0), (3, 1), (3, 2) }));
    }

    [Test]
    public void IsCrossRoadTest()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new City());

        False(game.Map.IsCrossRoad(0, 0));
        False(game.Map.IsCrossRoad(1, 0));
        False(game.Map.IsCrossRoad(2, 0));
        False(game.Map.IsCrossRoad(0, 1));
        True(game.Map.IsCrossRoad(1, 1));
        False(game.Map.IsCrossRoad(2, 1));
        False(game.Map.IsCrossRoad(0, 2));
        False(game.Map.IsCrossRoad(1, 2));
        False(game.Map.IsCrossRoad(2, 2));
    }

    [Test]
    public void PlaceCityRoadsTest()
    {
        var game = new Game();
        game.NewGame();
        game.Map.PlaceObject(0, 0, new City());
        var objCoordX = 0;
        var objCoordY = 0;
        for (int i = objCoordX + 0; i < 3; i++)
        {
            for (int j = objCoordY + 0; j < 3; j++)
            {
                if (i == objCoordX && j == objCoordY || i == objCoordX + 2 && j == objCoordY ||
                    i == objCoordX && j == objCoordY + 2 || i == objCoordX + 2 && j == objCoordY + 2)
                {
                    IsNotInstanceOf<Road>(game.Map.GetTile(i, j).Entity);
                }
                else
                {
                    IsInstanceOf<Road>(game.Map.GetTile(i, j).Entity);
                }
            }
        }
    }

    [Test]
    public void AddToCrossRoadIfNeededTest()
    {
        var game = new Game();
        game.NewGame();

    }
}
