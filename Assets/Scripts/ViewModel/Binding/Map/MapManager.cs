using UnityEngine;

public class MapManager : MonoBehaviour
{
    private TileResource[,] _map;
    private int _size = 30;

    public int Size
    {
        get
        {
            return _size;
        }
    }
    public TileResource GetTile(int x, int y)
    {
        Debug.LogWarning("Getting Tile: " + x + " " + y);
        return _map[x, y];
    }

    public void SetTile(int x, int y, GridObject? tile)
    {
        _map[x, y].Type = tile;
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _map = new TileResource[_size, _size];
        for (int x = 0; x < _size; x++)
        {
            for (int z = 0; z < _size; z++)
            {
                _map[x, z] = new TileResource(x,z);
            }
        }
    }
}
