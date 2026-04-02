public sealed class Truck : TransportVehicle
{
    public Truck(Resource cargoType) : base((int)Prices.TRUCK, 90, 24, cargoType)
    {
        
    }
}
