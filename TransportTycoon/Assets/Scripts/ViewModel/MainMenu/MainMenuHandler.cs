using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private Button NewGame_btn;
    [SerializeField]
    private Button LoadGame_btn;
    [SerializeField]
    private TMP_Dropdown GameSlot_dropdown;
    [SerializeField]
    private Button QuitGame_btn;
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

    private void OnNewGameClicked()
    {
        // TODO - Connect to model
        SceneManager.LoadScene("GameScreen");
    }
    private void OnLoadGameClicked()
    {
        // TODO - Milestone III.
    }
    private void OnQuitGameClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
