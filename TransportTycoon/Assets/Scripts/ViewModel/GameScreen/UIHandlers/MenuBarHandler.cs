using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace ViewModel.GameScreen.UIHandlers
{
    public class MenuBarHandler : MonoBehaviour
    {
        public static MenuBarHandler instance;

        [SerializeField]
        private TextMeshProUGUI AccountBalance_text;
        [SerializeField]
        private Button Pause_btn;
        [SerializeField]
        private Button Play_btn;
        [SerializeField]
        private Button Forward_btn;
        [SerializeField]
        private Button FastForward_btn;
        [SerializeField]
        private TextMeshProUGUI CalendarTime_text;
        [SerializeField]
        private TextMeshProUGUI Time_text;
        
        /// <summary>
        /// The account balance of the player
        /// </summary>
        public int AccountBalance
        { 
            get { return int.Parse(AccountBalance_text.text); }
            set
            {
                if (value < 0)
                    throw new Exception("Can't set negative account balance");
                else
                    AccountBalance_text.text = value.ToString();
            }
        }

        /// <summary>
        /// The current in-game calendar time
        /// </summary>
        public DateTime CalendarTime
        { 
            get { return DateTime.Parse(CalendarTime_text.text); }
            set
            {
                CalendarTime_text.text = value.ToString("yyyy.MM.dd");
            }
        }

        /// <summary>
        /// The current in-game time for the day
        /// </summary>
        public DateTime Time
        { 
            get { return DateTime.Parse(CalendarTime_text.text); }
            set
            {
                Time_text.text = value.ToString("HH:mm:ss");
            }
        }

        void Awake()
        {
            AccountBalance = 0;
            CalendarTime = DateTime.Now;
            Time = DateTime.Now;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Pause_btn.onClick.AddListener(OnPauseClicked);
            Play_btn.onClick.AddListener(OnPlayClicked);
            Forward_btn.onClick.AddListener(OnForwardClicked);
            FastForward_btn.onClick.AddListener(OnFastForwardClicked);

            // Only for debug pruposes
            Debug.Log(AccountBalance);
            Debug.Log(CalendarTime);
            Debug.Log(Time);
        }

        // Update is called once per frame
        void Update()
        {
            // The Properties can refresh real time if they are changed in non-monobehavior C# file
            // This is just for test purposes
            CalendarTime = DateTime.Now;
            Time = DateTime.Now;
        }

        private void OnPauseClicked()
        {
            // TODO - Connect to Model
        }
        private void OnPlayClicked()
        {
            // TODO - Connect to Model
        }
        private void OnForwardClicked()
        {
            // TODO - Connect to Model
        }
        private void OnFastForwardClicked()
        {
            // TODO - Connect to Model
        }
    }
}
