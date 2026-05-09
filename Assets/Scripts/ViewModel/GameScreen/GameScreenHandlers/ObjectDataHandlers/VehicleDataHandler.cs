using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VehicleDataHandler : MonoBehaviour
{
    public static VehicleDataHandler instance;

    #region Private variables
    [SerializeField]
    private TextMeshProUGUI ID_Text;
    [SerializeField]
    private TextMeshProUGUI Resource_Text;
    [SerializeField]
    private TextMeshProUGUI Capacity_Text;
    [SerializeField]
    private TextMeshProUGUI Condition_Text;
    [SerializeField]
    private TextMeshProUGUI RepairCost_Text;
    [SerializeField]
    private TextMeshProUGUI Worth_Text;
    [SerializeField]
    private Button Close_btn;
    [SerializeField]
    private Button SetRoute_btn;
    [SerializeField]
    private Button Repair_btn;
    [SerializeField]
    private Button Sell_btn;
    #endregion

    #region Public variables
    [SerializeField]
    public Vehicle SelectedVehicle;
    #endregion

    #region Properties
    /// <summary>
    /// General object ID
    /// </summary>
    public int ID
    {
        get { return int.Parse(ID_Text.text); }
        set
        {
            ID_Text.text = value.ToString();
        }
    }

    /// <summary>
    /// The resource that the vehicle can carry
    /// </summary>
    public string Resource
    {
        get { return Resource_Text.text; }
        set
        {
            Resource_Text.text = value;
        }
    }

    /// <summary>
    /// The maximum capacity of the resource the vehicle can carry, must be a positive number
    /// </summary>
    public int Capacity
    {
        get { return int.Parse(Capacity_Text.text); }
        set
        {
            if (value <= 0)
                throw new Exception("Capacity is not positive");
            else
                Capacity_Text.text = value.ToString();
        }
    }

    /// <summary>
    /// The condition of the vehicle (Obsolete/Service/New)
    /// </summary>
    public double Condition
    {
        get { return double.Parse(Condition_Text.text); }
        set
        {
            if (value < 0 || value > 100)
                throw new Exception("Value is not in % bounds");
            else
                Condition_Text.text = value.ToString() + "%";
        }
    }

    /// <summary>
    /// The price of changing the vehicle's condition to "New", must be non-negative number
    /// </summary>
    public double RepairCost
    {
        get { return int.Parse(RepairCost_Text.text); }
        set
        {
            if (value < 0)
                throw new Exception("Repair cost is negative");
            else
                RepairCost_Text.text = value.ToString();
        }
    }

    /// <summary>
    /// The worth of the vehicle, depends on the Condition and must be non-negative number
    /// </summary>
    public double Worth
    {
        get { return int.Parse(Worth_Text.text); }
        set
        {
            if (value < 0)
                throw new Exception("Worth is negative");
            else
                Worth_Text.text = value.ToString();
        }
    }
    #endregion

    #region Unity calls
    void Awake()
    {
        instance = this;

        SetDefaultValues();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Close_btn.onClick.AddListener(OnCloseClicked);
        SetRoute_btn.onClick.AddListener(OnSetRouteClicked);
        Repair_btn.onClick.AddListener(OnRepairClicked);
        Sell_btn.onClick.AddListener(OnSellClicked);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedVehicle != null)
        {
            // TODO - Set properties to the selected car's (3D modell - Bálint) <BINDING>
            ID = SelectedVehicle.ID;
            switch (SelectedVehicle)
            {
                case Bus:
                    Resource = "People";
                    Capacity = (SelectedVehicle as Bus).PassengersCount;
                    break;
                case Car:
                    Resource = "People";
                    Capacity = (SelectedVehicle as Car).PassengersCount;
                    break;
                case Minivan:
                    Resource = (SelectedVehicle as Minivan).CargoType.NameString;
                    Capacity = (SelectedVehicle as Minivan).CargoCapacity;
                    break;
                case Truck:
                    Resource = (SelectedVehicle as Truck).CargoType.NameString;
                    Capacity = (SelectedVehicle as Truck).CargoCapacity;
                    break;
                default:
                    Resource = "N/A";
                    Capacity = -1;
                    break;
            }
            Condition = SelectedVehicle.Condition;
            RepairCost = SelectedVehicle.RepairCost;
            Worth = SelectedVehicle.Worth;
            CheckDeselectKey();
        }
    }
    #endregion

    #region Button click events
    private void OnCloseClicked()
    {
        SetDefaultValues();
        gameObject.SetActive(false);
    }
    private void OnSetRouteClicked()
    {
        GameViewModel.instance.SetRouteDisplayActive(true);
    }
    private void OnRepairClicked()
    {
        // TODO - Connect to REAL DATA <BINDING>
        Game.instance.Player.Vehicles.Find(v => v.ID == SelectedVehicle.ID).Repair(Game.instance.Player);
    }
    private void OnSellClicked()
    {
        // TODO - Connect to model <BINDING>
        BuildingPlacer.instance.DemolishBuilding(SelectedVehicle.X, SelectedVehicle.Y, SelectedVehicle);
        Game.instance.Player.SellItem(SelectedVehicle);
        SetDefaultValues();
    }
    #endregion

    #region Private methods
    /// <summary>
    /// On k key pressed sets the Data Display off and to default values
    /// </summary>
    private void CheckDeselectKey()
    {
        if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            SetDefaultValues();
            gameObject.SetActive(false);
        }
    }

    private void SetDefaultValues()
    {
        SelectedVehicle = null;
        ID = -2;
        Resource = "None";
        Capacity = 9999;
        Condition = 0;
        RepairCost = 0;
        Worth = 0;
    }
    #endregion

    #region Public methods
    public void SetButtonsActive(bool state)
    {
        Close_btn.interactable = state;
        SetRoute_btn.interactable = state;
        Repair_btn.interactable = state;
        Sell_btn.interactable = state;
    }
    #endregion
}
