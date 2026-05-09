using System.Collections.Generic;

public sealed class Bus : PassengerVehicle
{
    public Bus(int seats, Map map) : base((int)Prices.BUS, 110, seats, map)
    {

    }
    public Bus(int seats, int id, int x, int y, double condition, Direction direction, Facility destination, List<Facility> route, Map map)
        : base((int)Prices.BUS, 110, seats, id, x, y, condition, direction, destination, route, map)
    {

    }
}
