using System;

public sealed class Map
{
    private static int idCounter = 0;
    private Tile[,] _map;
    public int Size => _map.GetLength(0);
    public Map(int size = 100)
    {
        _map = new Tile[size, size];
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
    public void PlaceObject(int x, int y, MapObject obj)
    {
        int areaSize = GetAreaSize(obj);
        

        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new IndexOutOfRangeException($"X or Y values are out of bounds. Values:\n X: {x},\nY: {y}");
        }
        if (areaSize > 1)
        {
            for (int i = 0; i < areaSize; ++i)
            {
                for (int j = 0; j < areaSize; ++j)
                {
                    if (!_map[i, j].IsFree)
                    {
                        throw new AreaIsNotFreeException();
                    }
                }
            }
        }
        int currentId = ++idCounter;
        _map[x, y] = new(x, y, obj, currentId);
        for (int i = 0; i < areaSize; ++i)
        {
            for (int j = 0; j < areaSize; ++j)
            {
                if (x != i && y != j)
                    _map[i, j] = new (i, j, null, currentId);
            }
        }
    }

    
    public void GenerateMap()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                _map[i,j] = new Tile(i, j);
            }
        }
        
        PlaceObject(0, 0, new Factory<Steel>());
        PlaceObject(Size-3,0, new Mine<Iron>());
        PlaceObject(0, Size - 3, new Farm<Milk>());
        PlaceObject(Size - 3, Size - 3, new LumberMill<Wood>());


    }
    public int GetAreaSize(MapObject obj)
    {
        if (obj is ProdFacility) return 2;
        else if (obj is City) return 3;
        return 1;
    }
}
