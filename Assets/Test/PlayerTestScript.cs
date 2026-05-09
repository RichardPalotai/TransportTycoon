using System.Collections;
using System.Linq;
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
        Game.instance = game;
        DataAccess dataAccess = new();
        game.NewGame(dataAccess);
        var player = game.Player;
        int initialMoney = player.Money;
        player.Purchase(new Bus(40, game.Map));

        Assert.AreEqual(initialMoney - (int)Prices.BUS, player.Money);
        Assert.AreEqual(1, player.Vehicles.Count);
        for (int i = 0; i < 12; ++i)
        {
            player.Purchase(new Bus(40, game.Map));
        }
        Assert.AreEqual(13, player.Vehicles.Count);

        Assert.AreEqual(initialMoney - (int)Prices.BUS * 13, player.Money);
        Assert.Throws<NotEnoughMoneyException>(() => player.Purchase(new Bus(40, game.Map)));
        Assert.Throws<NotEnoughMoneyException>(() => player.Purchase(new Bus(40, game.Map)));
        Assert.Throws<NotEnoughMoneyException>(() => player.Purchase(new Bus(40, game.Map)));
        Assert.Throws<NotEnoughMoneyException>(() => player.Purchase(new Bus(40, game.Map)));
        Assert.Greater(player.Money, 0);

    }
    [Test]
    public void PlayerSellTest()
    {
        var game = new Game();
        Game.instance = game;
        DataAccess dataAccess = new();
        game.NewGame(dataAccess);
        var player = game.Player;
        int initialMoney = player.Money;
        for (int i = 0; i < 12; ++i)
        {
            player.Purchase(new Bus(40, game.Map));
        }
        Assert.AreEqual(12, player.Vehicles.Count);
        Assert.AreEqual(initialMoney - (int)Prices.BUS * 12, player.Money);

        var vehiclesWorth = player.Vehicles.First().Worth;
        player.SellItem(player.Vehicles.First());
        Assert.AreEqual(11, player.Vehicles.Count);
        Assert.AreEqual(initialMoney - (int)Prices.BUS * 12 + System.Convert.ToInt32(vehiclesWorth), player.Money);
    }
}
