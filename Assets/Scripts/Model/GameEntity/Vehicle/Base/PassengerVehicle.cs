public abstract class PassengerVehicle : Vehicle
{
    public int Seats { get; init; }
    public int PassengersCount { get; set; } = 0;

    protected PassengerVehicle(int cost, double speed, int seats, Map map) : base(cost, speed, map)
    {
        Seats = seats;
    }
}
