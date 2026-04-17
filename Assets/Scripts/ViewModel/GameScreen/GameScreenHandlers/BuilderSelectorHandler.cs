using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
    public class BuilderSelectorHandler : MonoBehaviour
    {
        public static BuilderSelectorHandler instance;

        #region Private variables
        [SerializeField]
        private TextMeshProUGUI Price_Text;
        [SerializeField]
        private GameObject PriceTag;

        [SerializeField]
        private Button Mouse_btn;
        [SerializeField]
        private Button Road_btn;
        [SerializeField]
        private Button BusStop_btn;
        [SerializeField]
        private Button TrafficLight_btn;
        [SerializeField]
        private Button Bus_btn;
        [SerializeField]
        private Button Car_btn;
        [SerializeField]
        private Button Truck_btn;
        [SerializeField]
        private Button Minivan_btn;
        [SerializeField]
        private Button SelectedButton = null;

        private enum Building { NONE, ROAD, BUSSTOP, TRAFFICLIGHT, BUS, CAR, TRUCK, MINIVAN}
        private Building selectedBuilding;
        #endregion

        #region Properties
        private int Price
        {
            get { return int.Parse(Price_Text.text); }
            set
            {
                if (value <= 0)
                    throw new Exception("Price is not positive");
                else
                    Price_Text.text = value.ToString();
            }
        }
        public bool IsMouseSelected
        {
            get { return SelectedButton == Mouse_btn; }
        }
        #endregion

        #region Unity calls
        void Awake()
        {
            instance = this;

            SelectButton(Mouse_btn);
            selectedBuilding = Building.NONE;
            RemovePriceTag();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Mouse_btn.onClick.AddListener(OnMouseSelected);
            Road_btn.onClick.AddListener(OnRoadSelected);
            BusStop_btn.onClick.AddListener(OnBusStopSelected);
            TrafficLight_btn.onClick.AddListener(OnTrafficLightSelected);
            Bus_btn.onClick.AddListener(OnBusSelected);
            Car_btn.onClick.AddListener(OnCarSelected);
            Truck_btn.onClick.AddListener(OnTruckSelected);
            Minivan_btn.onClick.AddListener(OnMinivanSelected);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion

        #region Button click events
        private void OnMouseSelected()
        {
            SelectButton(Mouse_btn);
            selectedBuilding = Building.NONE;
            RemovePriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.MOUSE;
        }
        private void OnRoadSelected()
        {
            SelectButton(Road_btn);
            selectedBuilding = Building.ROAD;
            AddPriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.BUILD;
        }
        private void OnBusStopSelected()
        {
            SelectButton(BusStop_btn);
            selectedBuilding = Building.BUSSTOP;
            AddPriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.BUILD;
        }
        private void OnTrafficLightSelected()
        {
            SelectButton(TrafficLight_btn);
            selectedBuilding = Building.TRAFFICLIGHT;
            AddPriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.BUILD;
        }
        private void OnBusSelected()
        {
            SelectButton(Bus_btn);
            selectedBuilding = Building.BUS;
            AddPriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.BUILD;
        }
        private void OnCarSelected()
        {
            SelectButton(Car_btn);
            selectedBuilding = Building.CAR;
            AddPriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.BUILD;
        }
        private void OnTruckSelected()
        {
            SelectButton(Truck_btn);
            selectedBuilding = Building.TRUCK;
            AddPriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.BUILD;
        }
        private void OnMinivanSelected()
        {
            SelectButton(Minivan_btn);
            selectedBuilding = Building.MINIVAN;
            AddPriceTag();
            GameViewModel.instance.Gamemode = GameViewModel.GameMode.BUILD;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Sets the given button's outline to visible/invisible and background color to black/white depending on selection.
        /// If a button was already selected It will be deselected and the button given as parameter will be selected.
        /// </summary>
        /// <param name="btn">Button that is ought to be selected</param>
        private void SelectButton(Button btn)
        {
            if (SelectedButton != null)
            {
                SelectedButton.image.color = Color.white;
                // ColorBlock scb = SelectedButton.colors;
                // scb.normalColor = scb.highlightedColor = scb.selectedColor = scb.pressedColor = Color.white;
                // SelectedButton.colors = scb;

                Outline sol = SelectedButton.GetComponent<Outline>();
                sol.effectDistance = new Vector2(0, 0);
            }

            btn.image.color = Color.lightGray;
            // ColorBlock cb = btn.colors;
            // cb.normalColor = cb.highlightedColor = cb.selectedColor = cb.pressedColor = Color.dimGray;
            // btn.colors = cb;

            Outline ol = btn.GetComponent<Outline>();
            ol.effectDistance = new Vector2(2, -2);

            SelectedButton = btn;
        }

        /// <summary>
        /// Removes the price tag from the BuilderSelectorUI
        /// </summary>
        private void RemovePriceTag()
        {
            RectTransform rt = transform as RectTransform;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -139);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, 258);
            PriceTag.gameObject.SetActive(false);
            Price = 1;
        }

        /// <summary>
        /// Adds the price tag to the BuilderSelectorUI
        /// </summary>
        private void AddPriceTag()
        {
            RectTransform rt = transform as RectTransform;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -172);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, 322);
            PriceTag.gameObject.SetActive(true);
            
            // TODO - Connect to Model (Set prices)
            switch (selectedBuilding)
            {
                case Building.ROAD:
                    Price = 10;
                    break;
                case Building.BUSSTOP:
                    Price = 30;
                    break;
                case Building.TRAFFICLIGHT:
                    Price = 50;
                    break;
                case Building.BUS:
                    Price = 150;
                    break;
                case Building.CAR:
                    Price = 100;
                    break;
                case Building.TRUCK:
                    Price = 200;
                    break;
                case Building.MINIVAN:
                    Price = 150;
                    break;
                
                default:
                    Price = 1;
                    break;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Sets the "Mouse" button's outline to red/black and sets build mode buttons on/off depending on state
        /// </summary>
        /// <param name="state">true == demolish UI on / false == demolish UI off</param>
        public void SetDemolishMode(bool state)
        {
            if (state)
            {
                Outline dol = SelectedButton.GetComponent<Outline>();
                dol.effectColor = Color.red;
            }
            else
            {
                Outline dol = SelectedButton.GetComponent<Outline>();
                dol.effectColor = Color.black;
            }
            SetBuildButtonsActive(!state);
        }

        /// <summary>
        /// Sets all the buttons on/off
        /// </summary>
        /// <param name="state">true == buttons on / false == buttons off</param>
        public void SetButtonsActive(bool state)
        {
            Mouse_btn.interactable = state;
            Road_btn.interactable = state;
            BusStop_btn.interactable = state;
            TrafficLight_btn.interactable = state;
            Bus_btn.interactable = state;
            Car_btn.interactable = state;
            Truck_btn.interactable = state;
            Minivan_btn.interactable = state;
        }

        /// <summary>
        /// Sets Build mode buttons on/off
        /// </summary>
        /// <param name="state">true == build buttons on / false == build buttons off</param>
        public void SetBuildButtonsActive(bool state)
        {
            Road_btn.interactable = state;
            BusStop_btn.interactable = state;
            TrafficLight_btn.interactable = state;
            Bus_btn.interactable = state;
            Car_btn.interactable = state;
            Truck_btn.interactable = state;
            Minivan_btn.interactable = state;
        }
        #endregion
    }
}