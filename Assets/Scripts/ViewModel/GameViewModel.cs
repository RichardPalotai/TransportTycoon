using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using ViewModel.GameScreen.UIHandlers;

public class GameViewModel : MonoBehaviour
{
    public static GameViewModel instance;

    [SerializeField]
    private Canvas GameMenu_cnv;

    private Mouse mouse;
    private Keyboard keyboard;

    private bool IsGameMenuOn;
    public static bool IsGameLoaded = false;
    public static Save LoadedGame = null;

    // TODO - CHANGES FROM MODEL MUST BE MADE VISIBLE FOR NewGame/SaveGame/LoadGame
    public void NewGame() => Game.instance.NewGame();
    public void SaveGame() => Game.instance.SaveGame();
    public void LoadGame(Save game) => Game.instance.LoadGame(game);

    void Awake()
    {
        instance = this;

        mouse = Mouse.current;
        keyboard = Keyboard.current;

        IsGameMenuOn = false;

        Game game = new Game();
        Game.instance = game;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameMenu_cnv.gameObject.SetActive(false);
        if (LoadedGame != null)
        {
            Game.instance.LoadGame(LoadedGame);
        }
        else
        {
            Game.instance.NewGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Game.instance.UpdateGame(Time.deltaTime);
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            if (!IsMouseOverUI() && BuilderSelectorHandler.instance.BuildMode)
            {
                BuildingPlacer.instance.AttemptPlacement(mouse.position.ReadValue());
            }
        }
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
        {
            SetMenuActive(!IsGameMenuOn);
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void SetMenuActive(bool state)
    {
        IsGameMenuOn = state;
        GameMenu_cnv.gameObject.SetActive(state);
        // TODO - DISABLE MenuBar and BuilderSelector
        MenuBarHandler.instance.SetButtonsActive(!state);
        BuilderSelectorHandler.instance.SetButtonsActive(!state);

        if (CityDataHandler.instance != null)
        {
            CityDataHandler.instance.SetButtonsActive(!state);
            CityDataHandler.instance.enabled = !state;
        }
        if (FacilityDataHandler.instance != null)
        {
            FacilityDataHandler.instance.SetButtonsActive(!state);
            FacilityDataHandler.instance.enabled = !state;
        }
        if (TrafficLightDataHandler.instance != null)
        {
            TrafficLightDataHandler.instance.SetButtonsActive(!state);
            TrafficLightDataHandler.instance.enabled = !state;
        }
        if (VehicleDataHandler.instance != null)
        {
            VehicleDataHandler.instance.SetButtonsActive(!state);
            VehicleDataHandler.instance.enabled = !state;
        }

        CameraController.instance.enabled = !state;
        if (ObjectController.instance != null)
            ObjectController.instance.enabled = !state;

        if (state)
            Game.instance.PauseGame();
        else
            Game.instance.ResumeGame();
    }
}
