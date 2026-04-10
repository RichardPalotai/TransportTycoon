using System.Collections.Generic;

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
    private void AddToCrossRoadIfNeeded(int x, int y, TrafficLight trafficLight)
    {
        var roadCoords = GetTilesNeighborRoadsCoords(x, y);

        foreach (var (dirX, dirY) in roadCoords)
        {
            var road = _map[dirX, dirY].Entity as Road;

            if (road is not null && road.IsCrossRoad)
            {
                if (!Crossroads.ContainsKey((dirX, dirY)))
                    Crossroads[(dirX, dirY)] = new Crossroad();

                Crossroads[(dirX, dirY)].TrafficLights.Add(trafficLight);
                trafficLight.Crossroad = Crossroads[(dirX, dirY)];
                return;
            }
        }
    }
    private bool IsCrossRoad(int x, int y)
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