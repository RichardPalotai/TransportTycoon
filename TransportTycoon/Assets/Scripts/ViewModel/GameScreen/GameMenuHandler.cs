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
        // TODO - Save Game
        GameViewModel.instance.SaveGame();
    }

    private void OnMainMenuClicked()
    {
        // TODO - Save Game
        GameViewModel.instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnQuitGameClicked()
    {
        // TODO - Save Game
        GameViewModel.instance.SaveGame();
        GameViewModel.LoadedGame = null;
        UnityEditor.EditorApplication.isPlaying = false;
    }
    #endregion
}
