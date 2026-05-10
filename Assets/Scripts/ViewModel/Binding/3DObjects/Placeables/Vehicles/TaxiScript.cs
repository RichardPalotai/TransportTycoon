using UnityEngine;

public class TaxiScript : VehicleScript
{
    public override void OnObjectPlaced()
    {
        Debug.LogWarning("Placed Taxi");

    }

    void Start()
    {
        routeButton.onClick.AddListener(OnIconClicked);
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
        GameViewModel.instance.SelectObject(this.modelSelf);
    }
}