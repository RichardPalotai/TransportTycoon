using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

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

    #region Unity calls
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewGame_btn.onClick.AddListener(OnNewGameClicked);
        LoadGame_btn.onClick.AddListener(OnLoadGameClicked);
        QuitGame_btn.onClick.AddListener(OnQuitGameClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Private variables
    private void OnNewGameClicked()
    {
        // TODO - Model, New Game
        SceneManager.LoadScene("GameScreen");
    }
    private void OnLoadGameClicked()
    {
        // TODO - Load Game
    }
    private void OnQuitGameClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    #endregion
}
