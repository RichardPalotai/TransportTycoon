public sealed class Minivan : TransportVehicle
{
    public Minivan(Resource cargoType) : base((int)Prices.MINIVAN, 130, 1, cargoType)
    {
        
    }
}
