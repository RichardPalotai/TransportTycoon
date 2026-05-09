using System;
using System.Collections.Generic;
using System.Linq;

public sealed partial class Map
{
    private Tile[,] _map;
    public Dictionary<(int, int), Crossroad> Crossroads { get; private set; }
    public int Size => _map.GetLength(0);
    public Map(int size = 100)
    {
        _map = new Tile[size, size];
        Crossroads = new();
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
    public void SetTile(int x, int y, Tile tile)
    {
        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new IndexOutOfRangeException($"X or Y values are out of bounds. Values:\n X: {x},\nY: {y}");
        }
        _map[x, y] = tile;
    }
    private void MarkCity(int x, int y, int areaSize)
    {
        (int, int)[] roadCoord =
        {
            (x + 1, y),
            (x, y + 1),
            (x + 1, y + 1),
            (x + 2, y + 1),
            (x + 1,y + 2)
        };
        for (int i = x; i < x + areaSize; i++)
        {
            for (int j = y; j < y + areaSize; j++)
            {
                if (roadCoord.Contains((i, j)))
                {
                    PlaceCityRoads(i, j);
                }
            }
        }
#if DEBUG
        //Logger.LogMap(this, y: 25);
#endif
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

        //PlaceObject(25, 25, new Factory<Steel>());
        // PlaceObject(Size - 3, 0, new Mine<Iron>());
        // PlaceObject(0, Size - 3, new Farm<Milk>());
        // PlaceObject(Size - 3, Size - 3, new LumberMill<Wood>());
#if DEBUG
        Logger.Log("Map generated successfully");
#endif
    }


}
