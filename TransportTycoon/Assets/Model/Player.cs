using System.Collections.Generic;

public sealed class Player
{
    public int Money { get; set; }
    public List<Vehicle> Vehicles { get; private set; }
    public LinkedList<Facility> Facilities { get; set; }
    public void Purchase(ITradeable item)
    {
        throw new System.NotImplementedException();
    }
    public void SellItem(ITradeable item)
    {
        throw new System.NotImplementedException();
    }


}