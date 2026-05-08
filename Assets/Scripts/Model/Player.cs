using System.Collections.Generic;

public sealed class Player
{
    public static Player instance;
    public int Money { get; set; }
    public List<Vehicle> Vehicles { get; private set; }
    public List<Facility> Facilities { get; set; }
    public Player()
    {
        Money = 2000;
        Vehicles = new();
        Facilities = new();
    }
    public void Purchase(ITradeable item)
    {
        if (item.Cost > Money)
        {
            throw new NotEnoughMoneyException($"Player does not have enough money to buy: {item.GetType().Name}");
        }

        item.Purchase(this);

        if (Game.instance.IsGameOver())
            Game.instance.GameOver();
    }
    public void SellItem(ITradeable item)
    {
        item.Sell(this);
    }


}