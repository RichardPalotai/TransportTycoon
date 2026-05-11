using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TrafficLightDataHandler : MonoBehaviour
{
    public static TrafficLightDataHandler instance;

    #region Private variables
    [SerializeField]
    private TextMeshProUGUI ID_Text;
    [SerializeField]
    private TextMeshProUGUI Worth_Text;
    [SerializeField]
    private TextMeshProUGUI GreenLight_Text;
    [SerializeField]
    private Button Minus_btn;
    [SerializeField]
    private Button Plus_btn;
    [SerializeField]
    private Button Close_btn;
    [SerializeField]
    private Button Sell_btn;
    #endregion

    #region Public variables
    [SerializeField]
    public TrafficLight SelectedTrafficLight;
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
    /// The worth of the traffic light, must be non-negative number
    /// </summary>
    public int Worth
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

    /// <summary>
    /// The length of the green light, between 0 and 61 seconds
    /// </summary>
    public double GreenLight
    {
        get { return double.Parse(GreenLight_Text.text); }
        set
        {
            if (value <= 0 || value > 60)
                throw new Exception("Green light length is negative or more than 60 seconds");
            else
                GreenLight_Text.text = Math.Floor(value).ToString();
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
        Minus_btn.onClick.AddListener(OnMinusClicked);
        Plus_btn.onClick.AddListener(OnPlusClicked);
        Sell_btn.onClick.AddListener(OnSellClicked);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedTrafficLight != null)
        {
            ID = SelectedTrafficLight.ID;
            Worth = SelectedTrafficLight.Cost;
            //Debug.Log(SelectedTrafficLight.Crossroad == null);
            GreenLight = SelectedTrafficLight.Crossroad.GreenInterval; // GreenInterval != null (csak akkor ha szabályosan van letéve a traffic light)
        }
    }
    #endregion

    #region Button click events
    private void OnCloseClicked()
    {
        SetDefaultValues();
        GameViewModel.instance.DeselectObject();
        gameObject.SetActive(false);
    }
    private void OnMinusClicked()
    {
        SelectedTrafficLight.GreenLightDecrement();
    }
    private void OnPlusClicked()
    {
        SelectedTrafficLight.GreenLightIncrement();
    }
    private void OnSellClicked()
    {
        BuildingPlacer.instance.DemolishBuilding(SelectedTrafficLight.X, SelectedTrafficLight.Y, SelectedTrafficLight);
        Game.instance.Player.SellItem(SelectedTrafficLight);
        SetDefaultValues();
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Sets the default values to the data display for error cheking purposes
    /// </summary>
    private void SetDefaultValues()
    {
        SelectedTrafficLight = null;
        ID = -2;
        Worth = 0;
        GreenLight = 1;
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Sets all the buttons on the data display to active/inactive
    /// </summary>
    /// <param name="state">true == buttons active/false == buttons inactive</param>
    public void SetButtonsActive(bool state)
    {
        Close_btn.interactable = state;
        Plus_btn.interactable = state;
        Minus_btn.interactable = state;
        Sell_btn.interactable = state;
    }
    #endregion
}