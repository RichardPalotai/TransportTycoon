using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class GridObject : MonoBehaviour
{
    public Placeable data;
    public Vector2Int position;
    public Canvas routeCanvas;
    public Button routeButton;
    public Sprite DefaultSprite;
    public Sprite SelectedSprite;
    public TMP_Text OrderText;

    public abstract void OnObjectPlaced();
}
