public abstract class TransportVehicle : Vehicle
{
    public int CargoCapacity { get; init; }
    public Resource CargoType { get; init; }
    protected TransportVehicle(int cost, double speed, int cargoCapacity, Resource cargoType, Map map) : base(cost, speed, map)
    {
        CargoCapacity = cargoCapacity;
        CargoType = cargoType;
    }
}
