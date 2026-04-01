using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
    public class BuilderSelectorHandler : MonoBehaviour
    {
        public static BuilderSelectorHandler instance;

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

        private bool buildMode;
        private string selectedBuilding; // TODO Connect to 3D Model

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

        // TODO - Make objects follow mouse based on BuildMode and SelectedBuilding
        public bool BuildMode
        {
            get { return buildMode; }
        }

        public string SelectedBuilding
        {
            get { return selectedBuilding; }
        }
        // TODO

        void Awake()
        {
            instance = this;

            SelectButton(Mouse_btn);
            selectedBuilding = "None";
            RemovePriceTag();
            buildMode = false;
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
            if (buildMode && Mouse.current != null)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    // TODO - Connect to model (Add building to map)
                    // TODO - Place objects on actual landscape (3D modell - Bálint)
                }
            }
        }

        private void OnMouseSelected()
        {
            SelectButton(Mouse_btn);
            selectedBuilding = "None";
            RemovePriceTag();
            buildMode = false;
        }
        private void OnRoadSelected()
        {
            SelectButton(Road_btn);
            selectedBuilding = "Road";
            AddPriceTag();
            buildMode = true;
        }
        private void OnBusStopSelected()
        {
            SelectButton(BusStop_btn);
            selectedBuilding = "BusStop";
            AddPriceTag();
            buildMode = true;
        }
        private void OnTrafficLightSelected()
        {
            SelectButton(TrafficLight_btn);
            selectedBuilding = "TrafficLight";
            AddPriceTag();
            buildMode = true;
        }
        private void OnBusSelected()
        {
            SelectButton(Bus_btn);
            selectedBuilding = "Bus";
            AddPriceTag();
            buildMode = true;
        }
        private void OnCarSelected()
        {
            SelectButton(Car_btn);
            selectedBuilding = "Car";
            AddPriceTag();
            buildMode = true;
        }
        private void OnTruckSelected()
        {
            SelectButton(Truck_btn);
            selectedBuilding = "Truck";
            AddPriceTag();
            buildMode = true;
        }
        private void OnMinivanSelected()
        {
            SelectButton(Minivan_btn);
            selectedBuilding = "Minivan";
            AddPriceTag();
            buildMode = true;
        }

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

        private void RemovePriceTag()
        {
            RectTransform rt = transform as RectTransform;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -139);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, 258);
            PriceTag.gameObject.SetActive(false);
            Price = 1;
        }

        private void AddPriceTag()
        {
            RectTransform rt = transform as RectTransform;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -172);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, 322);
            PriceTag.gameObject.SetActive(true);
            
            // TODO - Connect to Model (Set prices)
            switch (selectedBuilding.ToLower())
            {
                case "road":
                    Price = 10;
                    break;
                case "busstop":
                    Price = 30;
                    break;
                case "trafficlight":
                    Price = 50;
                    break;
                case "bus":
                    Price = 150;
                    break;
                case "car":
                    Price = 100;
                    break;
                case "truck":
                    Price = 200;
                    break;
                case "minivan":
                    Price = 150;
                    break;
                
                default:
                    Price = 1;
                    break;
            }
        }

        public void SetButtonsActive(bool status)
        {
            Mouse_btn.interactable = status;
            Road_btn.interactable = status;
            BusStop_btn.interactable = status;
            TrafficLight_btn.interactable = status;
            Bus_btn.interactable = status;
            Car_btn.interactable = status;
            Truck_btn.interactable = status;
            Minivan_btn.interactable = status;
        }
    }
}