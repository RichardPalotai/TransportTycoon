using System.Collections.Generic;

public sealed class Minivan : TransportVehicle
{
    public Minivan(Resource cargoType, Map map) : base((int)Prices.MINIVAN, 130, 1, cargoType, map)
    {

    }
    public Minivan(Resource cargoType, int id, int x, int y, double condition, Direction direction, Facility destination, List<Facility> route, Map map)
        : base((int)Prices.MINIVAN, 130, 1, cargoType, id, x, y, condition, direction, destination, route, map)
    {

    }
}
