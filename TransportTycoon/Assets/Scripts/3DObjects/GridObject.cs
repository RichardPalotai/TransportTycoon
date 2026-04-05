using UnityEngine;
using UnityEngine.UI;

public abstract class GridObject : MonoBehaviour
{
    public Placeable data;
    public Vector2Int position;
    public Canvas routeCanvas;
    public Button routeButton;

    public abstract void OnObjectPlaced();
}
