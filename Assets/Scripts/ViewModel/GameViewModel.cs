using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameViewModel : MonoBehaviour
{
    public static GameViewModel instance;
    public enum GameMode { BUILD, DEMOLISH, MOUSE }

    #region Private variables
    [SerializeField]
    private Canvas GameMenu_cnv;
    [SerializeField]
    private Canvas VehicleRoute_cnv;
    [SerializeField]
    private GameEntity selectedObject;

    private GameMode gameMode;
    private Mouse mouse;
    private Keyboard keyboard;
    private bool IsGameMenuOn;
    private bool IsDemolishOn;
    public static bool IsGameLoaded = false;
#nullable enable
    public static (string name, DateTime timeOfSave)? LoadedGame = null;
#nullable disable
    #endregion

    #region Properties
    public bool IsRouteDisplayOn { get; private set; }

    /// <summary>
    /// Play mode of the game (BUILD/DEMOLISH/MOUSE)
    /// </summary>
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

    /// <summary>
    /// The object that is selected by the mouse pointer click
    /// </summary>
    public GameEntity SelectedObject
    {
        get { return selectedObject; }
        set
        {
            selectedObject = value;
        }
    }
    #endregion

    #region Events
    public event Action<bool> OnRouteDisplayChanged;
    #endregion

    #region Unity calls
    void Awake()
    {
        instance = this;

        mouse = Mouse.current;
        keyboard = Keyboard.current;
        selectedObject = null;

        IsGameMenuOn = false;
        IsDemolishOn = false;
        IsRouteDisplayOn = false;
        gameMode = GameMode.MOUSE;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LoadedGame != null)
        {
            Game game = new();
            Game.instance = Game.LoadGame(new DataAccess(), LoadedGame?.name);
            MenuBarHandler.instance.SelectPauseButton();
        }
        else
        {
            Game game = new Game();
            Game.instance = game;
            MenuBarHandler.instance.SelectPlayButton();

            Game.instance.NewGame(new DataAccess());
        }
        Game.instance.OnGameOver += HandleGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        Game.instance.UpdateGame(Time.deltaTime);
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            if (!IsMouseOverUI() && gameMode == GameMode.BUILD)
            {
                try
                {
                    BuildingPlacer.instance.AttemptPlacement(mouse.position.ReadValue(), BuilderSelectorHandler.instance.selectedBuilding);
                    // TODO - Specify parameter!!!!! <BINDING> <MODEL>
                    Game.instance.Player.Purchase(SelectedObject as ITradeable);
                }
                catch (NotEnoughSpaceForObjectException e)
                {
                    ErrorHandler.instance.DisplayError(e.Tag, e.Message);
                }
                catch (FieldOverrideException e)
                {
                    ErrorHandler.instance.DisplayError(e.Tag, e.Message);
                }
            }
            else if (!IsMouseOverUI() && gameMode == GameMode.DEMOLISH && selectedObject == null)
            {
                // TODO - Destror building from  -- BuildingPlacer -- <BINDING>
                BuildingPlacer.instance.DemolishBuilding(mouse.position.ReadValue(), SelectedObject);
                Game.instance.Player.SellItem(SelectedObject as ITradeable);
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
    #endregion

    #region Private methods
    /// <summary>
    /// Determines whether the mouse is over a UI element or not
    /// </summary>
    /// <returns>true == mouse is over UI / false == mouse is not over UI</returns>
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    /// <summary>
    /// Sets the GameScreenUI on/off
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off</param>
    private void SetGameScreenUIActive(bool state)
    {
        MenuBarHandler.instance.gameObject.SetActive(state);
        BuilderSelectorHandler.instance.gameObject.SetActive(state);

        // TODO - REACTIVATE THE SELECTED VEHICLE DATA DISPLAY <BINDING>
        switch (selectedObject)
        {
            case City:
                CityDataHandler.instance.gameObject.SetActive(state);
                break;
            case ProdFacility:
                FacilityDataHandler.instance.gameObject.SetActive(state);
                break;
            case TrafficLight:
                TrafficLightDataHandler.instance.gameObject.SetActive(state);
                break;
            case Vehicle:
                VehicleDataHandler.instance.gameObject.SetActive(state);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Sets the buttons on/off on the GameScreenUI canvas
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off on</param>
    private void SetGameScreenUIButtonsActive(bool state)
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

    /// <summary>
    /// Turns on GameOver screen
    /// </summary>
    private void HandleGameOver()
    {
        GameOverHandler.instance.ToggleGameOverScreen();
    }
    #endregion

    #region Public methods

    public void SelectObject(GameEntity obj)
    {
        if (SelectedObject != null)
            return;
            
        SelectedObject = obj;
        switch (selectedObject)
        {
            case City:
                CityDataHandler.instance.SelectedCity = selectedObject as City;
                CityDataHandler.instance.gameObject.SetActive(true);
                break;
            case ProdFacility:
                FacilityDataHandler.instance.SelectedFacility = selectedObject as ProdFacility;
                FacilityDataHandler.instance.gameObject.SetActive(true);
                break;
            case TrafficLight:
                TrafficLightDataHandler.instance.SelectedTrafficLight = selectedObject as TrafficLight;
                TrafficLightDataHandler.instance.gameObject.SetActive(true);
                break;
            case Vehicle:
                VehicleDataHandler.instance.SelectedVehicle = selectedObject as Vehicle;
                VehicleDataHandler.instance.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Sets GameMenuUI on/off
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off</param>
    public void SetMenuActive(bool state)
    {
        IsGameMenuOn = state;
        GameMenu_cnv.gameObject.SetActive(state);
        SetGameScreenUIButtonsActive(!state);
        SetCameraControllerActive(!state);

        if (state)
            Game.instance.PauseGame();
        else
        {
            Game.instance.ResumeGame();
            MenuBarHandler.instance.SelectPlayButton();
        }
    }

    /// <summary>
    /// Sets the VehicleRouteUI on/off
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off</param>
    public void SetRouteDisplayActive(bool state)
    {
        IsRouteDisplayOn = state;
        VehicleRoute_cnv.gameObject.SetActive(state);
        SetGameScreenUIActive(!state);

        if (state)
        {
            Game.instance.PauseGame();
            VehicleRouteHandler.instance.LoadRoute();
        }
        else
        {
            Game.instance.ResumeGame();
            MenuBarHandler.instance.SelectPlayButton();
        }

        OnRouteDisplayChanged?.Invoke(state);
    }

    /// <summary>
    /// Sets the Build mode buttons active
    /// </summary>
    public void DeselectObject()
    {
        BuilderSelectorHandler.instance.SetBuildButtonsActive(true);
        // TODO - Reset the PROPERTIES <BINDING>
        switch (selectedObject)
        {
            case City:
                CityDataHandler.instance.SelectedCity = null;
                break;
            case ProdFacility:
                FacilityDataHandler.instance.SelectedFacility = null;
                break;
            case TrafficLight:
                TrafficLightDataHandler.instance.SelectedTrafficLight = null;
                break;
            case Vehicle:
                VehicleDataHandler.instance.SelectedVehicle = null;
                break;
            default:
                break;
        }
        selectedObject = null;
    }
    public void NewGame() => Game.instance.NewGame(new DataAccess());
    public void SaveGame() => Game.instance.SaveGame();
    public void LoadGame((string name, DateTime timeOfSave) game) => Game.LoadGame(new DataAccess(), game.name);
    #endregion
}
