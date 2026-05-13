using System;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance;

    public MapManager mapManager;
    public GridObject gridObject;
    private Placeable selectedBuilding;
    public LayerMask groundLayer;

    public GridObject _road;
    public GridObject _lumberMill;
    public GridObject _mine;
    public GridObject _farm;
    public GridObject _factory;
    public GridObject _city;

    public GridObject _trafficLight;
    public GridObject _busStop;

    public GridObject _car;
    public GridObject _bus;
    public GridObject _miniVan;
    public GridObject _truck;

    private GridObject[] objects = new GridObject[4];
    private int num;
    private int farmCommodity;
    private int factoryCommodity;

    void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        objects[0] = _lumberMill;
        objects[1] = _mine;
        objects[2] = _farm;
        objects[3] = _factory;
        num = 0;



        Debug.LogWarning("Placed starters");
    }

    public void AttemptPlacement(Vector2 mousePos)
    {
        selectedBuilding = gridObject.data;
        if (selectedBuilding == null) return;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
        {
            Debug.LogWarning("Point hit: " + hit.point.x + " " + hit.point.z);
            Debug.LogWarning("Point hit(tile): " + (hit.point.x + 50) + " " + (hit.point.z + 50));
            int originX = Mathf.FloorToInt((hit.point.x / 5.0f) + 50);
            int originZ = Mathf.FloorToInt((hit.point.z / 5.0f) + 50);

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

    /// <summary>
    /// Place down the selected building thus the facility/vehicle type given in gridObject.data (that is the scriptable object which is given as a parameter to the 3D model's prefab as a data connector)
    /// </summary>
    /// <param name="x">x position on map</param>
    /// <param name="y">y position on map</param>
    /// <exception cref="Exception">When the object cannot be recognized</exception>
    private void AttemptSelectedPlacement(int x, int y)
    {
        selectedBuilding = gridObject.data;
        if (selectedBuilding == null) return;

        int originX = x;
        int originZ = y;

        GridObject clickedTileObject = mapManager.GetTile(originX, originZ).Type;

        if (selectedBuilding is VehicleObject)
        {
            VehicleObject vehicle = selectedBuilding as VehicleObject;
            if (clickedTileObject is StraightRoadScript road)
            {
                switch (vehicle.type)
                {
                    case VehicleObject.Type.MINIVAN:
                        if (_miniVan is VehicleScript minivanPrefab)
                        {
                            //Game.instance.Map.PlaceObject(originX, originZ, new Minivan(Milk.Instance, Game.instance.Map));

                            Minivan van = new Minivan(Milk.Instance, Game.instance.Map);
                            Game.instance.Player.Purchase(van);
                            van.PlaceVehicle(originX, originZ);


                            VehicleScript spawnedVan = road.AddVehicle(minivanPrefab) as VehicleScript;

                            spawnedVan.modelSelf = van;

                        }
                        break;

                    case VehicleObject.Type.CAR:
                        if (_car is VehicleScript carPrefab)
                        {
                            //Game.instance.Map.PlaceObject(originX, originZ, new Car(4, Game.instance.Map));

                            Car car = new Car(5, Game.instance.Map);
                            Game.instance.Player.Purchase(car);
                            car.PlaceVehicle(originX, originZ);


                            VehicleScript spawnedCar = road.AddVehicle(carPrefab) as VehicleScript;

                            spawnedCar.modelSelf = car;

                        }
                        break;

                    case VehicleObject.Type.BUS:
                        if (_bus is VehicleScript busPrefab)
                        {
                            //Game.instance.Map.PlaceObject(originX, originZ, new Bus(50, Game.instance.Map));

                            Bus bus = new Bus(50, Game.instance.Map);
                            Game.instance.Player.Purchase(bus);
                            bus.PlaceVehicle(originX, originZ);


                            VehicleScript spawnedBus = road.AddVehicle(busPrefab) as VehicleScript;

                            spawnedBus.modelSelf = bus;

                        }
                        break;

                    case VehicleObject.Type.TRUCK:
                        if (_truck is VehicleScript truckPrefab)
                        {
                            //Game.instance.Map.PlaceObject(originX, originZ, new Truck(Milk.Instance, Game.instance.Map));

                            Truck truck = new Truck(Milk.Instance, Game.instance.Map);
                            Game.instance.Player.Purchase(truck);
                            truck.PlaceVehicle(originX, originZ);


                            VehicleScript spawnedTruck = road.AddVehicle(truckPrefab) as VehicleScript;

                            spawnedTruck.modelSelf = truck;
                        }
                        break;

                    default:
                        throw new Exception("Vehicle type not found");
                }
            }
        }
        else
        {
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
        switch (build)
        {
            case BuilderSelectorHandler.Building.ROAD: gridObject = _road; break;
            case BuilderSelectorHandler.Building.TRAFFICLIGHT: gridObject = _trafficLight; break;
            case BuilderSelectorHandler.Building.CAR: gridObject = _car; break;
            case BuilderSelectorHandler.Building.BUS: gridObject = _bus; break;
            case BuilderSelectorHandler.Building.TRUCK: gridObject = _truck; break;
            case BuilderSelectorHandler.Building.MINIVAN: gridObject = _miniVan; break;
            case BuilderSelectorHandler.Building.BUSSTOP: gridObject = _busStop; break;
            case BuilderSelectorHandler.Building.NONE: return;
            default:
                gridObject = objects[num % 4];
                ++num;
                break;
        }

        selectedBuilding = gridObject.data;
        if (selectedBuilding == null) return;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            int originX = Mathf.FloorToInt((hit.point.x / 5.0f) + 50);
            int originZ = Mathf.FloorToInt((hit.point.z / 5.0f) + 50);

            bool isVehicle = (build == BuilderSelectorHandler.Building.CAR ||
                          build == BuilderSelectorHandler.Building.BUS ||
                          build == BuilderSelectorHandler.Building.TRUCK ||
                          build == BuilderSelectorHandler.Building.MINIVAN);

            GridObject clickedTileObject = mapManager.GetTile(originX, originZ).Type;

            if (isVehicle)
            {
                if (clickedTileObject is StraightRoadScript road)
                {
                    switch (build)
                    {
                        case BuilderSelectorHandler.Building.MINIVAN:
                            if (_miniVan is VehicleScript minivanPrefab)
                            {
                                //Game.instance.Map.PlaceObject(originX, originZ, new Minivan(Milk.Instance, Game.instance.Map));

                                Minivan minivan = new Minivan(Milk.Instance, Game.instance.Map);
                                Game.instance.Player.Purchase(minivan);
                                minivan.PlaceVehicle(originX, originZ);


                                VehicleScript spawnedVan = road.AddVehicle(minivanPrefab) as VehicleScript;

                                spawnedVan.modelSelf = minivan;

                            }
                            break;

                        case BuilderSelectorHandler.Building.CAR:
                            if (_car is VehicleScript carPrefab)
                            {
                                //Game.instance.Map.PlaceObject(originX, originZ, new Car(4, Game.instance.Map));

                                Car car = new Car(5, Game.instance.Map);
                                Game.instance.Player.Purchase(car);
                                car.PlaceVehicle(originX, originZ);


                                VehicleScript spawnedCar = road.AddVehicle(carPrefab) as VehicleScript;

                                spawnedCar.modelSelf = car;

                            }
                            break;

                        case BuilderSelectorHandler.Building.BUS:
                            if (_bus is VehicleScript busPrefab)
                            {
                                //Game.instance.Map.PlaceObject(originX, originZ, new Bus(50, Game.instance.Map));

                                Bus bus = new Bus(50, Game.instance.Map);
                                Game.instance.Player.Purchase(bus);
                                bus.PlaceVehicle(originX, originZ);


                                VehicleScript spawnedBus = road.AddVehicle(busPrefab) as VehicleScript;

                                spawnedBus.modelSelf = bus;

                            }
                            break;

                        case BuilderSelectorHandler.Building.TRUCK:
                            if (_truck is VehicleScript truckPrefab)
                            {
                                //Game.instance.Map.PlaceObject(originX, originZ, new Truck(Milk.Instance, Game.instance.Map));

                                Truck truck = new Truck(Milk.Instance, Game.instance.Map);
                                Game.instance.Player.Purchase(truck);
                                truck.PlaceVehicle(originX, originZ);


                                VehicleScript spawnedTruck = road.AddVehicle(truckPrefab) as VehicleScript;

                                spawnedTruck.modelSelf = truck;
                            }
                            break;
                    }
                }
            }
            else
            {
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
    }

    private bool IsFootprintValid(int startX, int startZ, int footprint)
    {
        for (int x = 0; x < footprint; x++)
        {
            for (int z = 0; z < footprint; z++)
            {
                int checkX = startX + x;
                int checkZ = startZ + z;
                //Debug.LogWarning("Checking footprint x: " + x);
                //Debug.LogWarning("Checking footprint y: " + z);
                //Debug.LogWarning("Hit Tile: " + checkX + " " + checkZ);

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
        if (gridObject == _trafficLight || gridObject == _busStop)
        {
            return Game.instance.Map.GetTile(startX, startZ).IsFree;
        }
        return true;
    }

    private void PlaceBuilding(int startX, int startZ)
    {
        float worldX = (startX - 50 + (selectedBuilding.tileSize / 2.0f)) * 5;
        float worldZ = (startZ - 50 + (selectedBuilding.tileSize / 2.0f)) * 5;
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
        GameEntity facil = null;
        Debug.LogError(selectedBuilding.objectName);
        if (selectedBuilding is FacilityObject facility)
        {

            switch (facility.type)
            {
                case FacilityObject.Type.FARM:
                    if (facility.productType == FacilityObject.Product.MILK)
                    {
                        facil = new Farm<Milk>();
                        //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Farm<Milk>());
                    }
                    else if (facility.productType == FacilityObject.Product.EGG)
                    {
                        facil = new Farm<Egg>();
                        //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Farm<Egg>());
                    }
                    else
                    {
                        facil = new Farm<Cheese>();
                        //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Farm<Cheese>());
                    }

                    break;
                case FacilityObject.Type.FACTORY:
                    if (facility.productType == FacilityObject.Product.STEEL)
                    {
                        facil = new Factory<Steel>();
                        //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Factory<Steel>());
                    }
                    else if (facility.productType == FacilityObject.Product.PAPER)
                    {
                        facil = new Factory<Paper>();
                        //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Factory<Paper>());
                    }
                    break;
                case FacilityObject.Type.MINE:
                    facil = new Mine<Iron>();
                    //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Iron>());
                    break;
                case FacilityObject.Type.LUMBERMILL:
                    facil = new LumberMill<Wood>();
                    //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Wood>());
                    break;
                case FacilityObject.Type.CITY:
                    facil = new City();
                    //Game.instance.Map.PlaceObject(startX + x, startZ + z, new City());
                    break;
                case FacilityObject.Type.BUSSTOP:
                    facil = new BusStop(false);
                    //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Iron>());
                    break;
                case FacilityObject.Type.TRAFFICLIGHT:
                    facil = new TrafficLight(false);
                    //Game.instance.Map.PlaceObject(startX + x, startZ + z, new Mine<Iron>());
                    break;
                default:
                    break;
            }
        }

        ///             +1
        ///         -1  X   +1
        ///             -1

        else if (selectedBuilding is RoadObject road)
        {
            facil = new Road(false);
            //Game.instance.Map.PlaceObject(startX, startZ, new Road(false));

            if (gridObject is StraightRoadScript stroad)
            {
                stroad.map = mapManager;

                for (int i = -1; i < 2; i += 2)
                {

                    if ((startX + i < mapManager.Size) && (startX + i >= 0) && mapManager.GetTile(startX + i, startZ).Type is StraightRoadScript neighbourX)
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

        if (facil == null)
        {
            throw new System.Exception("Facility or road not found");
        }

        Game.instance.Player.Purchase(facil as ITradeable);
        Game.instance.Map.PlaceObject(startX, startZ, facil);
        gridObjScript.modelSelf = Game.instance.Map.GetTile(startX, startZ).Entity;

        gridObjScript.OnObjectPlaced();
    }

    public void DemolishBuilding(Vector2 mousePos, GameEntity selectedBuilding)
    {

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.LogWarning("Point hit: " + hit.point.x + " " + hit.point.z);
            Debug.LogWarning("Point hit(tile): " + (hit.point.x + 50) + " " + (hit.point.z + 50));
            int x = Mathf.FloorToInt((hit.point.x / 5.0f) + 50);
            int y = Mathf.FloorToInt((hit.point.z / 5.0f) + 50);

            GridObject demol;
            if (mapManager.GetTile(x, y).Type != null)
            {
                demol = mapManager.GetTile(x, y).Type;
                Debug.LogWarning("Attempting to sell:" + demol.data.name);
            }
            else
            {
                return;
            }

            for (int i = 0; i < mapManager.Size; ++i)
            {
                for (int j = 0; j < mapManager.Size; ++j)
                {
                    if (mapManager.GetTile(x, y).Type != null && mapManager.GetTile(x, y).Type.ID == demol.ID)
                    {
                        mapManager.GetTile(x, y).Type = null;
                    }
                }
            }
            Game.instance.Player.SellItem(demol.modelSelf as ITradeable);
            Game.instance.Map.RemoveObject(demol.modelSelf);

            Destroy(demol.selfObject);
        }
    }

    public void DemolishBuilding(int x, int y, GameEntity selectedBuilding)
    {
        GridObject demol = mapManager.GetTile(x, y).Type;

        if (demol == null)
            return;

        for (int i = 0; i < mapManager.Size; ++i)
        {
            for (int j = 0; j < mapManager.Size; ++j)
            {
                GridObject tileObject = mapManager.GetTile(i, j).Type;

                if (tileObject != null && tileObject.ID == demol.ID)
                {
                    mapManager.GetTile(i, j).Type = null;
                }
            }
        }

        if (demol.modelSelf is ITradeable tradeable)
        {
            Game.instance.Player.SellItem(tradeable);
        }

        Game.instance.Map.RemoveObject(demol.modelSelf);

        Destroy(demol.selfObject);
    }


    private bool after = false;
    public void Update()
    {
        if (!after)
        {
            gridObject = _city;
            AttemptSelectedPlacement(50, 50);
            gridObject = _lumberMill;
            AttemptSelectedPlacement(25, 50);
            gridObject = _mine;
            AttemptSelectedPlacement(20, 50);
            gridObject = _farm;
            (gridObject.data as FacilityObject).productType = FacilityObject.Product.MILK;
            AttemptSelectedPlacement(60, 20);
            gridObject = _farm;
            (gridObject.data as FacilityObject).productType = FacilityObject.Product.CHEESE;
            AttemptSelectedPlacement(70, 20);
            gridObject = _farm;
            (gridObject.data as FacilityObject).productType = FacilityObject.Product.EGG;
            AttemptSelectedPlacement(20, 70);
            gridObject = _factory;
            (gridObject.data as FacilityObject).productType = FacilityObject.Product.PAPER;
            AttemptSelectedPlacement(50, 20);
            gridObject = _factory;
            (gridObject.data as FacilityObject).productType = FacilityObject.Product.STEEL;
            AttemptSelectedPlacement(50, 40);
            after = true;
        }
    }

    public void RebuildVisualsFromLoadedModel()
    {
        if (Game.instance == null || Game.instance.Map == null || Game.instance.Player == null)
        {
            Debug.LogError("Cannot rebuild visuals: Game, Map or Player is null.");
            return;
        }

        foreach (Facility facility in Game.instance.Player.Facilities)
        {
            SpawnVisualForEntity(facility);
        }

        foreach (Vehicle vehicle in Game.instance.Player.Vehicles)
        {
            SpawnVisualForEntity(vehicle);
        }
    }

    private void SpawnVisualForEntity(GameEntity entity)
    {
        if (entity is Vehicle vehicle)
        {
            SpawnVehicleVisual(vehicle);
            return;
        }

        SpawnFacilityOrRoadVisual(entity);
    }

    private void SpawnFacilityOrRoadVisual(GameEntity entity)
    {
        GridObject prefab = GetPrefabForEntity(entity);

        if (prefab == null)
        {
            Debug.LogWarning($"No prefab found for loaded entity: {entity.GetType().Name}");
            return;
        }

        int x = entity.X;
        int z = entity.Y;

        int footprint = prefab.data != null ? prefab.data.tileSize : 1;

        float worldX = (x - 50 + (footprint / 2.0f)) * 5;
        float worldZ = (z - 50 + (footprint / 2.0f)) * 5;

        Vector3 spawnPosition = new Vector3(worldX, 0, worldZ);

        GameObject spawnedObj = Instantiate(
            prefab.gameObject,
            spawnPosition,
            prefab.transform.rotation
        );

        GridObject gridObjScript = spawnedObj.GetComponent<GridObject>();

        gridObjScript.data = prefab.data;
        gridObjScript.position = new Vector2Int(x, z);
        gridObjScript.selfObject = spawnedObj;
        gridObjScript.modelSelf = entity;

        for (int dx = 0; dx < footprint; dx++)
        {
            for (int dz = 0; dz < footprint; dz++)
            {
                mapManager.SetTile(x + dx, z + dz, gridObjScript);
            }
        }

        gridObjScript.OnObjectPlaced();

        if (gridObjScript is StraightRoadScript road)
        {
            road.map = mapManager;
            road.UpdateRoadShape();
        }
    }

    private void SpawnVehicleVisual(Vehicle vehicle)
    {
        GridObject prefab = GetPrefabForEntity(vehicle);

        if (prefab == null)
        {
            Debug.LogWarning($"No vehicle prefab found for loaded vehicle: {vehicle.GetType().Name}");
            return;
        }

        int x = vehicle.X;
        int z = vehicle.Y;

        GridObject tileObject = mapManager.GetTile(x, z).Type;

        if (tileObject is not StraightRoadScript road)
        {
            Debug.LogWarning($"Loaded vehicle {vehicle.GetType().Name} is not on a visual road at {x},{z}.");
            return;
        }

        if (prefab is not VehicleScript vehiclePrefab)
        {
            Debug.LogWarning($"Prefab for {vehicle.GetType().Name} is not a VehicleScript.");
            return;
        }

        VehicleScript spawnedVehicle = road.AddVehicle(vehiclePrefab) as VehicleScript;

        if (spawnedVehicle == null)
        {
            Debug.LogWarning($"Could not spawn vehicle visual for {vehicle.GetType().Name}.");
            return;
        }

        spawnedVehicle.modelSelf = vehicle;
    }

    private GridObject GetPrefabForEntity(GameEntity entity)
    {
        switch (entity)
        {
            case City:
                return _city;

            case LumberMill<Wood>:
                return _lumberMill;

            case Mine<Iron>:
                return _mine;

            case Farm<Milk>:
            case Farm<Egg>:
            case Farm<Cheese>:
                return _farm;

            case Factory<Steel>:
            case Factory<Paper>:
                return _factory;

            case Road:
                return _road;

            case TrafficLight:
                return _trafficLight;

            case BusStop:
                return _busStop;

            case Car:
                return _car;

            case Bus:
                return _bus;

            case Minivan:
                return _miniVan;

            case Truck:
                return _truck;

            default:
                return null;
        }
    }
}
