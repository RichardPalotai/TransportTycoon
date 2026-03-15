public abstract class TransportVehicle : Vehicle
{
    public int CargoCapacity { get; init; }
    public TransportVehicle(int cost, double speed, int cargoCapacity) : base(cost, speed)
    {
        CargoCapacity = cargoCapacity;
    }
}
