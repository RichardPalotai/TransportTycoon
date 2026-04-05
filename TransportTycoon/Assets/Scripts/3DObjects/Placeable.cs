using UnityEngine;

[CreateAssetMenu(fileName = "Placeables", menuName = "Placeable objects")]
public class Placeable : ScriptableObject
{
    public enum Facility { FARM, FACTORY, MINE, SAWMILL }
    public int ID;
    public Facility FacilityObj;
    public string objectName;
    public int tileSize;
    public GameObject prefab;
    public int placementCost;

}
