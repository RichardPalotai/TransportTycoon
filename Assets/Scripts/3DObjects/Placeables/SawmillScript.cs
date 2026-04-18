using UnityEngine;

public class SawmillScript : GridObject
{
    #region Private variables
    [SerializeField]
    private int ID;
    #endregion

    #region Public override methods
    public override void OnObjectPlaced()
    {
        Debug.LogWarning("Placed Sawmill");
    }
    #endregion

    #region Unity calls
    void Awake()
    {
        DefaultSprite = routeButton.image.sprite;
        SelectedSprite = routeButton.spriteState.highlightedSprite;
        routeCanvas.gameObject.SetActive(false);
    }

    void Start()
    {
        ID = VehicleRouteHandler.instance.TestObjID++;
        routeButton.onClick.AddListener(OnIconClicked);


        GameViewModel.instance.OnRouteDisplayChanged += HandleRouteDisplayChanged;
        VehicleRouteHandler.instance.OnRouteReset += HandleRouteReset;
        VehicleRouteHandler.instance.OnRouteChanged += HandleRouteChanged;

        HandleRouteDisplayChanged(GameViewModel.instance.IsRouteDisplayOn);
        HandleRouteReset();
        HandleRouteChanged();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Camera.main.transform.rotation;
        routeCanvas.transform.LookAt(routeCanvas.transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
    #endregion

    #region Events
    private void OnIconClicked()
    {
        Debug.Log("Route Icon clicked");
        try
        {
            if (VehicleRouteHandler.instance.IsPlaceSelected(ID))
            {
                routeButton.image.sprite = SelectedSprite;
            }
            else
            {
                routeButton.image.sprite = DefaultSprite;
            }
            OrderText.text = VehicleRouteHandler.instance.GetPlaceOrder(ID).ToString();
        }
        catch (RouteException e)
        {
            RouteErrorHandler.instance.DisplayError(e.Message);
        }
    }

    private void HandleRouteDisplayChanged(bool isOn)
    {
        routeCanvas.gameObject.SetActive(isOn);
        if (VehicleRouteHandler.instance.IsPlaceInRoute(ID) && isOn)
        {
            routeButton.image.sprite = SelectedSprite;
        }
        else
        {
            routeButton.image.sprite = DefaultSprite;
        }
    }

    private void HandleRouteReset()
    {
        routeButton.image.sprite = DefaultSprite;
        OrderText.text = VehicleRouteHandler.instance.GetPlaceOrder(ID).ToString();
    }

    private void HandleRouteChanged()
    {
        OrderText.text = VehicleRouteHandler.instance.GetPlaceOrder(ID).ToString();
    }
    #endregion
}
