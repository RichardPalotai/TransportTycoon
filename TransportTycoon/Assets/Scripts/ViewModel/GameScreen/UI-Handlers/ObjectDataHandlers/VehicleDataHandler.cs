using System;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleDataHandler : MonoBehaviour
{
    public static VehicleDataHandler instance;

    [SerializeField]
    private TextMeshProUGUI Type_Text;
    [SerializeField]
    private TextMeshProUGUI Condition_Text;
    [SerializeField]
    private TextMeshProUGUI Capacity_Text;
    [SerializeField]
    private TextMeshProUGUI Worth_Text;
    [SerializeField]
    private TextMeshProUGUI RepairCost_Text;
    [SerializeField]
    private Button Close_btn;
    [SerializeField]
    private Button SetRoute_btn;
    [SerializeField]
    private Button Repair_btn;
    [SerializeField]
    private Button Sell_btn;

    [SerializeField]
    private GameObject SelectedCar;

    public string Type
    {
        get { return Type_Text.text; }
        set
        {
            Type_Text.text = value;
        }
    }

    public string Condition
    {
        get { return Condition_Text.text; }
        set
        {
            Condition_Text.text = value;
        }
    }

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

    public int RepairCost
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

    void Awake()
    {
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
        if (SelectedCar != null)
        {
            // TODO - Connect to Model (Set properties)
        }
    }

    private void OnCloseClicked()
    {
        SetDefaultValues();
        gameObject.SetActive(false);
    }
    private void OnSetRouteClicked()
    {
        // TODO - Milestone III.
    }
    private void OnRepairClicked()
    {
        // TODO - Connect to Model
    }
    private void OnSellClicked()
    {
        // TODO - Connect to model
        // TODO - Destroy Car Object
        SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        SelectedCar = null;
        Type = "None";
        Condition = "None";
        Capacity = 9999;
        Worth = 0;
        RepairCost = 0;
    }
}
