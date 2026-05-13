using UnityEngine;

[CreateAssetMenu(fileName = "Facility", menuName = "Placeable facility")]
public class FacilityObject : Placeable
{
    public enum Type { FARM, FACTORY, MINE, LUMBERMILL, TRAFFICLIGHT, BUSSTOP, CITY }
    public enum Product { MILK, EGG, CHEESE, PAPER, STEEL, IRON, WOOD, NONE }

    public Type type;
    public  Product productType;
}
