public sealed class Bus : PassengerVehicle
{
    public Bus(int seats, Map map) : base((int)Prices.BUS, 110, seats, map)
    {
        
    }
}
