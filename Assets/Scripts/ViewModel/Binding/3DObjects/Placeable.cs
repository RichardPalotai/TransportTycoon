using UnityEngine;

[CreateAssetMenu(fileName = "Placeables", menuName = "Placeable objects")]
public class Placeable : ScriptableObject
{
    public string objectName;
    public int tileSize;
    public GameObject prefab;
    public int placementCost;
}
