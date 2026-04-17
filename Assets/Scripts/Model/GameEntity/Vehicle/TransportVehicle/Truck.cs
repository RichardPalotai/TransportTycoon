public sealed class Truck : TransportVehicle
{
    public Truck(Resource cargoType, Map map) : base((int)Prices.TRUCK, 90, 24, cargoType, map)
    {
        
    }
}
