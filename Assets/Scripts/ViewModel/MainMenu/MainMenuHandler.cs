using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
        get { return Game.GetSaves().Count > 0; }
    }
    public List<string> SaveNames => Game.GetSaves().Select(save => save.Name).OrderBy(name => name).ToList();
    #endregion

    #region Public methods    
    public Save GetSave(string name) => Game.GetSaves().First(save => save.Name == name);
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
        Save save = GetSave(GameSlot_dropdown.options[0].text);
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
}
