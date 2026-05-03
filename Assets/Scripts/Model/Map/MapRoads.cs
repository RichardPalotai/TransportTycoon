using System.Collections.Generic;
using System.Diagnostics;
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
    public List<(int x, int y)> GetTilesAllNeighborRoadTilesCoords(int x, int y)
    {
        (int dx, int dy)[] dirs =
        {
            (-1, -1), (0, -1), (1, -1),
            (-1,  0),          (1,  0),
            (-1,  1), (0,  1), (1,  1)
        };

        List<(int x, int y)> neighborRoads = new();

        foreach (var (dx, dy) in dirs)
        {
            int nx = x + dx;
            int ny = y + dy;

            if (nx < 0 || nx >= Size || ny < 0 || ny >= Size)
                continue;

            if (_map[nx, ny].Entity is Road)
                neighborRoads.Add((nx, ny));
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
        if (!IsCrossRoad(x, y))
            return;

        var middle = GetTheMiddleOfTheCrossroad(x, y);

        trafficLight.FacingDirection = SetLightDirection(x, y, middle);

        Crossroads[middle].TrafficLights.Add(trafficLight);


    }
    /// <summary>
    /// If x y coords on the map are pointing to a road -> we look around in a + shape and check whether we have at least one road which has at least 3 road neighbors (include (x,y) itself, because the clicked tile might be the center of the crossroad).
    /// If x y is not a road, we have to check all 8 neighbor tiles road neighbors (). If there is at least one which has at least 3 road neighbors -> crossroad.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsCrossRoad(int x, int y)
    {
        if (_map[x, y].Entity is Road)
        {
            var roads = GetTilesNeighborRoadsCoords(x, y).Append((x, y));
            return roads.Any(road => GetTilesNeighborRoadsCoords(road.x, road.y).Count >= 3);
        }
        else
        {
            var roads = GetTilesAllNeighborRoadTilesCoords(x, y);
            return roads.Any(road => GetTilesAllNeighborRoadTilesCoords(road.x, road.y).Count >= 3);
        }
    }
    public (int, int) GetTheMiddleOfTheCrossroad(int x, int y)
    {
        if (_map[x, y].Entity is Road)
            return GetTilesNeighborRoadsCoords(x, y)
                        .Append((x, y))
                        .Select(coord => new
                        {
                            Coord = coord,
                            Count = GetTilesNeighborRoadsCoords(coord.x, coord.y).Count
                        })
                        .OrderByDescending(x => x.Count)
                        .First()
                        .Coord;

        return GetTilesAllNeighborRoadTilesCoords(x, y)
                        .Select(coord => new
                        {
                            Coord = coord,
                            Count = GetTilesNeighborRoadsCoords(coord.x, coord.y).Count
                        })
                        .OrderByDescending(x => x.Count)
                        .First()
                        .Coord;
    }

    private void PlaceCityRoads(int x, int y)
    {
        Road r = new Road(true);
        _map[x, y] = new(x, y, r);
        r.IsCrossRoad = IsCrossRoad(x, y);
        Crossroads.Add((x, y), new Crossroad());
    }
}