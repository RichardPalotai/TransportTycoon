using System.Collections.Generic;

public sealed class Car : PassengerVehicle
{
    public Car(int seats, Map map) : base((int)Prices.CAR, 130, seats, map)
    {
        
    }
    public Car(int seats, int id, int x, int y, double condition, Direction direction, Facility destination, List<Facility> route, Map map)
        : base((int)Prices.CAR, 130, seats, id, x, y, condition, direction, destination, route, map)
    {

    }
}
