using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using ViewModel.GameScreen.UIHandlers;

public class GameViewModel : MonoBehaviour
{
    public static GameViewModel instance;
    public enum GameMode { BUILD, DEMOLISH, MOUSE }

    [SerializeField]
    private Canvas GameMenu_cnv;
    [SerializeField]
    private Canvas VehicleRoute_cnv;

    private GameMode gameMode;
    private Mouse mouse;
    private Keyboard keyboard;
    private bool IsGameMenuOn;

    private bool IsDemolishOn;

    public GameMode Gamemode
    {
        get { return gameMode; }
        set
        {
            if (gameMode == value)
                return;
            
            gameMode = value;
        }
    }

    void Awake()
    {
        instance = this;

        mouse = Mouse.current;
        keyboard = Keyboard.current;

        IsGameMenuOn = false;
        IsDemolishOn = false;
        gameMode = GameMode.MOUSE;

        Game game = new Game();
        Game.instance = game;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Game.instance.NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        Game.instance.UpdateGame(Time.deltaTime);
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            if (!IsMouseOverUI() && gameMode == GameMode.BUILD)
            {
                BuildingPlacer.instance.AttemptPlacement(mouse.position.ReadValue());
            }
            else if (!IsMouseOverUI() && gameMode == GameMode.DEMOLISH)
            {
                // Destror building from  -- BuildingPlacer --
            }
        }

        if (keyboard != null)
        {
            if (keyboard.escapeKey.wasPressedThisFrame)
                SetMenuActive(!IsGameMenuOn);

            if (keyboard.leftShiftKey.wasPressedThisFrame && !IsGameMenuOn && BuilderSelectorHandler.instance.IsMouseSelected)
            {
                IsDemolishOn = !IsDemolishOn;
                gameMode = IsDemolishOn ? GameMode.DEMOLISH : GameMode.MOUSE;
                BuilderSelectorHandler.instance.SetDemolishMode(IsDemolishOn);
            }
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


    /// <summary>
    /// Sets GameMenuUI canvas on/off
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off</param>
    public void SetMenuActive(bool state)
    {
        IsGameMenuOn = state;
        GameMenu_cnv.gameObject.SetActive(state);
        SetGameScreenUIActive(!state);
        SetCameraControllerActive(!state);

        if (state)
            Game.instance.PauseGame();
        else
            Game.instance.ResumeGame();
    }


    /// <summary>
    /// Sets the VehicleRouteUI canvas on/off
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off</param>
    public void SetRouteDisplayActive(bool state)
    {
        VehicleRoute_cnv.gameObject.SetActive(state);
        SetGameScreenUIActive(!state);
        SetCameraControllerActive(!state);

        if (state)
            Game.instance.PauseGame();
        else
            Game.instance.ResumeGame();
    }

    /// <summary>
    /// Sets the buttons on/off on the GameScreenUI canvas
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off on</param>
    private void SetGameScreenUIActive(bool state)
    {
        MenuBarHandler.instance.SetButtonsActive(state);
        BuilderSelectorHandler.instance.SetButtonsActive(state);

        if (CityDataHandler.instance != null)
        {
            CityDataHandler.instance.SetButtonsActive(state);
            CityDataHandler.instance.enabled = state;
        }
        if (FacilityDataHandler.instance != null)
        {
            FacilityDataHandler.instance.SetButtonsActive(state);
            FacilityDataHandler.instance.enabled = state;
        }
        if (TrafficLightDataHandler.instance != null)
        {
            TrafficLightDataHandler.instance.SetButtonsActive(state);
            TrafficLightDataHandler.instance.enabled = state;
        }
        if (VehicleDataHandler.instance != null)
        {
            VehicleDataHandler.instance.SetButtonsActive(state);
            VehicleDataHandler.instance.enabled = state;
        }
    }

    /// <summary>
    /// Sets all camera movement on/off
    /// </summary>
    /// <param name="state">true == Camera moves / false == Camera freezes</param>
    private void SetCameraControllerActive(bool state)
    {
        CameraController.instance.enabled = state;
        if (ObjectController.instance != null)
            ObjectController.instance.enabled = state;
    }
}
