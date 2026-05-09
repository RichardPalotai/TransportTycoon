using System.Collections.Generic;
using NUnit.Framework;
using static NUnit.Framework.Assert;
public class VehicleTestScript
{
    [Test]
    public void VehicleMovement()
    {
        Game game = new Game();
        game.NewGame(new DataAccess());
        Game.instance = game;
        Player.instance = game.Player;
        game.AccountBalance = 100000000;

        game.Map.PlaceObject(10, 10, new Road(false));
        game.Map.PlaceObject(11, 10, new Road(false));
        game.Map.PlaceObject(12, 10, new Road(false));
        game.Map.PlaceObject(13, 10, new Road(false));
        game.Map.PlaceObject(14, 10, new Road(false));
        game.Map.PlaceObject(15, 10, new Road(false));
        game.Map.PlaceObject(16, 10, new Road(false));

        var fac = new Factory<Paper>();
        var mine = new Mine<Iron>();
        game.Player.Purchase(fac);
        game.Player.Purchase(mine);
        game.Map.PlaceObject(10, 11, fac);
        game.Map.PlaceObject(16, 11, mine);

        Car c = new Car(5, game.Map);
        int _x = 10, _y = 10;
        c.X = c.Y = _x;
        c.Destination = mine;
        c.Route = new(new List<Facility>() { mine, fac });
        game.Player.Purchase(c);
        for (int i = 0; i < 6; i++)
        {
            ++_x;
            c.Update(3);
            AreEqual(_x, c.X);
            AreEqual(_y, c.Y);
        }

    }
}
