using UnityEngine;

public class TrafficLightScript : GridObject
{
    // #region Private variables

    // #endregion

    // #region Public override methods
    public override void OnObjectPlaced()
    {
        Debug.LogWarning("Placed Traffic Light");
    }
    // #endregion

    // #region Unity calls
    public override void Awake()
    {
        base.Awake();
        // DefaultSprite = routeButton.image.sprite;
        // SelectedSprite = routeButton.spriteState.highlightedSprite;
        routeCanvas.gameObject.SetActive(false);
    }

    void Start()
    {
        routeButton.onClick.AddListener(OnIconClicked);


        GameViewModel.instance.OnRouteDisplayChanged += HandleRouteDisplayChanged;
        // VehicleRouteHandler.instance.OnRouteReset += HandleRouteReset;
        // VehicleRouteHandler.instance.OnRouteChanged += HandleRouteChanged;

        HandleRouteDisplayChanged(GameViewModel.instance.IsRouteDisplayOn);
        // HandleRouteReset();
        // HandleRouteChanged();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Camera.main.transform.rotation;
        routeCanvas.transform.LookAt(routeCanvas.transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
    // #endregion

    // #region Events
    private void OnIconClicked()
    {
        Debug.Log("Route Icon clicked");
        if (GameViewModel.instance.Gamemode == GameViewModel.GameMode.MOUSE)
            GameViewModel.instance.SelectObject(this.modelSelf);
    }

    private void HandleRouteDisplayChanged(bool isOn)
    {
        routeCanvas.gameObject.SetActive(isOn);
    }

    // private void HandleRouteReset()
    // {
    //     routeButton.image.sprite = DefaultSprite;
    //     OrderText.text = VehicleRouteHandler.instance.GetPlaceOrder(ID).ToString();
    // }

    // private void HandleRouteChanged()
    // {
    //     OrderText.text = VehicleRouteHandler.instance.GetPlaceOrder(ID).ToString();
    // }
    // #endregion
}
