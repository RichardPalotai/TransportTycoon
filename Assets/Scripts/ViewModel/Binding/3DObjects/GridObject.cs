using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class GridObject : MonoBehaviour
{
    private static int _globalCounter = 0;

    public GameEntity modelSelf;

    public Placeable data;
    public Vector2Int position;
    public Canvas routeCanvas;
    public Button routeButton;
    public Sprite DefaultSprite;
    public Sprite SelectedSprite;
    public TMP_Text OrderText;
    private int _id;
    public GameObject selfObject;

    public int ID
    {
        get
        {
            return _id;
        }
    }

    public abstract void OnObjectPlaced();

    public virtual void Awake()
    {
        _id = _globalCounter;
        _globalCounter++;
    }
}
