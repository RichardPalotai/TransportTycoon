using System.Collections.Generic;
using System.Linq;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

public sealed partial class Map
{
    public List<(int x, int y)> GetTilesNeighborRoadsCoords(int x, int y)
    {
        (int dx, int dy)[] dirs =
        {
            (0, -1),
            (0, 1),
            (1, 0),
            (-1, 0)
        };
        List<(int x, int y)> neighborRoads = new();
        foreach (var (dirX, dirY) in dirs)
        {
            if (x + dirX < 0 || x + dirX >= Size || y + dirY < 0 || y + dirY >= Size)
                continue;

            if (_map[x + dirX, y + dirY].Entity is Road)
            {
                neighborRoads.Add((x + dirX, y + dirY));

            }
        }
        return neighborRoads;
    }
    public int GetTilesNeighborRoadsCount(int x, int y)
    {
        return GetTilesNeighborRoadsCoords(x, y).Count;
    }
    public List<(int x, int y)> GetFacilityNeighborRoads(Facility facility)
    {
        int x = facility.X;
        int y = facility.Y;
        int size = GetAreaSize(facility);
        HashSet<(int, int)> result = new();

        for (int i = x; i < x + size; i++)
        {
            for (int j = y; j < y + size; j++)
            {
                foreach (var neighbor in GetTilesNeighborRoadsCoords(i, j))
                {
                    result.Add(neighbor);
                }
            }
        }

        return result.ToList();
    }
    private void AddToCrossRoadIfNeeded(int x, int y, TrafficLight trafficLight)
    {
        var roadCoords = GetTilesNeighborRoadsCoords(x, y);
        if (roadCoords.Count < 3) //Not a crossroad, or no coords
            return;

        var maxNeighborCount = roadCoords.Max(x => GetTilesNeighborRoadsCount(x.x, x.y));
        
        var (middleX, middleY) = roadCoords.Where(x => GetTilesNeighborRoadsCount(x.x, x.y)
                                                == maxNeighborCount)
                                            .OrderBy(x => x.x)
                                            .ThenBy(x => x.y)
                                            .First(); //To make the first element deterministic



        var road = _map[middleX, middleY].Entity as Road;
        if (road is not null && road.IsCrossRoad)
        {
            if (!Crossroads.ContainsKey((middleX, middleY)))
                Crossroads[(middleX, middleY)] = new Crossroad();

            Crossroads[(middleX, middleY)].TrafficLights.Add(trafficLight);
            trafficLight.Crossroad = Crossroads[(middleX, middleY)];
            return;
        }
    }
    public bool IsCrossRoad(int x, int y)
    {
        return (
            (y - 1 >= 0
                ? _map[x, y - 1].Entity is Road ? 1 : 0
                : 0) +
            (y + 1 < Size
                ? _map[x, y + 1].Entity is Road ? 1 : 0
                : 0) +
            (x - 1 >= 0
                ? _map[x - 1, y].Entity is Road ? 1 : 0
                : 0) +
            (x + 1 < Size
                ? _map[x + 1, y].Entity is Road ? 1 : 0
                : 0)
            ) >= 3;
    }

    private void PlaceCityRoads(int x, int y)
    {
        Road r = new Road(true);
        _map[x, y] = new(x, y, r);
        r.IsCrossRoad = IsCrossRoad(x, y);
        Crossroads.Add((x, y), new Crossroad());
    }
}