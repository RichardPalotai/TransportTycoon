using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private Button ResumeGame_btn;
    [SerializeField]
    private Button SaveGame_btn;
    [SerializeField]
    private Button MainMenu_btn;
    [SerializeField]
    private Button QuitGame_btn;
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

    private void OnResumeGameClicked()
    {
        GameViewModel.instance.SetMenuActive(false);
    }

    private void OnSaveGameClicked()
    {
        
    }

    private void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnQuitGameClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
