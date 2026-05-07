using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VehicleRouteHandler : MonoBehaviour
{
    public static VehicleRouteHandler instance;

    #region Private variables
    [SerializeField]
    private Button Cancel_btn;
    [SerializeField]
    private Button Reset_btn;
    [SerializeField]
    private Button Ok_btn;

    private LinkedList<int> currentRoute = new LinkedList<int>();
    #endregion

    #region Public variables
    // TODO - Connect to REAL ID from Model (delete dummy data) <BINDING>
    public int TestObjID = 0;
    #endregion

    #region Events
    public event Action OnRouteReset;
    public event Action OnRouteChanged;
    #endregion

    #region Unity calls
    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cancel_btn.onClick.AddListener(OnCancelClicked);
        Reset_btn.onClick.AddListener(OnResetClicked);
        Ok_btn.onClick.AddListener(OnOkClicked);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Private methods
    private void OnCancelClicked()
    {
        ResetRoute();
        GameViewModel.instance.SetRouteDisplayActive(false);
    }

    private void OnResetClicked()
    {
        ResetRoute();
    }

    private void OnOkClicked()
    {
        SaveRoute();
        GameViewModel.instance.SetRouteDisplayActive(false);
    }

    private bool IsPlaceAnEnd(int ID)
    {
        if (currentRoute.First.Value == ID || currentRoute.Last.Value == ID)
        {
            return true;
        }
        else
        {
            throw new RouteException("Places with two or more neighbours cannot be removed from route");
        }
    }

    private bool IsPlaceNear(int toID)
    {
        // Facility toFacility = Game.instance.Player.Facilities.Find(x => x.ID == toID);
        // Facility fromFacility = null;
        // foreach (int fromID in currentRoute)
        // {
        //     fromFacility = Game.instance.Player.Facilities.Find(x => x.ID == fromID);
        //     if (PathFinder.HasPathBetweenFacilities(Game.instance.Map, fromFacility, toFacility))
        //     {
        //         return true;
        //     }
        // }
        // throw new RouteException("The place is too far from the current route");
        return true;
    }
    #endregion

    #region Public methods
    public bool IsPlaceSelected(int ID)
    {
        if (currentRoute.Contains(ID) && IsPlaceAnEnd(ID))
        {
            currentRoute.Remove(ID);
            OnRouteChanged?.Invoke();
            Debug.Log("[REMOVED] " + ID + " from " + currentRoute.ToList());
            return false;
        }
        else if (!currentRoute.Contains(ID) && IsPlaceNear(ID))
        {
            currentRoute.AddLast(ID);
            OnRouteChanged?.Invoke();
            Debug.Log("[ADDED] " + ID + " to " + currentRoute.ToList());
            return true;
        }

        return false;
    }

    public bool IsPlaceInRoute(int ID)
    {
        return currentRoute.Contains(ID);
    }

    public string GetPlaceOrder(int ID)
    {
        var order = currentRoute.ToList().IndexOf(ID);
        if (currentRoute.Count == 0)
            return string.Empty;
        else if (currentRoute.Count > 0 && order != -1)
            return (order + 1).ToString();
        else
            return "N/A";
    }

    /// <summary>
    /// Loads the current route from Model if there is one
    /// </summary>
    public void LoadRoute()
    {
        // TODO - connect REAL DATA!!!! <BINDING>
        Vehicle f = Game.instance.Player.Vehicles.Find(v => v.ID == GameViewModel.instance.SelectedObject.GetComponent<SawmillScript>().ID);
        if (f == null)
            return;

        // TODO - connect REAL DATA!!!! <BINDING>
        currentRoute = new LinkedList<int>(Game.instance.Player.Vehicles.Find(v => v.ID == GameViewModel.instance.SelectedObject.GetComponent<SawmillScript>().ID).Route.Select(f => f.ID));
    }

    /// <summary>
    /// Saves the current route to model
    /// </summary>
    private void SaveRoute()
    {
        // TODO - connect REAL DATA!!! <BINDING>
        //Game.instance.Player.Vehicles.Find(v => v.ID == GameViewModel.instance.SelectedObject.GetComponent<SawmillScript>().ID).SetRoute(currentRoute, Game.instance.Player.Facilities);
    }

    /// <summary>
    /// Resets the current route list and notifies the Objects to be deselected
    /// </summary>
    public void ResetRoute()
    {
        currentRoute = new LinkedList<int>();
        OnRouteReset?.Invoke();
    }
    #endregion
}
