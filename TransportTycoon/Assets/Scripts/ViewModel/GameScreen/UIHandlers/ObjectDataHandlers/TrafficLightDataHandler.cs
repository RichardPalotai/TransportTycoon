using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
    public class TrafficLightDataHandler : MonoBehaviour
    {
        // INSTANCE MUST BE SET ACTIVE WHEN TRAFFIC LIGHT IS CLICKED - (3D modell - Bálint)
        public static TrafficLightDataHandler instance;

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

        [SerializeField]
        public GameObject SelectedTrafficLight;

        public int ID
        {
            get { return int.Parse(ID_Text.text); }
            set
            {
                ID_Text.text = value.ToString();
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

        public int GreenLight
        {
            get { return int.Parse(GreenLight_Text.text); }
            set
            {
                if (value <= 0 || value > 60)
                    throw new Exception("Green light length is negative or more than 60 seconds");
                else
                    GreenLight_Text.text = value.ToString();
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
                // TODO - Set properties to the selected car's (3D modell - Bálint)
                OnkKeyPressed();
            }
        }
        
        private void OnCloseClicked()
        {
            SetDefaultValues();
            gameObject.SetActive(false);
        }
        private void OnMinusClicked()
        {
            // TODO - Connect to Model
        }
        private void OnPlusClicked()
        {
            // TODO - Connect to Model
        }
        private void OnSellClicked()
        {
            // TODO - Connect to model
            Destroy(SelectedTrafficLight);
            SetDefaultValues();
        }

        private void OnkKeyPressed()
        {
            if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
            {
                SetDefaultValues();
                gameObject.SetActive(false);
            }
        }

        private void SetDefaultValues()
        {
            SelectedTrafficLight = null;
            ID = -2;
            Worth = 0;
            GreenLight = 1;
        }

        public void SetButtonsActive(bool state)
        {
            Close_btn.interactable = state;
            Plus_btn.interactable = state;
            Minus_btn.interactable = state;
            Sell_btn.interactable = state;
        }
    }   
}
