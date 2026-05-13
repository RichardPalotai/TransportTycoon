using UnityEngine;
#nullable enable


public class StraightRoadScript : GridObject
{
    public VehicleScript[] occupyingVehicles = new VehicleScript[2];

    public GameObject? Straight;
    public GameObject? Turn;
    public GameObject? Tri;
    public GameObject? Quad;

    public Transform? SlotRight;
    public Transform? SlotLeft;

    private GridObject? RightOccupied = null;
    private GridObject? LeftOccupied = null;


    private int score;

    public int Score
    {
        get { return score; }
    }

    public MapManager? map;
    public override void OnObjectPlaced()
    {
        Debug.LogWarning("Placed Road");

        UpdateRoadShape();

    }

    public void UpdateRoadShape()
    {
        score = 0;

        if (IsRoadAt(position.x, position.y + 1)) score += 1;
        
        if (IsRoadAt(position.x + 1, position.y)) score += 2;
        
        if (IsRoadAt(position.x, position.y - 1)) score += 4;
        
        if (IsRoadAt(position.x - 1, position.y)) score += 8;

        ApplyVisuals(score);
    }

    private bool IsRoadAt(int x, int z)
    {
        if (x < 0 || x >= map.Size|| z < 0 || z >= map.Size)
            return false;

        GridObject? obj = map.GetTile(x, z).Type;

        if (obj != null)
        {
            if (obj is StraightRoadScript) return true;
        }

        

        return false;
    }

    public void ApplyVisuals(int score)
    {
        Straight.SetActive(false);
        Turn.SetActive(false);
        Tri.SetActive(false);
        Quad.SetActive(false);

        Debug.LogWarning("Road applied score:" +  score);

        switch (score)
        {

            case 0: // No neighbors
            case 1: // North neighbor only
            case 4: // South neighbor only
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case 2: // East neighbor only
            case 8: // West neighbor only
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;

            case 5: // North (1) + South (4)
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case 10: // East (2) + West (8)
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;

            case 3: // North (1) + East (2)
                Turn.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case 6: // East (2) + South (4)
                Turn.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;

            case 12: // South (4) + West (8)
                Turn.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;

            case 9: // North (1) + West (8)
                Turn.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 270, 0); // or -90
                break;


            case 11: // North (1) + East (2) + West (8)
                Tri.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case 7: // North (1) + East (2) + South (4)
                Tri.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;

            case 14: // East (2) + South (4) + West (8)
                Tri.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;

            case 13: // North (1) + South (4) + West (8)
                Tri.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;


            case 15: // All four directions
                Quad.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            default:
                // This is a fallback just in case math goes weird
                Debug.LogWarning($"Road encountered unknown bitmask score: {score}");
                break;
        }
    }

    public void RemoveCar(bool left)
    {
        if (left)
        {
            LeftOccupied = null;
        }
        else
        {
            RightOccupied = null;
        }
    }

    public VehicleScript? AddVehicle(VehicleScript vehiclePrefab)
    {
        Debug.LogWarning("Trying to place car!");

        if (SlotRight == null || SlotLeft == null)
        {
            Debug.LogError("CRITICAL: SlotRight or SlotLeft is missing on this Road prefab! Assign them in the Inspector.");
            return null;
        }

        GameObject spawnedVehicleObj = null;
        VehicleScript spawnedScript = null;

        if (RightOccupied == null)
        {
            spawnedVehicleObj = Instantiate(vehiclePrefab.data.prefab, SlotRight.position, SlotRight.rotation);
            Debug.LogWarning("Placed car on Right side!");
            spawnedScript = spawnedVehicleObj.GetComponent<VehicleScript>();

            RightOccupied = spawnedScript;
            Debug.LogError(spawnedVehicleObj);
            return spawnedScript;
        }
        if (LeftOccupied == null)
        {
            spawnedVehicleObj = Instantiate(vehiclePrefab.data.prefab, SlotLeft.position, SlotLeft.rotation);
            Debug.LogWarning("Placed car on Left side!");
            spawnedScript = spawnedVehicleObj.GetComponent<VehicleScript>();

            LeftOccupied = spawnedScript; 
            return spawnedScript;
        }


        Debug.LogWarning("Cannot place car: This road tile is full!");
        return null;
    }

    // You also need to fix your second AddCar method so it doesn't leak memory when cars move
    public void AddCar(VehicleScript existingCarClone, bool left)
    {
        if (left && LeftOccupied == null)
        {
            LeftOccupied = existingCarClone;
            // DO NOT Instantiate here! Just move the existing 3D model!
            existingCarClone.transform.position = SlotLeft.position;
            existingCarClone.transform.rotation = SlotLeft.rotation;
        }
        else if (!left && RightOccupied == null)
        {
            RightOccupied = existingCarClone;
            // DO NOT Instantiate here!
            existingCarClone.transform.position = SlotRight.position;
            existingCarClone.transform.rotation = SlotRight.rotation;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (RightOccupied != null && RightOccupied.modelSelf is Vehicle rightVehicle)
        {
            if (rightVehicle.X != position.x || rightVehicle.Y != position.y)
            {
                bool goesLeft = CalculateLaneDirection(rightVehicle, RightOccupied);

                if (map.GetTile(rightVehicle.X, rightVehicle.Y).Type is StraightRoadScript nextRoad)
                {
                    nextRoad.AddCar(RightOccupied as VehicleScript, goesLeft);
                }

                RightOccupied = null;
            }
            else
            {
                RightOccupied.position.x = rightVehicle.X;
                RightOccupied.position.y = rightVehicle.Y;
            }
        }

        if (LeftOccupied != null && LeftOccupied.modelSelf is Vehicle leftVehicle)
        {
            if (leftVehicle.X != position.x || leftVehicle.Y != position.y)
            {
                bool goesLeft = CalculateLaneDirection(leftVehicle, LeftOccupied);

                if (map.GetTile(leftVehicle.X, leftVehicle.Y).Type is StraightRoadScript nextRoad)
                {
                    nextRoad.AddCar(LeftOccupied as VehicleScript, goesLeft);
                }

                LeftOccupied = null;
            }
            else
            {
                LeftOccupied.position.x = leftVehicle.X;
                LeftOccupied.position.y = leftVehicle.Y;
            }
        }
    }

    private bool CalculateLaneDirection(Vehicle backendVehicle, GridObject carScript)
    {
        bool left = true;
        if (backendVehicle.X > carScript.position.x) left = false;
        if (backendVehicle.Y > carScript.position.y) left = false;
        if (backendVehicle.X < carScript.position.x) left = true;
        if (backendVehicle.Y < carScript.position.y) left = true;

        return left;
    }
}