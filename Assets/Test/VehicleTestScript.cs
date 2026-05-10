using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using static NUnit.Framework.Assert;
public class VehicleTestScript
{
    [Test]
    public void VehicleMovementOnAStraightRoadTest()
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
    [Test]
    public void VehicleMovementWithARightTurnTest()
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
        game.Map.PlaceObject(13, 11, new Road(false));
        game.Map.PlaceObject(13, 12, new Road(false));
        game.Map.PlaceObject(13, 13, new Road(false));
        game.Map.PlaceObject(13, 14, new Road(false));

        var fac = new Factory<Paper>();
        var mine = new Mine<Iron>();
        game.Player.Purchase(fac);
        game.Player.Purchase(mine);
        game.Map.PlaceObject(10, 11, fac);
        game.Map.PlaceObject(14, 14, mine);

        Car c = new Car(5, game.Map);
        int _x = 10, _y = 10;
        c.X = c.Y = _x;
        c.Destination = mine;
        c.Route = new(new List<Facility>() { mine, fac });
        game.Player.Purchase(c);
        for (int i = 0; i < 7; i++)
        {
            c.Update(3);
        }
        _x = 13;
        _y = 14;
        AreEqual(_x, c.X);
        AreEqual(_y, c.Y);

    }
    [Test]
    public void VehicleMovementWithARightTurnAndMultipleIterationsTest()
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
        game.Map.PlaceObject(13, 11, new Road(false));
        game.Map.PlaceObject(13, 12, new Road(false));
        game.Map.PlaceObject(13, 13, new Road(false));
        game.Map.PlaceObject(13, 14, new Road(false));

        var fac = new Factory<Paper>();
        var mine = new Mine<Iron>();
        game.Player.Purchase(fac);
        game.Player.Purchase(mine);
        game.Map.PlaceObject(10, 11, fac);
        game.Map.PlaceObject(14, 14, mine);

        Car c = new Car(5, game.Map);
        int _x = 10, _y = 10;
        c.X = c.Y = _x;
        c.Destination = mine;
        c.Route = new(new List<Facility>() { mine, fac });
        game.Player.Purchase(c);
        for (int i = 0; i < 14; i++)
        {
            c.Update(3);
            if (i < 3)
            {
                AreEqual(Direction.EAST, c.Direction);
            }
            else if (i < 7)
            {
                AreEqual(Direction.SOUTH, c.Direction);
            }
            else if (i < 11)
            {
                AreEqual(Direction.NORTH, c.Direction);
            }
            else
            {
                AreEqual(Direction.WEST, c.Direction);

            }
        }
        _x = 10;
        _y = 10;
        AreEqual(_x, c.X);
        AreEqual(_y, c.Y);

    }

    [Test]
    public void VehicleMovementWithARightTurnAndMultipleStopsTest()
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
        game.Map.PlaceObject(13, 11, new Road(false));
        game.Map.PlaceObject(13, 12, new Road(false));
        game.Map.PlaceObject(13, 13, new Road(false));
        game.Map.PlaceObject(13, 14, new Road(false));
        game.Map.PlaceObject(13, 15, new Road(false));
        game.Map.PlaceObject(13, 16, new Road(false));
        game.Map.PlaceObject(13, 17, new Road(false));
        game.Map.PlaceObject(13, 18, new Road(false));

        var fac = new Factory<Paper>();
        var mine = new Mine<Iron>();
        var busStop = new BusStop(false);
        game.Player.Purchase(fac);
        game.Player.Purchase(mine);
        game.Player.Purchase(busStop);
        game.Map.PlaceObject(10, 11, fac);
        game.Map.PlaceObject(14, 14, mine);
        game.Map.PlaceObject(14, 18, busStop);

        Car c = new Car(5, game.Map);
        int _x = 10, _y = 10;
        c.X = c.Y = _x;
        c.Destination = busStop;
        c.Route = new(new List<Facility>() { mine, busStop, fac });
        game.Player.Purchase(c);
        for (int i = 0; i < 11; i++)
        {
            c.Update(3);
        }
        _x = 13;
        _y = 18;
        AreEqual(_x, c.X);
        AreEqual(_y, c.Y);

    }

    [Test]
    public void BusPersistenceConstructorTest()
    {
        Bus bus = new(40, 12345, 1, 1, 0.5, Direction.NORTH, new Factory<Steel>(), new List<Facility>(), new Map());
        NotNull(bus);
        AreEqual(40, bus.Seats);
        AreEqual(12345, bus.ID);
        AreEqual(1, bus.X);
        AreEqual(1, bus.Y);
        AreEqual(0.5, bus.Condition);
        AreEqual(Direction.NORTH, bus.Direction);
        IsInstanceOf<Factory<Steel>>(bus.Destination);
        NotNull(bus.Route);
        Bus bus2 = new(40, new Map());
        NotNull(bus2);
        AreEqual(40, bus2.Seats);
    }
    [Test]
    public void CarPersistenceConstructorTest()
    {
        Car car = new(5, 12345, 1, 1, 0.5, Direction.NORTH, new Factory<Steel>(), new List<Facility>(), new Map());
        NotNull(car);
        AreEqual(5, car.Seats);
        AreEqual(12345, car.ID);
        AreEqual(1, car.X);
        AreEqual(1, car.Y);
        AreEqual(0.5, car.Condition);
        AreEqual(Direction.NORTH, car.Direction);
        IsInstanceOf<Factory<Steel>>(car.Destination);
        NotNull(car.Route);
        Car car2 = new(5, new());
        NotNull(car2);
        AreEqual(5, car2.Seats);
    }
    [Test]
    public void MinivanPersistenceConstructorTest()
    {
        Minivan van = new(Milk.Instance, 12345, 1, 1, 0.5, Direction.NORTH, new Factory<Steel>(), new List<Facility>(), new Map());
        NotNull(van);
        IsInstanceOf<Milk>(van.CargoType);
        AreEqual(12345, van.ID);
        AreEqual(1, van.X);
        AreEqual(1, van.Y);
        AreEqual(0.5, van.Condition);
        AreEqual(Direction.NORTH, van.Direction);
        IsInstanceOf<Factory<Steel>>(van.Destination);
        NotNull(van.Route);
        Minivan van2 = new(Milk.Instance, new());
        NotNull(van2);
        IsInstanceOf<Milk>(van2.CargoType);
    }
    [Test]
    public void TruckPersistenceConstructorTest()
    {
        Truck truck = new(Milk.Instance, 12345, 1, 1, 0.5, Direction.NORTH, new Factory<Steel>(), new List<Facility>(), new Map());
        NotNull(truck);
        IsInstanceOf<Milk>(truck.CargoType);
        AreEqual(12345, truck.ID);
        AreEqual(1, truck.X);
        AreEqual(1, truck.Y);
        AreEqual(0.5, truck.Condition);
        AreEqual(Direction.NORTH, truck.Direction);
        IsInstanceOf<Factory<Steel>>(truck.Destination);
        NotNull(truck.Route);
        Truck truck2 = new(Milk.Instance, new());
        NotNull(truck2);
        IsInstanceOf<Milk>(truck2.CargoType);
    }
}
