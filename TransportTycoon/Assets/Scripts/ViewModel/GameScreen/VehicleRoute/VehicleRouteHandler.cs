using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleRouteHandler : MonoBehaviour
{
    public static VehicleRouteHandler instance;

    [SerializeField]
    private Button Cancel_btn;
    [SerializeField]
    private Button Reset_btn;
    [SerializeField]
    private Button Ok_btn;

    private LinkedList<int> currentRoute = new LinkedList<int>();
    public bool AddPlace(int ID)
    {
        try
        {    
            if (IsPlaceNear() && !currentRoute.Contains(ID))
            {
                currentRoute.AddLast(ID);
                return true;
            }
        }
        catch (RouteException e)
        {
            RouteErrorHandler.instance.DisplayError(e.Message);
        }

        return false;
    }

    // TODO - Implement this
    private bool IsPlaceNear()
    {
        // Chekc if place is one route away from any place in currentRoute - VEHICLE FUNCTIONALITY

        throw new RouteException("Buliding is too far from route");
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        Cancel_btn.onClick.AddListener(OnCancelClicked);
        Reset_btn.onClick.AddListener(OnResetClicked);
        Ok_btn.onClick.AddListener(OnOkClicked);
        
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCancelClicked()
    {
        GameViewModel.instance.SetRouteDisplayActive(false);
    }

    private void OnResetClicked()
    {
        
    }

    private void OnOkClicked()
    {
        GameObject vehicle = GameViewModel.instance.SelectedObject;
        // Vehicle.SetRoute(currentRoute)
    }
}
