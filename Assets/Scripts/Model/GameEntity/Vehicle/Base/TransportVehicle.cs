using System.Collections.Generic;

public abstract class TransportVehicle : Vehicle
{
    public int CargoCapacity { get; init; }
    public int CurrentCargo { get; set; }
    public Resource CargoType { get; init; }
    protected TransportVehicle(int cost, double speed, int cargoCapacity, Resource cargoType, Map map) : base(cost, speed, map)
    {
        CargoCapacity = CurrentCargo = cargoCapacity;
        CargoType = cargoType;
    }
    protected TransportVehicle(int cost, double speed, int cargoCapacity, Resource cargoType, int id, int x, int y, double condition, Direction direction, Facility destination, List<Facility> route, Map map)
        : base(id, x, y, cost, speed, condition, direction, destination, route, map)
    {
        CargoCapacity = CurrentCargo = cargoCapacity;
        CargoType = cargoType;
    }
}
