using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MenuBarHandler : MonoBehaviour
{
    public static MenuBarHandler instance;

    #region Private variables
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
    [SerializeField]
    private Button SelectedButton = null;
    #endregion

    #region Properties
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
        get { return DateTime.Parse(Time_text.text); }
        set
        {
            Time_text.text = value.ToString("HH:mm:ss");
        }
    }
    #endregion

    #region Unity calls
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

        SelectButton(Play_btn);

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
            AccountBalance = Game.instance.AccountBalance;
            Time = Game.instance.CurrentTime;
            CalendarTime = Game.instance.CurrentTime;
        }
    }
    #endregion

    #region Private methods
    private void OnPauseClicked()
    {
        Debug.Log("GAME INSTANCE: " + Game.instance);
        Debug.Log("GAME CURRENTIME: " + Game.instance?.CurrentTime);
        Debug.Log("CALENDAR: " + CalendarTime);
        Debug.Log("TIME: " + Time);
        Game.instance.PauseGame();
        SelectButton(Pause_btn);
    }
    private void OnPlayClicked()
    {
        Game.instance.ResumeGame();
        SelectButton(Play_btn);
    }
    private void OnForwardClicked()
    {
        if (Game.instance.IsPaused)
            Game.instance.ResumeGame();

        Game.instance.TimeScale = 64.0f;
        SelectButton(Forward_btn);
    }
    private void OnFastForwardClicked()
    {
        if (Game.instance.IsPaused)
            Game.instance.ResumeGame();

        Game.instance.TimeScale = 512.0f;
        SelectButton(FastForward_btn);

    }
    private void SelectButton(Button btn)
    {
        if (SelectedButton != null)
            SelectedButton.image.color = Color.white;

        btn.image.color = Color.lightGray;

        SelectedButton = btn;
    }
    #endregion

    #region Public methods
    public void SetButtonsActive(bool status)
    {
        Pause_btn.interactable = status;
        Play_btn.interactable = status;
        Forward_btn.interactable = status;
        FastForward_btn.interactable = status;
    }

    public void SelectPlayButton()
    {
        SelectButton(Play_btn);
    }
    #endregion
}
