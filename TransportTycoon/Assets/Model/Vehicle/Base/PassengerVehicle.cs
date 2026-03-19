public abstract class PassengerVehicle : Vehicle
{
    public int Seats { get; init; }

    protected PassengerVehicle(int cost, double speed, int seats) : base(cost, speed)
    {
        Seats = seats;
    }
}
