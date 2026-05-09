using System.Collections.Generic;

public sealed class Truck : TransportVehicle
{
    public Truck(Resource cargoType, int id, int x, int y, double condition, Direction direction, Facility destination, List<Facility> route, Map map) 
        : base((int)Prices.TRUCK, 90, 24, cargoType, id, x, y, condition, direction, destination, route, map)
    {
        
    }
}
