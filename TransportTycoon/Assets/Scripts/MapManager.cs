using UnityEngine;

public class MapManager : MonoBehaviour
{
    private Tile[,] _map;
    private int _size = 30;

    public int Size
    {
        get
        {
            return _size;
        }
    }
    public Tile GetTile(int x, int y)
    {
        return _map[x, y];
    }

    public void SetTile(int x, int y, Tile tile)
    {
        _map[x, y] =tile;
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _map = new Tile[_size, _size];
        for (int x = 0; x < _size; x++)
        {
            for (int z = 0; z < _size; z++)
            {
                _map[x, z] = new Tile(x,z);
            }
        }
    }
}
