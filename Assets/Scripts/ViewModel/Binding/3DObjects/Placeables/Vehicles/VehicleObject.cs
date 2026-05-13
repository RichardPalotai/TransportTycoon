using UnityEngine;

[CreateAssetMenu(fileName = "Vehicle", menuName = "Placeable vehicles")]
public class VehicleObject : Placeable
{
    public enum Type { CAR, BUS, MINIVAN, TRUCK }
    public enum Cargo { PEOPLE, COMMODITY }
    public int Capacity;
    public int Speed;

    public Type type;
    public Cargo cargoType;
}
