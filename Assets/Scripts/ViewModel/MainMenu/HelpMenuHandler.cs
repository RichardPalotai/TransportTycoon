using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelpMenuHandler : MonoBehaviour
{
    #region Private variables
    [SerializeField]
    private Button HelpMenu_btn;
    private Sprite DefaultHelpIcon;
    [SerializeField]
    private GameObject ScrollView_scrl;
    #endregion

    #region  Unity calls
    void Awake()
    {
        DefaultHelpIcon = HelpMenu_btn.image.sprite;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScrollView_scrl.SetActive(false);
        HelpMenu_btn.onClick.AddListener(OnHelpMenuClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Private methods
    private void OnHelpMenuClicked()
    {
        if (ScrollView_scrl.activeSelf)
        {
            HelpMenu_btn.image.sprite = DefaultHelpIcon;
            EventSystem.current.SetSelectedGameObject(null);
            ScrollView_scrl.SetActive(false);
        }
        else
        {
            HelpMenu_btn.image.sprite = HelpMenu_btn.spriteState.highlightedSprite;
            ScrollView_scrl.SetActive(true);
        }
    }
    #endregion
}
