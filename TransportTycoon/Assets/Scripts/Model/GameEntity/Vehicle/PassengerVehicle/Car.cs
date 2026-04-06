public sealed class Car : PassengerVehicle
{
    public Car(int seats, Map map) : base((int)Prices.CAR, 130, seats, map)
    {
        
    }
}
