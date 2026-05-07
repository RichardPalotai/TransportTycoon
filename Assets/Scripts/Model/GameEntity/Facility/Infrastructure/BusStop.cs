public sealed class BusStop : Facility, IPassengerInteractable
{
    public BusStop(bool isGenerated) : base((int)Prices.BUSSTOP, isGenerated)
    {
    }

    public int PassInteract()
    {
        System.Random rnd = new();
        return rnd.Next(-10, 10);
    }
    
    public int PassInteractFixedSeed(int seed)
    {
        System.Random rnd = new(seed);
        return rnd.Next(-10, 10);
    }
}
