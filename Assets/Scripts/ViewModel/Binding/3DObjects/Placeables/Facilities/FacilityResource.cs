using UnityEngine;

[CreateAssetMenu(fileName = "Facility", menuName = "Placeable objects")]
public class FacilityResource : Placeable
{
    public enum Facility { FARM, FACTORY, MINE, SAWMILL, TRAFFICLIGHT, BUSSTOP }

    public Facility FacilityObj;
}
