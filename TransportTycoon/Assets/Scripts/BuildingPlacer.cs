using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    public MapManager mapManager;
    public Placeable selectedBuilding;

    private void Start()
    {
        //PlaceBuilding(2, 2);
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            AttemptPlacement();
        }
    }

    private void AttemptPlacement()
    {
        if (selectedBuilding == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.LogWarning("Point hit: " + hit.point.x + " " + hit.point.z);
            Debug.LogWarning("Point hit(tile): " + (hit.point.x + 15) + " " + (hit.point.z + 15));
            int originX = Mathf.FloorToInt((hit.point.x / 5.0f) + 15);
            int originZ = Mathf.FloorToInt((hit.point.z / 5.0f) + 15);

            if (IsFootprintValid(originX, originZ, selectedBuilding.tileSize))
            {
                PlaceBuilding(originX, originZ);
            }
            else
            {
                Debug.LogWarning("Cannot build here! Area is blocked or out of bounds." + originX + " " + originZ);
            }
        }
    }

    private bool IsFootprintValid(int startX, int startZ, int footprint)
    {
        for (int x = 0; x < footprint; x++)
        {
            for (int z = 0; z < footprint; z++)
            {
                int checkX = startX + x;
                int checkZ = startZ + z;
                Debug.LogWarning("Hit Tile: " + checkX + " " + checkZ);

                if (checkX < 0 || checkX >= mapManager.Size ||
                    checkZ < 0 || checkZ >= mapManager.Size)
                {
                    return false;
                }

                if (mapManager.GetTile(checkX, checkZ).IsFree() == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void PlaceBuilding(int startX, int startZ)
    {
        float worldX = (startX - 15 + (selectedBuilding.tileSize / 2)) * 5;
        float worldZ = (startZ - 15 + (selectedBuilding.tileSize / 2)) * 5;
        Vector3 spawnPosition = new Vector3(worldX, 0, worldZ);

        GameObject newBuildingObj = Instantiate(selectedBuilding.prefab, spawnPosition, Quaternion.identity);

        GridObject gridObjScript = newBuildingObj.GetComponent<GridObject>();
        gridObjScript.data = selectedBuilding;
        gridObjScript.position = new Vector2Int(startX, startZ);
        
        int x = 0;
        int z = 0;
        for (x = 0; x < selectedBuilding.tileSize; x++)
        {
            for (z = 0; z < selectedBuilding.tileSize; z++)
            {
                mapManager.GetTile(startX + x, startZ + z).Type = gridObjScript;
            }
        }
        switch (selectedBuilding.FacilityObj)
        {
            case Placeable.Facility.FARM:
                Game.instance.Map.PlaceObject(startX + x, startZ + z, new Farm<Milk>());
                break;
            case Placeable.Facility.FACTORY:
                Game.instance.Map.PlaceObject(startX + x, startZ + z, new Factory<Steel>());
                break;
            case Placeable.Facility.MINE:
                Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Iron>());
                break;
            case Placeable.Facility.SAWMILL:
                Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Wood>());
                break;
            default:
                break;
        }

        gridObjScript.OnObjectPlaced();
    }
}
