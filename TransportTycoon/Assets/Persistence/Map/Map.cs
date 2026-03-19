using System;

public sealed class Map
{
    private Tile[,] _map;
    public int Size => _map.GetLength(0);
    public Tile GetTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new IndexOutOfRangeException($"X or Y values are out of bounds. Values:\n X: {x},\nY: {y}");
        }
        return _map[x, y];
    }
    public void SetTile(int x, int y, Facility obj)
    {
        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new IndexOutOfRangeException($"X or Y values are out of bounds. Values:\n X: {x},\nY: {y}");
        }
        _map[x, y].Object = obj;
    }

}