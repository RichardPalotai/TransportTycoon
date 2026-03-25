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
            instance = this;

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
            Debug.Log("Account Balance: " + AccountBalance);
            Debug.Log("Calendar Time: " + CalendarTime);
            Debug.Log("Time: " + Time);
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance != null)
            {
                CalendarTime = Game.instance.CurrentTime;
                Time = Game.instance.CurrentTime;
            }
        }

        private void OnPauseClicked()
        {
            Debug.Log("GAME INSTANCE: " + Game.instance);
            Debug.Log("GAME CURRENTIME: " + Game.instance?.CurrentTime);
            Debug.Log("CALENDAR: " + CalendarTime);
            Debug.Log("TIME: " + Time);
            Game.instance.PauseGame();
        }
        private void OnPlayClicked()
        {
            Game.instance.ResumeGame();
        }
        private void OnForwardClicked()
        {
            Game.instance.TimeScale *= 1.4f;
        }
        private void OnFastForwardClicked()
        {
            Game.instance.TimeScale *= 2.0f;
        }

        public void SetButtonsActive(bool status)
        {
            Pause_btn.interactable = status;
            Play_btn.interactable = status;
            Forward_btn.interactable = status;
            FastForward_btn.interactable = status;
        }
    }
}
