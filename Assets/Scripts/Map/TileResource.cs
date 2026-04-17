using UnityEngine;

public class TileResource
{
    private int X { get; }
    private int Y { get; }

    #nullable enable
    private GridObject? _type;
    #nullable disable

    public TileResource(int x, int y)
    {
        X = x;
        Y = y;
        Type = null;
    }
    public Vector2Int coordinates()
    {
        return new Vector2Int(X, Y);
    }

    #nullable enable
    public GridObject? Type
    {
        set
        {
            _type = value;
        }
        get
        {
            return _type;
        }
    }
    #nullable disable

    public bool IsFree()
    {
        return _type == null;
    }
}
