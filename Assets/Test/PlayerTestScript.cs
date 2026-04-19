using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTestScript
{
    [Test]
    public void PlayerInitializationTest()
    {
        var player = new Player();
        Player.instance = player;

        Assert.IsNotNull(player);
        Assert.IsNotNull(Player.instance);
        Assert.AreEqual(2000, player.Money);
        Assert.IsNotNull(player.Facilities);
        Assert.IsNotNull(player.Vehicles);
        Assert.AreEqual(0, player.Facilities.Count);
        Assert.AreEqual(0, player.Vehicles.Count);

    }

    [Test]
    public void PlayerPurchaseTest()
    {
        var game = new Game();
        var player = new Player();
        int initialMoney = player.Money;
        player.Purchase(new Bus(40, game.Map));

        Assert.AreEqual(initialMoney - (int)Prices.BUS, player.Money);
        Assert.AreEqual(1, player.Vehicles.Count);
    }


}
