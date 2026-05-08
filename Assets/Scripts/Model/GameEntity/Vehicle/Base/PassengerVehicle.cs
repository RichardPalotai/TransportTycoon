using System.Collections.Generic;

public abstract class PassengerVehicle : Vehicle
{
    public int Seats { get; init; }
    public int PassengersCount { get; set; } = 0;

    protected PassengerVehicle(int cost, double speed, int seats, Map map) : base(cost, speed, map)
    {
        Seats = seats;
    }
    protected PassengerVehicle(int cost, double speed, int seats, int id, int x, int y, double condition, Direction direction, Facility destination, List<Facility> route, Map map)
        : base(id, x, y, cost, speed, condition, direction, destination, route, map)
    {
        Seats = seats;
    }
}
