using UnityEngine;

[CreateAssetMenu(fileName = "Facility", menuName = "Placeable objects")]
public class FacilityResource : Placeable
{
    public enum Facility { FARM, FACTORY, MINE, SAWMILL, TRAFFICLIGHT, BUSSTOP, CITY }

    public Facility FacilityObj;
}
