using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FacilityDataHandler : MonoBehaviour
{
    public static FacilityDataHandler instance;

    #region Private variables
    [SerializeField]
    private TextMeshProUGUI ID_Text;
    [SerializeField]
    private TextMeshProUGUI Traffic_Text;
    [SerializeField]
    private TextMeshProUGUI Consume_Text;
    [SerializeField]
    private TextMeshProUGUI Product_Text;
    [SerializeField]
    private Button Close_btn;
    #endregion

    #region Public variables
    [SerializeField]
    public ProdFacility SelectedFacility;
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
    /// The traffic of the facility in % depending on the vehicles visiting it
    /// </summary>
    public double Traffic
    {
        get { return double.Parse(Traffic_Text.text); }
        set
        {
            if (value < 0 || value > 100)
                throw new Exception("Value is not in % bounds");
            else
                Traffic_Text.text = Math.Round(value, 2).ToString() + "%";
        }
    }

    /// <summary>
    /// The resource a facility needs 
    /// </summary>
    public string Consume
    {
        get { return Consume_Text.text; }
        set
        {
            Consume_Text.text = value;
        }
    }

    /// <summary>
    /// The resource a facility produces
    /// </summary>
    public string Product
    {
        get { return Product_Text.text; }
        set
        {
            Product_Text.text = value;
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

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedFacility != null)
        {
            ID = SelectedFacility.ID;
            Traffic = SelectedFacility.Traffic(Game.instance.Player);
            switch (SelectedFacility.GetType().GetGenericArguments()[0].Name)
            {
                case "Steel":
                    Consume = "Iron";
                    Product = "Steel";
                    break;
                case "Paper":
                    Consume = "Wood";
                    Product = "Paper";
                    break;
                case "Wood":
                    Consume = "----";
                    Product = "Wood";
                    break;
                case "Iron":
                    Consume = "----";
                    Product = "Iron";
                    break;
                case "Cheese":
                    Consume = "----";
                    Product = "Cheese";
                    break;
                case "Milk":
                    Consume = "----";
                    Product = "Milk";
                    break;
                case "Egg":
                    Consume = "----";
                    Product = "Egg";
                    break;
                default:
                    Consume = "N/A";
                    Product = "N/A";
                    break;
            }
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
    #endregion

    #region Private methods
    /// <summary>
    /// Sets the default values to the data display for error cheking purposes
    /// </summary>
    private void SetDefaultValues()
    {
        SelectedFacility = null;
        ID = -2;
        Traffic = 0;
        Consume = "None";
        Product = "None";
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
    }
    #endregion
}