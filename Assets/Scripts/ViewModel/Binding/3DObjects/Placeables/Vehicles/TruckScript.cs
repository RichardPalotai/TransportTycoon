using UnityEngine;

public class TruckScript : VehicleScript
{
    public override void OnObjectPlaced()
    {
        Debug.LogWarning("Placed Taxi");

    }

    public override void Awake()
    {
        base.Awake();
        routeCanvas.gameObject.SetActive(false);
    }

    void Start()
    {
        routeButton.onClick.AddListener(OnIconClicked);
        GameViewModel.instance.OnRouteDisplayChanged += HandleRouteDisplayChanged;

        HandleRouteDisplayChanged(GameViewModel.instance.IsRouteDisplayOn);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Camera.main.transform.rotation;
        routeCanvas.transform.LookAt(routeCanvas.transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    private void OnIconClicked()
    {
        Debug.Log("Route Icon clicked");
        if (GameViewModel.instance.Gamemode == GameViewModel.GameMode.MOUSE)
            GameViewModel.instance.SelectObject(this.modelSelf);
    }

    private void HandleRouteDisplayChanged(bool isOn)
    {
        if (GameViewModel.instance.SelectedObject == this.modelSelf)
            routeCanvas.gameObject.SetActive(isOn);
    }
}