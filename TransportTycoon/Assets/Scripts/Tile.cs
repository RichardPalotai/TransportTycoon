using UnityEngine;

public class Tile
{
    private int X { get; }
    private int Y { get; }
    private GridObject? _type;

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
        Type = null;
    }
    public Vector2Int coordinates()
    {
        return new Vector2Int(X, Y);
    }

    public GridObject? Type
    {
        set
        {
            _type = value;
        }
    }
    public bool IsFree()
    {
        return _type == null;
    }
}
