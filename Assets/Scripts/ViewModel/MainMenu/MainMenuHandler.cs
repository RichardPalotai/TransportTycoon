using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    #region Private variables
    [SerializeField]
    private Button NewGame_btn;
    [SerializeField]
    private Button LoadGame_btn;
    [SerializeField]
    private TMP_Dropdown GameSlot_dropdown;
    [SerializeField]
    private Button QuitGame_btn;
    #endregion

    #region Properties
    public bool IsThereSave
    {
        get
        {
            try
            {
                return Game.GetSaves(new DataAccess()).Count > 0;
            }
            catch (System.Exception e)
            {
                ErrorHandler.instance.DisplayError("Error", e.Message);
                return false;
            }
        }
    }
    public List<string> SaveNames => Game.GetSaves(new DataAccess()).Select(save => save.name).OrderBy(name => name).ToList();
    #endregion

    #region Public methods    
    public (string name, DateTime timeOfSave) GetSave(string name) => Game.GetSaves(new DataAccess()).FirstOrDefault(save => save.name == name);
    #endregion

    #region Unity calls
    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewGame_btn.onClick.AddListener(OnNewGameClicked);
        LoadGame_btn.onClick.AddListener(OnLoadGameClicked);
        QuitGame_btn.onClick.AddListener(OnQuitGameClicked);
        
        LoadGame_btn.interactable = IsThereSave;
        SetSlotsActive(IsThereSave);
        GameSlot_dropdown.onValueChanged.AddListener(OnNewSaveSelected);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Button click events
    private void OnNewGameClicked()
    {
        SceneManager.LoadScene("GameScreen");
    }
    private void OnLoadGameClicked()
    {
        (string name, DateTime timeOfSave) save = GetSave(GameSlot_dropdown.options[GameSlot_dropdown.value].text);
        GameViewModel.LoadedGame = save;
        SceneManager.LoadScene("GameScreen");

    }
    private void OnQuitGameClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Sets the GameSlot dropdown on/off
    /// </summary>
    /// <param name="state">true == GameSlots UI on / false == GameSlots UI off</param>
    private void SetSlotsActive(bool state)
    {
        GameSlot_dropdown.gameObject.SetActive(state);
        RectTransform rt = QuitGame_btn.GetComponent<RectTransform>();
        if (state)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -203);
            RefreshSlots();
        }
        else
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -130);
            return;
        }
    }

    /// <summary>
    /// Refreshes the saved game slots with the new saves
    /// </summary>
    private void RefreshSlots()
    {
        GameSlot_dropdown.ClearOptions();
        foreach (var name in SaveNames)
        {
            GameSlot_dropdown.options.Add(new TMP_Dropdown.OptionData(name));
        }
        GameSlot_dropdown.captionText.text = GameSlot_dropdown.options[0].text;
    }

    private void OnNewSaveSelected(int index)
    {
        GameSlot_dropdown.captionText.text = GameSlot_dropdown.options[index].text;
    }
    #endregion
}
