using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    private static int _globalCounter = 0;

    public Placeable data;
    public Vector2Int position;
    private int _id;

    public int ID
    {
        get
        {
            return _id;
        }
    }

    public abstract void OnObjectPlaced();

    protected virtual void Awake()
    {
        _id = _globalCounter;
        _globalCounter++;
    }
}
