using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public Placeable data;
    public Vector2Int position;

    public abstract void OnObjectPlaced();
}
