using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AlgorithmTestScript
{
    [Test]
    public void HasPathBetweenFacilitiesWithConnectedRoadsTest()
    {
        Game g = new Game();
        g.NewGame(new DataAccess());
        Game.instance = g;

        Mine<Iron> mine = new();
        Factory<Iron> factory = new();

        mine.Build(g.Map, g.Map.GetTile(0, 0));
        factory.Build(g.Map, g.Map.GetTile(0, 5));

        new Road(false).Build(g.Map, g.Map.GetTile(0, 2));
        new Road(false).Build(g.Map, g.Map.GetTile(0, 3));
        new Road(false).Build(g.Map, g.Map.GetTile(0, 4));

        bool result = PathFinder.HasPathBetweenFacilities(g.Map, mine, factory);

        Assert.IsTrue(result);
    }
    [Test]
    public void HasPathBetweenFacilitiesWithoutNeighborRoadsTest()
    {
        Game g = new Game();
        g.NewGame(new DataAccess());
        Game.instance = g;

        Mine<Iron> mine = new();
        Factory<Iron> factory = new();

        mine.Build(g.Map, g.Map.GetTile(1, 1));
        factory.Build(g.Map, g.Map.GetTile(8, 8));

        bool result = PathFinder.HasPathBetweenFacilities(g.Map, mine, factory);

        Assert.IsFalse(result);
    }


}
