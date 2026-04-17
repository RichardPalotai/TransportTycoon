using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
    public class VehicleDataHandler : MonoBehaviour
    {
        // INSTANCE MUST BE SET ACTIVE WHEN VEHICLE IS CLICKED - (3D modell - Bálint)
        public static VehicleDataHandler instance;

        [SerializeField]
        private TextMeshProUGUI ID_Text;
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

        public int ID
        {
            get { return int.Parse(ID_Text.text); }
            set
            {
                ID_Text.text = value.ToString();
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
            if (SelectedCar != null)
            {
                // TODO - Set properties to the selected car's (3D modell - Bálint)
            }
            else
            {
                OnEscapePressed();
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
            Destroy(SelectedCar);
            SetDefaultValues();
        }

        private void OnEscapePressed()
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SetDefaultValues();
                gameObject.SetActive(false);
            }
        }

        private void SetDefaultValues()
        {
            SelectedCar = null;
            ID = -2;
            Condition = "None";
            Capacity = 9999;
            Worth = 0;
            RepairCost = 0;
        }

        public void SetButtonsActive(bool state)
        {
            Close_btn.interactable = state;
            SetRoute_btn.interactable = state;
            Repair_btn.interactable = state;
            Sell_btn.interactable = state;
        }
    }   
}
