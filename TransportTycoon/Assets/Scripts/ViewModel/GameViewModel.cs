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
    [SerializeField]
    private GameObject selectedObject;

    private GameMode gameMode;
    private Mouse mouse;
    private Keyboard keyboard;
    private bool IsGameMenuOn;
    private bool IsDemolishOn;
    public bool IsRouteDisplayOn { get; private set; }

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

    public GameObject SelectedObject
    {
        get { return selectedObject; }
        set
        {
            selectedObject = value;
        }
    }

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
            else if (!IsMouseOverUI() && gameMode == GameMode.DEMOLISH && selectedObject == null)
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

        if (selectedObject != null)
        {
            BuilderSelectorHandler.instance.SetBuildButtonsActive(false);
            // SET SELECTED properties to given
            // switch (selectedObject)
            // {
            //     case "City":
            //         CityDataHandler.instance.SelectedCity = selectedObject;
            //         break;
            //     case "Facility":
            //         FacilityDataHandler.instance.SelectedFacility = selectedObject;
            //         break;
            //     case "TrafficLight":
            //         TrafficLightDataHandler.instance.SelectedTrafficLight = selectedObject;
            //         break;
            //     case "Vehicle":
            //         VehicleDataHandler.instance.SelectedVehicle = selectedObject;
            //         break;
            //     default:
            //         break;
            // }
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
    /// Sets the VehicleRouteUI canvas on/off
    /// </summary>
    /// <param name="state">true == UI is on / false == UI is off</param>
    public void SetRouteDisplayActive(bool state)
    {
        IsRouteDisplayOn = state;
        VehicleRoute_cnv.gameObject.SetActive(state);
        SetGameScreenUIActive(!state);
        //SetCameraControllerActive(!state);

        if (state)
            Game.instance.PauseGame();
        else
        {
            Game.instance.ResumeGame();
            MenuBarHandler.instance.SelectPlayButton();
        }
    }

    private void SetGameScreenUIActive(bool state)
    {
        MenuBarHandler.instance.gameObject.SetActive(state);
        BuilderSelectorHandler.instance.gameObject.SetActive(state);

        // TODO - Delete
        VehicleDataHandler.instance.gameObject.SetActive(state);

        // REACTIVATE THE SELECTED VEHICLE DATA DISPLAY
        // switch (selectedObject)
        // {
        //     case "City":
        //         CityDataHandler.instance.gameObject.SetActive(state);
        //         break;
        //     case "Facility":
        //         FacilityDataHandler.instance.gameObject.SetActive(state);
        //         break;
        //     case "TrafficLight":
        //         TrafficLightDataHandler.instance.gameObject.SetActive(state);
        //         break;
        //     case "Vehicle":
        //         VehicleDataHandler.instance.gameObject.SetActive(state);
        //         break;
        //     default:
        //         break;
        // }
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

    public void DeselectObject()
    {
        BuilderSelectorHandler.instance.SetBuildButtonsActive(true);
        // Reset the PROPERTIES
        // switch (selectedObject)
        // {
        //     case "City":
        //         CityDataHandler.instance.SelectedCity = null;
        //         break;
        //     case "Facility":
        //         FacilityDataHandler.instance.SelectedFacility = null;
        //         break;
        //     case "TrafficLight":
        //         TrafficLightDataHandler.instance.SelectedTrafficLight = null;
        //         break;
        //     case "Vehicle":
        //         VehicleDataHandler.instance.SelectedVehicle = null;
        //         break;
        //     default:
        //         break;
        // }
        selectedObject = null;
    }
}
