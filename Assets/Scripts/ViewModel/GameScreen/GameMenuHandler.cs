using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuHandler : MonoBehaviour
{
    #region Private variables
    [SerializeField]
    private Button ResumeGame_btn;
    [SerializeField]
    private Button SaveGame_btn;
    [SerializeField]
    private Button MainMenu_btn;
    [SerializeField]
    private Button QuitGame_btn;
    #endregion

    #region Unity calls
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResumeGame_btn.onClick.AddListener(OnResumeGameClicked);
        SaveGame_btn.onClick.AddListener(OnSaveGameClicked);
        MainMenu_btn.onClick.AddListener(OnMainMenuClicked);
        QuitGame_btn.onClick.AddListener(OnQuitGameClicked);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Button click events
    private void OnResumeGameClicked()
    {
        GameViewModel.instance.SetMenuActive(false);
    }

    private void OnSaveGameClicked()
    {
        GameViewModel.instance.SaveGame();
    }

    private void OnMainMenuClicked()
    {
        GameViewModel.LoadedGame = null;
        GameViewModel.IsGameLoaded = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnQuitGameClicked()
    {
#if UNITY_EDITOR
        GameViewModel.LoadedGame = null;
        GameViewModel.IsGameLoaded = false;
        UnityEditor.EditorApplication.isPlaying = false;
#else
        GameViewModel.IsGameLoaded = false;
        GameViewModel.LoadedGame = null;
        Application.Quit();
#endif
    }
    #endregion
}
