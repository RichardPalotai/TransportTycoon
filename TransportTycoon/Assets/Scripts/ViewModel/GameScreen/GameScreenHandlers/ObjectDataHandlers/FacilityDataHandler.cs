using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
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
        public GameObject SelectedFacility;
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
        public string Produce
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
                // TODO - Set properties to the selected facility's (3D modell - Bálint)
                OnkKeyPressed();
            }
        }
        #endregion
        
        #region Button click events
        private void OnCloseClicked()
        {
            SetDefaultValues();
            gameObject.SetActive(false);
        }
        #endregion

        #region Private methods
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
            SelectedFacility = null;
            ID = -2;
            Traffic = 0;
            Consume = "None";
            Produce = "None";
        }
        #endregion

        #region Public methods
        public void SetButtonsActive(bool state)
        {
            Close_btn.interactable = state;
        }
        #endregion
    }   
}
