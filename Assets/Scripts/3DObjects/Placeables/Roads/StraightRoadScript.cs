using Moq;
using UnityEngine;

public class StraightRoadScript : GridObject
{
    public VehicleScript[] occupyingVehicles = new VehicleScript[2];

    public GameObject Straight;
    public GameObject Turn;
    public GameObject Tri;
    public GameObject Quad;

    public MapManager map;
    public override void OnObjectPlaced()
    {
        Debug.LogWarning("Placed Road");

        UpdateRoadShape();

    }

    public void UpdateRoadShape()
    {
        int score= 0;

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

        if (obj is StraightRoadScript) return true;

        return false;
    }

    public void ApplyVisuals(int score)
    {
        // 1. Reset everything to hidden first so meshes don't overlap!
        Straight.SetActive(false);
        Turn.SetActive(false);
        Tri.SetActive(false);
        Quad.SetActive(false);

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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}