using UnityEngine;
using UnityEngine.EventSystems;

public class SawmillScript : GridObject
{
    public override void OnObjectPlaced()
    {
        Debug.LogWarning("Placed Sawmill");

    }

    private void OnIconClicked()
    {
        Debug.Log("Route Icon clicked");
        if (!VehicleRouteHandler.instance.AddPlace(data.ID))
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void Awake()
    {
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

    private void HandleRouteDisplayChanged(bool isOn)
    {
        routeCanvas.gameObject.SetActive(isOn);
    }
}
