using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CityDataHandler : MonoBehaviour
{
    public static CityDataHandler instance;

    #region Private variables
    [SerializeField]
    private TextMeshProUGUI ID_Text;
    [SerializeField]
    private TextMeshProUGUI Satisfaction_Text;
    [SerializeField]
    private TextMeshProUGUI Needs_Text;
    [SerializeField]
    private Button Close_btn;
    #endregion

    #region Public variables
    [SerializeField]
    public City SelectedCity;
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
    /// The satisfaction of a city in %
    /// </summary>
    public double Satisfaction
    {
        get { return double.Parse(Satisfaction_Text.text); }
        set
        {
            if (value < 0 || value > 100)
                throw new Exception("Value is not in % bounds");
            else
                Satisfaction_Text.text = value.ToString() + "%";
        }
    }

    /// <summary>
    /// A list of Resources that the city is lacking
    /// </summary>
    public List<string> Needs
    {
        get { return Needs_Text.text.Split(",").ToList(); }
        set
        {
            Needs_Text.text = string.Join(",", value);
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
        if (SelectedCity != null)
        {
            // TODO - Set properties to the selected city's (3D modell - Bálint) <BINDING>
            ID = SelectedCity.ID;
            Satisfaction = SelectedCity.Satisfaction();
            Needs = SelectedCity.Need.Select(x => x.Key.ToString()).ToList();
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
    private void SetDefaultValues()
    {
        SelectedCity = null;
        ID = -2;
        Satisfaction = 0;
        Needs = new List<string> { "N/A", "N/A", "N/A" };
    }
    #endregion

    #region Public methods
    public void SetButtonsActive(bool state)
    {
        Close_btn.interactable = state;
    }
    #endregion
}