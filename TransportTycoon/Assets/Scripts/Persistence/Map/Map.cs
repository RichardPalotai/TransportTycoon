using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Map
{
    private Tile[,] _map;
    private Dictionary<Road, Crossroad> _crossroads;
    public int Size => _map.GetLength(0);
    public Map(int size = 100)
    {
        _map = new Tile[size, size];
        _crossroads = new();
        GenerateMap();
    }
    public Tile GetTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new IndexOutOfRangeException($"X or Y values are out of bounds. Values:\n X: {x},\nY: {y}");
        }
        return _map[x, y];
    }
    public void PlaceObject(int x, int y, GameEntity entity)
    {
        int areaSize = GetAreaSize(entity);


        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new IndexOutOfRangeException($"X or Y values are out of bounds. Values:\n X: {x},\nY: {y}");
        }

        if (!IsFree(x, y, areaSize))
            throw new NotEnoughSpaceForObjectException();

        if (entity is Road road)
        {
            road.IsCrossRoad = IsCrossRoad(x, y);
            _crossroads.Add(road, new Crossroad());
        }

        if (entity is TrafficLight trafficLight)
        {
            AddToCrossRoadIfNeeded(x, y, trafficLight);
        }

        _map[x, y] = new(x, y, entity);
        MarkAreaTilesWithId(x, y, areaSize);


        Logger.ObjectPlacedLog(entity.GetType(), x, y);
    }
    private void AddToCrossRoadIfNeeded(int x, int y, TrafficLight trafficLight)
    {
        (int dx, int dy)[] dirs =
        {
            (1, -1),
            (1, 1),
            (-1, -1),
            (-1, 1)
        };

        foreach (var (dirX, dirY) in dirs)
        {
            if (_map[x + dirX, y + dirY].Entity is Road r && r.IsCrossRoad)
            {
                _crossroads[r].TrafficLights.Add(trafficLight);
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

    private bool IsFree(int x, int y, int areaSize)
    {
        if (x + areaSize > Size || y + areaSize > Size)
        {
            return false;
        }
        for (int i = x; i < x + areaSize; ++i)
        {
            for (int j = y; j < y + areaSize; ++j)
            {
                if (!_map[i, j].IsFree)
                {
                    UnityEngine.Debug.Log(i + " " + j + " " + _map[i, j].IsFree);
                    return false;
                }
            }
        }
        return true;
    }
    private void MarkAreaTilesWithId(int x, int y, int areaSize)
    {
        for (int i = x; i < x + areaSize; ++i)
        {
            for (int j = y; j < y + areaSize; ++j)
            {
                if (x != i && y != j)
                    _map[i, j] = new(i, j, _map[x, y].Entity.ID);
            }
        }
    }
    public void GenerateMap()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                _map[i, j] = new Tile(i, j);
            }
        }

        // PlaceObject(0, 0, new Factory<Steel>());
        // PlaceObject(Size - 3, 0, new Mine<Iron>());
        // PlaceObject(0, Size - 3, new Farm<Milk>());
        // PlaceObject(Size - 3, Size - 3, new LumberMill<Wood>());

        Logger.Log("Map generated successfully");
    }
    public int GetAreaSize(GameEntity entity)
    {
        if (entity is ProdFacility) return 2;
        else if (entity is City) return 3;
        return 1;
    }
}
