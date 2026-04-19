<<<<<<< HEAD
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class GridObject : MonoBehaviour
{
    private static int _globalCounter = 0;

    public Placeable data;
    public Vector2Int position;
    public Canvas routeCanvas;
    public Button routeButton;
    public Sprite DefaultSprite;
    public Sprite SelectedSprite;
    public TMP_Text OrderText;
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
=======
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public Placeable data;
    public Vector2Int position;

    public abstract void OnObjectPlaced();
>>>>>>> origin/master
}
