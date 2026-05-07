using UnityEngine;

[CreateAssetMenu(fileName = "Vehicle", menuName = "Placeable vehicles")]
public class VehicleResource : Placeable
{
    public enum Vehicle { CAR, BUS, MINIVAN, TRUCK }
    public enum Transport { PEOPLE, RESOURCE}
    public int Capacity;
    public int Speed;

    public Vehicle VehicleObj;
    public Transport Type;
}
