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

    private void ApplyVisuals(int score)
    {
        Straight.SetActive(false);
        Turn.SetActive(false);
        Tri.SetActive(false);
        Quad.SetActive(false);


        switch (score)
        {
            case 1:
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 4:
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 8:
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            default:
                Straight.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
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