using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
    public class CityDataHandler : MonoBehaviour
    {
        // INSTANCE MUST BE SET ACTIVE WHEN CITY IS CLICKED - (3D modell - Bálint)
        public static CityDataHandler instance;

        [SerializeField]
        private TextMeshProUGUI ID_Text;
        [SerializeField]
        private TextMeshProUGUI Satisfaction_Text;
        [SerializeField]
        private TextMeshProUGUI Needs_Text;
        [SerializeField]
        private Button Close_btn;

        [SerializeField]
        public GameObject SelectedCity;

        public int ID
        {
            get { return int.Parse(ID_Text.text); }
            set
            {
                ID_Text.text = value.ToString();
            }
        }

        public int Satisfaction
        {
            get { return int.Parse(Satisfaction_Text.text); }
            set
            {
                if (value < 0 || value > 100)
                    throw new Exception("Value is not in % bounds");
                else
                    Satisfaction_Text.text = value.ToString() + "%";
            }
        }

        public List<string> Needs
        {
            get { return Needs_Text.text.Split(",").ToList(); }
            set
            {
                Needs_Text.text = string.Join(",", value);
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
            
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (SelectedCity != null)
            {
                // TODO - Set properties to the selected city's (3D modell - Bálint)
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
            SelectedCity = null;
            ID = -2;
            Satisfaction = 0;
            Needs = new List<string> {"N/A", "N/A", "N/A"};
        }
    }   
}
