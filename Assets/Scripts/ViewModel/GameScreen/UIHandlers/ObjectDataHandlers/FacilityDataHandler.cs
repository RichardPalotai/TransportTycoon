using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
    public class FacilityDataHandler : MonoBehaviour
    {
        // INSTANCE MUST BE SET ACTIVE WHEN FACILITY IS CLICKED - (3D modell - Bálint)
        public static FacilityDataHandler instance;

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

        [SerializeField]
        private GameObject SelectedFacility;

        public int ID
        {
            get { return int.Parse(ID_Text.text); }
            set
            {
                ID_Text.text = value.ToString();
            }
        }

        public int Traffic
        {
            get { return int.Parse(Traffic_Text.text); }
            set
            {
                if (value < 0 || value > 100)
                    throw new Exception("Value is not in % bounds");
                else
                    Traffic_Text.text = value.ToString() + "%";
            }
        }

        public string Consume
        {
            get { return Consume_Text.text; }
            set
            {
                Consume_Text.text = value;
            }
        }

        public string Produce
        {
            get { return Product_Text.text; }
            set
            {
                Product_Text.text = value;
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
            
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (SelectedFacility != null)
            {
                // TODO - Set properties to the selected facility's (3D modell - Bálint)
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
            SelectedFacility = null;
            ID = -2;
            Traffic = 0;
            Consume = "None";
            Produce = "None";
        }

        public void SetButtonsActive(bool state)
        {
            Close_btn.interactable = state;
        }
    }   
}
