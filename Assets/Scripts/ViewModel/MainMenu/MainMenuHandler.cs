<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
=======
>>>>>>> origin/master
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
<<<<<<< HEAD
    #region Private variables
=======
>>>>>>> origin/master
    [SerializeField]
    private Button NewGame_btn;
    [SerializeField]
    private Button LoadGame_btn;
    [SerializeField]
    private TMP_Dropdown GameSlot_dropdown;
    [SerializeField]
    private Button QuitGame_btn;
<<<<<<< HEAD
    #endregion

    #region Properties
    public bool IsThereSave
    {
        get
        {
            try
            {
                return Game.GetSaves().Count > 0;
            }
            catch (System.Exception e)
            {
                ErrorHandler.instance.DisplayError("Error", e.Message);
                return false;
            }
        }
    }
    public List<string> SaveNames => Game.GetSaves().Select(save => save.name).OrderBy(name => name).ToList();
    #endregion

    #region Public methods    
    public (string name, DateTime timeOfSave) GetSave(string name) => Game.GetSaves().First(save => save.name == name);
    #endregion

    #region Unity calls
    void Awake()
    {

    }

=======
>>>>>>> origin/master
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewGame_btn.onClick.AddListener(OnNewGameClicked);
        LoadGame_btn.onClick.AddListener(OnLoadGameClicked);
        QuitGame_btn.onClick.AddListener(OnQuitGameClicked);
<<<<<<< HEAD

        LoadGame_btn.interactable = IsThereSave;
        SetSlotsActive(IsThereSave);
=======
>>>>>>> origin/master
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD

    }
    #endregion

    #region Button click events
    private void OnNewGameClicked()
    {
=======
        
    }

    private void OnNewGameClicked()
    {
        // TODO - Connect to model
>>>>>>> origin/master
        SceneManager.LoadScene("GameScreen");
    }
    private void OnLoadGameClicked()
    {
<<<<<<< HEAD
        (string name, DateTime timeOfSave) save = GetSave(GameSlot_dropdown.options[0].text);
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
        Debug.LogWarning(state);
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
    }
    #endregion
=======
        // TODO - Milestone III.
    }
    private void OnQuitGameClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
>>>>>>> origin/master
}
