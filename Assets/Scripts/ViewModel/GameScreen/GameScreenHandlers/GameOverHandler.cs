using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    public static GameOverHandler instance;

    #region Private variables
    [SerializeField]
    private Button TryAgain_btn;
    [SerializeField]
    private Button MainMenu_btn;
    #endregion

    #region Unity calls
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TryAgain_btn.onClick.AddListener(OnTryAgainClick);
        MainMenu_btn.onClick.AddListener(OnMainMenuClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Private methods
    private void OnTryAgainClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnMainMenuClick()
    {
        GameViewModel.LoadedGame = null;
        GameViewModel.IsGameLoaded = false;
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

    #region Public methods
    public void ToggleGameOverScreen() => gameObject.SetActive(!gameObject.activeSelf);
    #endregion
}
