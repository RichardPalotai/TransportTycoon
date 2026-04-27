using UnityEngine;
using ViewModel.GameScreen.UIHandlers;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance;

    public MapManager mapManager;
    public GridObject gridObject;
    private Placeable selectedBuilding;
    public LayerMask groundLayer;

    public GridObject Road;
    public GridObject SawMill;
    public GridObject Mine;
    public GridObject Farm;
    public GridObject Factory;

    private GridObject[] objects = new GridObject[4];
    private int num;

    void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        objects[0] = SawMill;
        objects[1] = Mine;
        objects[2] = Farm;
        objects[3] = Factory;
        num = 0;
        

    }

    public void AttemptPlacement(Vector2 mousePos)
    {
        selectedBuilding = gridObject.data;
        if (selectedBuilding == null) return;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
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

    public void AttemptPlacement(Vector2 mousePos, BuilderSelectorHandler.Building build)
    {
        if (build == BuilderSelectorHandler.Building.ROAD) {
            gridObject = Road;
        }
        else
        {
            gridObject = objects[num%4];
            ++num;

        }


            selectedBuilding = gridObject.data;
        if (selectedBuilding == null) return;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

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
                Debug.LogWarning("Checking footprint x: " + x);
                Debug.LogWarning("Checking footprint y: " + z);
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
        float worldX = (startX - 15 + (selectedBuilding.tileSize / 2.0f)) * 5;
        float worldZ = (startZ - 15 + (selectedBuilding.tileSize / 2.0f)) * 5;
        Vector3 spawnPosition = new Vector3(worldX, 0, worldZ);

        GameObject newBuildingObj = Instantiate(selectedBuilding.prefab, spawnPosition, selectedBuilding.prefab.transform.rotation);


        GridObject gridObjScript = newBuildingObj.GetComponent<GridObject>();
        gridObjScript.data = selectedBuilding;
        gridObjScript.position = new Vector2Int(startX, startZ);
        gridObjScript.selfObject = newBuildingObj;
        
        int x = 0;
        int z = 0;
        for (x = 0; x < selectedBuilding.tileSize; x++)
        {
            for (z = 0; z < selectedBuilding.tileSize; z++)
            {
                mapManager.SetTile(startX + x, startZ + z, gridObjScript);
            }
        }
        
        if (selectedBuilding is FacilityResource facility)
        {
            switch (facility.FacilityObj)
            {
                case FacilityResource.Facility.FARM:
                    Game.instance.Map.PlaceObject(startX + x, startZ + z, new Farm<Milk>());
                    break;
                case FacilityResource.Facility.FACTORY:
                    Game.instance.Map.PlaceObject(startX + x, startZ + z, new Factory<Steel>());
                    break;
                case FacilityResource.Facility.MINE:
                    Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Iron>());
                    break;
                case FacilityResource.Facility.SAWMILL:
                    Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Wood>());
                    break;
                default:
                    break;
            }
        }

        ///             +1
        ///         -1  X   +1
        ///             -1

        else if(selectedBuilding is RoadResource road)
        {
            if (gridObject is StraightRoadScript stroad)
            {
                stroad.map = mapManager;

                for (int i = -1; i < 2; i += 2)
                {

                    if ( (startX + i < mapManager.Size ) && (startX + i >= 0) && mapManager.GetTile(startX + i, startZ).Type is StraightRoadScript neighbourX)
                    {
                        neighbourX.UpdateRoadShape();
                    }

                    if ((startZ + i < mapManager.Size) && (startZ + i >= 0) && mapManager.GetTile(startX, startZ + i).Type is StraightRoadScript neighbourZ)
                    {
                        neighbourZ.UpdateRoadShape();
                    }
                }
            }
        }


        gridObjScript.OnObjectPlaced();
    }

    public void DemolishBuilding(Vector2 mousePos)
    {

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
        {
            Debug.LogWarning("Point hit: " + hit.point.x + " " + hit.point.z);
            Debug.LogWarning("Point hit(tile): " + (hit.point.x + 15) + " " + (hit.point.z + 15));
            int x = Mathf.FloorToInt((hit.point.x / 5.0f) + 15);
            int y = Mathf.FloorToInt((hit.point.z / 5.0f) + 15);

            GridObject demol;
            if (mapManager.GetTile(x, y).Type != null)
            {
                demol = mapManager.GetTile(x, y).Type;
            }
            else
            {
                return;
            }

            for (int i = 0; i < mapManager.Size; ++i)
            {
                for (int j = 0; j < mapManager.Size; ++j)
                {
                    if (mapManager.GetTile(x,y).Type != null &&mapManager.GetTile(x, y).Type.ID == demol.ID)
                    {
                        mapManager.GetTile(x, y).Type = null;
                    }
                }
            }
            Destroy(demol.selfObject);
        }

        

        //Delete from game model and add to balance ! ! ! !


        
    }
}
